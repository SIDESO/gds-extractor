using Gds.Messages.Data;
using Gds.Messages.Header;
using Gds.Messages;
using Gds.Utils;
using messages.Gds.Websocket;

namespace GDSExtractor
{
    internal class Program
    {
        private static CountdownEvent countdown = new CountdownEvent(1);
        private class Reference<T>
        {
            public T Value { get; set; }
            public Reference(T val)
            {
                Value = val;
            }
        }

        private class TestListener : IGDSMessageListener
        {
            private readonly Reference<AsyncGDSClient> client;
            public TestListener(Reference<AsyncGDSClient> client)
            {
                this.client = client;
            }
            public override void OnConnectionSuccess(MessageHeader header, ConnectionAckData data)
            {
                Console.WriteLine("Client successfully connected!");

                Console.WriteLine("Sending query message..");
                client.Value.SendQueryRequest10(
                    "SELECT * FROM multi_event LIMIT 1000", ConsistencyType.NONE, 10000L
                    );
            }

            public override void OnDisconnect()
            {
                Console.WriteLine("Client disconnected!");
            }

            public override void OnConnectionFailure(Either<Exception, KeyValuePair<MessageHeader, ConnectionAckData>> cause)
            {
                Console.WriteLine(string.Format("Client got wrecked! Reason: {0}", cause.ToString()));
                countdown.Signal();
            }

            public override void OnQueryRequestAck11(MessageHeader header, QueryRequestAckData data)
            {
                if (data.Status != StatusCode.OK)
                {
                    Console.WriteLine("Select failed!");
                    Console.WriteLine(string.Format("Status: {0}, Reason: {1}", data.Status, data.Exception));
                    client.Value.Close();
                    countdown.Signal();
                    return;
                }

                Console.WriteLine(string.Format("The client received {0} records.", data.AckData.NumberOfHits));

                if (data.AckData.HasMorePage)
                {
                    client.Value.SendMessage(
                       MessageManager.GetHeader("user", DataType.NextQueryPageRequest),
                       MessageManager.GetNextQueryPageRequest(data.AckData.QueryContextDescriptor, 10000L)
                   );
                }
                else
                {
                    countdown.Signal();
                    client.Value.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            Reference<AsyncGDSClient> clientRef = new Reference<AsyncGDSClient>(null);
            TestListener listener = new TestListener(clientRef);

            AsyncGDSClient client = AsyncGDSClient.GetBuilder()
                .WithListener(listener)
                .WithTimeout(10000)
                .WithPingPongInterval(10000)
                .Build();
            clientRef.Value = client;


            client.Connect();

            countdown.Wait();
        }
    }
}
