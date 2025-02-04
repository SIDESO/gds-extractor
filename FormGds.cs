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
using System.Runtime.CompilerServices;
using System.Net;
using static GDSExtractor.SdkGds;
using Newtonsoft.Json;

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

        //get endpoint
        public string getEndpoint()
        {
            return this.endPoint;
        }
        //get







        public FormGds()
        {
            args = Environment.GetCommandLineArgs();

            InitializeComponent();

            //if (args.Length < 2)
            //{
            //    MessageBox.Show("Este aplicativo requiere ser iniciado desde TránsitoApp", "Error");
            //    this.Close();
            //    return;
            //}

            this.url = "gdsdeis://transitoapp.co?" +
                "accessToken=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiOGVhNGI4N2Y2YjQ4MDJhNDZmOGRhN2JkMWFjMDQ4NTNiZDQ5ZjBjNjkwOTUyMjRiMTQzYmFkOWVjNGYwMGY3N2ZhYzYzMjQ5ZmZkN2YzZDIiLCJpYXQiOjE3MzY5NzEwOTAuMzg5MjcxLCJuYmYiOjE3MzY5NzEwOTAuMzg5MjczLCJleHAiOjE3Njg1MDcwOTAuMzgwMzE2LCJzdWIiOiI4MCIsInNjb3BlcyI6WyJnZHNEZWlzIl19.ctykszlg0VZt9Ia4n7WsqSBlwnKRyzcwcXeQJnp7YVy9uopTnXz7Qc_yg3nZrXf_tr_tY405TrHnuhEcpSgvwri-M-uA28Tp9cLskBgoKeGINOXfb4XmQB-QmIxNFuqXqOAlbM7AMdzuBBVIC5m50CsXs4wvhOpIw40KkR7SynFkkUG1k7laf4Lj99I3_UphuPzr4TqPLVDg0ZKJ9l5ltQ2LuDed6TaocHgcbSvT5SrvujdY3UDhvzux6DBzOyrr5sYVdGyY4QSsyyJNyimpRBqBb5C3eX0ba9CfsAwnS9lg15a5tBgPZtLkEXUoVcM-7kTBCygszUnA1gNjl7wcikRvUWd97Tl-TKzimhBlh4opK-uzNh4_WHyELn3hfJiXRlo2tpxxrWDfBIgFWuPTAhwEZ3Pvwk-RsEG9Cjofi8X7qgbFqcB0O-aKBjg8T6EuDPKD85_FyNR-8J-ZNtovtUab-EQmpsrlm-SEgcHjWQoOvSYQXTN82EJbZorEIErjX6gVatXBKHhSB1L2-VZM15lcs3vG8yjDwY5aYSUiPg2edxI2iK8dmQflBZ64BmSwh42dUaQ-Oxr1R-oAxRP1I9PQtK_QNYPBMUtvlbyj3_m-L_D5rA8CX5Lo1XcLS09ZTaB2u4XA5ML4A_C3AU2OY_4UrNVmJPozWx8qLhrp0RU" +
                "&endPoint=http%3A%2F%2F127.0.0.1%3A8000" +
                "&userName=Luis+Villera+Pastrana" +
                "&command=gdsDeis";

            ValidateParamsAndBoot();


            //crear el directorio de imagenes
            // Specify the directory you want to manipulate.
            string path = @"c:\images_gds";

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());

            }


            Reference<AsyncGDSClient> clientRef = new Reference<AsyncGDSClient>(null);
            TestListener listener = new TestListener(clientRef, this);
            AsyncGDSClient client = AsyncGDSClient.GetBuilder()
                .WithListener(listener)
                .WithTimeout(10000)
                 .WithUserName("admin")
                .WithPingPongInterval(10000)
                .Build();
            clientRef.Value = client;
            client.Connect();

            //countdown.Wait();
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

                this.userLabel.Text = paramsCollection["userName"];

                //agregar las columnas al datagrid
                this.dataGridDeis.Columns.Add("id", "ID");
                this.dataGridDeis.Columns.Add("plate", "Placa");
                this.dataGridDeis.Columns.Add("date", "Fecha");
                this.dataGridDeis.Columns.Add("max_speed", "Velocidad");
                this.dataGridDeis.Columns.Add("camera", "Camara");
                this.dataGridDeis.Columns.Add("resultado", "Resultado");
                this.dataGridDeis.Columns.Add("msg_id", "uuuid mensaje");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la aplicación " + url + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

        }


        public HttpClient HttpClientF()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

            return httpClient;
        }


    }
}
