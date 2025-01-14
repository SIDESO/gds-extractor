using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Gds.Messages.Data;
using Gds.Messages.Header;
using Gds.Messages;
using Gds.Utils;
using messages.Gds.Websocket;

namespace GDSExtractor
{
    public partial class FormGds : Form
    {

        string[] args;
        string accessToken;
        string endPoint;
        string subjectName;
        string issuerName;
        string command;
        string url;

        private class Reference<T>
        {
            public T Value { get; set; }
            public Reference(T val)
            {
                Value = val;
            }
        }


        private readonly Reference<AsyncGDSClient> client;

        private static CountdownEvent countdown = new CountdownEvent(1);


        public FormGds()
        {
            args =  Environment.GetCommandLineArgs();

            InitializeComponent();

            //if (args.Length < 2)
            //{
            //    MessageBox.Show("Este aplicativo requiere ser iniciado desde TránsitoApp", "Error");
            //    this.Close();
            //    return;
            //}

            this.url = "tappsigner://transitoapp.co?accessToken=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMDZkZGNjYTE3OTA0OWNlOWEzNTlmNjMxZjBiYzhkMDE4NjFlZDhlYWJhNzUzYTUyNzVlMjI4ZTEyNjhjNjQ4OTVkZThlN2Q1MmMyZmJjOTEiLCJpYXQiOjE3MzY4NzcyMDEuNDYxOTc4LCJuYmYiOjE3MzY4NzcyMDEuNDYxOTgxLCJleHAiOjE3Njg0MTMyMDEuMTcyNDQyLCJzdWIiOiI4MCIsInNjb3BlcyI6WyJ0YXBwU2lnbmVyIl19.H4elKNFtMPP1i-rgb_sHNsnNdO1o79OFF3lsoFjNR2qBPNltalnF79Pj1nYyZqHjTo5mTwa6mFL6SYTMgPn_xhzsMEdliSlV2aohye1K_E2mxCPJ-4krZap1t_tNznfqVObf_qHQ1cYAG9YIikKPzVhYFM8f6LK_7TLWTYTNCQxEao8fpYnZuH34T28Z8mVq-L9Vi_fv_YGJ5gM7WKUN7h0IG4dEYq0rhsHK1UMdzgIbZb5YmcYePxZ5-uaWZpdq12UkR8e1SSNLA2fkAaJXrHvfL9DPq1N4r3YKxYQkODGK5MOQ487Z8x__tAvJglarOwYCDw-p2QS2SwcyDla0T0ssgvi8Rks1dAN0-Qj7QnMyw2rjqXRpiaRm7ZKLVxK_ndvxeTxyUKQXkHjDS19orlnbraoDJ_K-ETU1m5HIutEBrebmqnzUUa4K1FGCnq2q6h9tZ80GJ1R5h9UIQs3LmGQNsGfKfC9sfBeEtNYAS6m4ExaHbd2O319L92RAfoeLL0WBQxXYvz07VJx6HhmV5Zzia03LdM_eaE8AvBQYlyvf0lVhzxgMSu8CUte18ng0xuSaGVrnejEKwOzrM_H-60pV0KMr2d6vIg5FpMum7ASmzSZGPMBw48G6y5Mlgk5VKzik4CtnOzvc8P3NRtXVOdVoV4isfW6AS9s4qHTEPZY&endPoint=http%3A%2F%2F127.0.0.1%3A8000&signerName=Luis+Villera+Pastrana&command=dei&skipSign=no/";

            ValidateParamsAndBoot();


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


        /**
         * validate the parameters and boot the application
         */
        private void ValidateParamsAndBoot()
        {

            try
            {

                string queryString = this.url.Split("?")[1];
                var paramsCollection = HttpUtility.ParseQueryString(queryString);

                accessToken = paramsCollection["accessToken"];
                if (accessToken == null || accessToken == String.Empty)
                {
                    MessageBox.Show("No se encontró el token web", "Error");
                    this.Close();
                }

                endPoint = paramsCollection["endPoint"];
                if (endPoint == null || endPoint == String.Empty)
                {
                    MessageBox.Show("No se definió un endpoint");
                    this.Close();
                }

                command = paramsCollection["command"];
                if (string.IsNullOrEmpty(command))
                {
                    MessageBox.Show("No se encontró la accion requerida");
                    this.Close();
                }
                this.userLabel.Text = paramsCollection["signerName"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la aplicación " + url + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
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

            //Este método recibe la respuesta de cualquier query realizada
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

                var dataRecords = data.AckData.Records;
                var fields = data.AckData.FieldDescriptors;

                foreach (var record in dataRecords)
                {
                    processDataTapp(record);

                    //Procedo a solicitar los adjuntos de cada registro
                    string messageID = Guid.NewGuid().ToString();
                    var recordId = record.First();

                    Console.WriteLine("Getting Attahcments .." + recordId + " " + messageID);
                    client.Value.SendAttachmentRequest4("SELECT * FROM \"multi_event-@attachment\" WHERE id='" + recordId + "' FOR UPDATE WAIT 86400", messageID);

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


            //metodo para procesar la informacion y enviarla a transito app
            private void processDataTapp(List<object> record)
            {
                //FieldDescriptor	FieldDescriptor.fieldName	FieldDescriptor.fieldType	FieldDescriptor.mimeType	FieldDescriptor.FieldName	FieldDescriptor.FieldType	FieldDescriptor.MimeType

                //crear la data que se va a enviar a transito app

                Dei dei = new Dei
                {
                    id = record[0].ToString(),
                    plate = record[63].ToString(), //plate
                    max_speed = record[18].ToString(), //average_speed
                    date = new DateTime((long)record[1]),
                    camera = record[32].ToString(), //entry_device_id
                    resultado = "OK"

                };


                //Aqui se procesa la informacion y se envia a transito app


            }
        }

    }


}
