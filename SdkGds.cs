using Gds.Messages.Data;
using Gds.Messages.Header;
using Gds.Messages;
using Gds.Utils;
using messages.Gds.Websocket;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Newtonsoft.Json;
using Microsoft.VisualBasic.ApplicationServices;

namespace GDSExtractor
{
    internal class SdkGds
    {

        public static CountdownEvent countdown = new CountdownEvent(1);
        public class Reference<T>
        {
            public T Value { get; set; }
            public Reference(T val)
            {
                Value = val;
            }
        }

        public class TestListener : IGDSMessageListener
        {
            private readonly Reference<AsyncGDSClient> client;

            FormGds formGds;

            List<Dei> list_deis = new List<Dei>();

            public TestListener(Reference<AsyncGDSClient> client, FormGds formGds)
            {
                this.client = client;
                this.formGds = formGds;
            }



            public override void OnConnectionSuccess(MessageHeader header, ConnectionAckData data)
            {

                this.formGds.Invoke((MethodInvoker)delegate
                {
                    this.formGds.infoConection.Text = "Cliente conectado, enviando query";

                });

                client.Value.SendQueryRequest10(
                    "SELECT * FROM multi_event LIMIT 10", ConsistencyType.NONE, 10000L
                    );
            }

            public override void OnDisconnect()
            {
                this.formGds.Invoke((MethodInvoker)delegate
                {
                    this.formGds.infoConection.Text = "Cliente desconectado";

                });
            }

            public override void OnConnectionFailure(Either<Exception, KeyValuePair<MessageHeader, ConnectionAckData>> cause)
            {


                this.formGds.Invoke((MethodInvoker)delegate
                {
                    var message = string.Format("Client got wrecked! Reason: {0}", cause.ToString());
                    this.formGds.infoConection.Text = message;

                });

                countdown.Signal();
            }

            //Este método recibe la respuesta de cualquier query realizada
            public override void OnQueryRequestAck11(MessageHeader header, QueryRequestAckData data)
            {
                if (data.Status != StatusCode.OK)
                {
                    this.formGds.Invoke((MethodInvoker)delegate
                    {
                        var message = string.Format("Status: {0}, Reason: {1}", data.Status, data.Exception);
                        this.formGds.infoConection.Text = message;

                    });
                   
                   // client.Value.Close();
                  //  countdown.Signal();
                    return;
                }

                this.formGds.Invoke((MethodInvoker)delegate
                {
                    var message = string.Format("The client received {0} records.", data.AckData.NumberOfHits);
                    this.formGds.infoConection.Text = message;

                });

                var dataRecords = data.AckData.Records;
                //var fields = data.AckData.FieldDescriptors;

                //enviar a procesar cada resultados
                foreach (var record in dataRecords)
                {

                    processDataTapp(record);
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

            /*
             * Este método recibe los datos de los adjuntos solicitados
             * 
             */
            public override void OnAttachmentResponse6(MessageHeader header, AttachmentResponseData data)
            {
                //Para saber a que adjunto corresponde la respuesta, se puede obtener el messageID del header
                //Este fue el que nosotros generamos al solicitar el adjunto
                string messageID = header.MessageId;
                byte[] attachment = data.Result.Attachment;

                //enviar adjunto a transito app
                if (attachment != null)
                {
                    //descargar el archivo
                    string path = Path.Combine("C:\\images_gds\\", messageID + ".jpg");


                    File.WriteAllBytes(path, attachment);
                    //enviar a transito app
                    //this.formGds.Invoke((MethodInvoker)delegate
                    //{
                    //    this.formGds.labelInfoRow.Text = "Sending



                }


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

                //darle un uuid para el mensaje
                string messageID = Guid.NewGuid().ToString();

                //Los adjuntos se solicitan por separado estan el poción de 9 en foma de array
                //tambien parecen ser 3,tambien en la 91

                var adjuntos = record[91] as List<object>;


                //Los adjuntos se solicitan por separado estan el poción de 72 en foma de array

                //fecha 5
                //id 71
                //placa 98
                //velocida  121
                //camara 119



                //crear la data que se va a enviar a transito app
                Dei dei = new Dei
                {
                    id = record[71].ToString(),
                    license_plate = record[98].ToString(), //plate
                    max_speed = record[121].ToString(), //average_speed
                    date = record[5].ToString(),
                    camera_serial = record[119].ToString(), //entry_device_id
                    resultado = "OK",
                    data=messageID

                };

                //agregar a la lista
                list_deis.Add(dei);



                //se agrega a la lista de deis  en la tabla de la interfaz
                this.formGds.Invoke((MethodInvoker)delegate
                {
                   int index =  this.formGds.dataGridDeis.Rows.Add(dei.id, dei.license_plate, dei.date, dei.max_speed, dei.license_plate, dei.resultado, messageID);



                    if (adjuntos != null)
                    {
                        foreach (var adjunto in adjuntos)
                        {
                            var adjuntoId = adjunto.ToString();
                            this.formGds.Invoke((MethodInvoker)delegate
                            {
                                var message = "Getting Attahcments .." + adjuntoId + " " + messageID;

                            });
                            getAttachment(adjuntoId, messageID);
                        }
                    }

                   // getAttachment(dei.id, messageID);


                    

                });


                ////crear la dei en transito app sin las imagenes
                ///
                




                //solicitar los adjuntos de cada registro
                //var recordId = record.First();

                //this.formGds.Invoke((MethodInvoker)delegate
                //{
                //    var message = "Getting Attahcments .." + recordId + " " + messageID;
                //    this.formGds.labelInfoRow.Text = message;

                //});
                





            }


            /**
             * obtenr adjuntos
             */
            public void getAttachment(string recordId, string messageID)
            {
                //enviar la consulta de los adjuntos
                client.Value.SendAttachmentRequest4("SELECT * FROM \"multi_event-@attachment\" WHERE id='" + recordId + "' FOR UPDATE WAIT 86400", messageID);

            }



            private async void createDeiAsync(Dei d, int deiIndex)
            {
                HttpClient httpClient = this.formGds.HttpClientF();

                string url = this.formGds.getEndpoint() + "/windows_apps_api/dei_create";
                string json = d.json();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                var contentResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var definition = new { success = "", id = "" };
                    var data = JsonConvert.DeserializeAnonymousType(contentResponse, definition);

                    this.formGds.Invoke((MethodInvoker)delegate
                    {
                        DataGridViewRow row = this.formGds.dataGridDeis.Rows[deiIndex];
                        row.Cells[0].Value = data.id;
                        d.resultado = data.id;

                    });
                }
                else
                {
                    this.formGds.Invoke((MethodInvoker)delegate
                    {
                        d.resultado = "ERROR";

                    });
                }
            }

        }


    }
}
