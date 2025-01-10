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

            //Este método recibe la respuecta de cualquier query realizada
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

                var hola = data.AckData.Records;

                foreach (var record in hola)
                {
                    //Procedo a solicitar los adjuntos de cada registro
                    string messageID = Guid.NewGuid().ToString();
                    Console.WriteLine("Getting Attahcments .." + record.First() + " " + messageID);
                    client.Value.SendAttachmentRequest4("SELECT * FROM \"multi_event-@attachment\" WHERE id='" + record.First() + "' FOR UPDATE WAIT 86400", messageID);

                    var hola2 = record;
                }

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

            //Este método no hace nada pero es necesario por la arqitectura del SDK y GDS
            public override void OnAttachmentRequestAck5(MessageHeader header, AttachmentRequestAckData data)
            {
                Console.WriteLine("Attachment request ack message received with '" + data.Status + "' status code");
                if (data.AckData != null)
                {
                    byte[] attachment = data.AckData.Result.Attachment;
                    if (attachment == null)
                    {
                        //if you requested the binary, the attachment will be sent as an 'attachment response' type message at a later time
                    }
                }
            }

            //Este método recibe los datos de los adjuntos solicitados
            public override void OnAttachmentResponse6(MessageHeader header, AttachmentResponseData data)
            {
                //Para saber a que adjunto corresponde la respuesta, se puede obtener el messageID del header
                //Este fue el que nosotros generamos al solicitar el adjunto
                string messageID = header.MessageId;
                byte[] attachment = data.Result.Attachment;

                //Una vez recibido el adjunto, debemos informar a GDS que lo recibimos correctamente
                //De lo contrario, GDS lo volverá a enviar indefinidamente
                client.Value.SendAttachmentResponseAck7(StatusCode.OK,
                        new AttachmentResponseAckTypeData(StatusCode.Created,
                            new AttachmentResponseAckResult(
                                data.Result.RequestIds,
                                data.Result.OwnerTable,
                                data.Result.AttachmentId
                            )
                        )
                    );
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
