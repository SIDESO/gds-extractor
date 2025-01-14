
namespace GDSExtractor
{
    internal class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            ApplicationConfiguration.Initialize();

            var sentryOptions = new SentryOptions
            {
                // Tells which project in Sentry to send events to:
                Dsn = "https://2b20b1362a68e7d76e53fa4dfbc711c9@o4505348454744064.ingest.us.sentry.io/4508376324243456",

                // When configuring for the first time, to see what the SDK is doing:
                Debug = true,

                // Set traces_sample_rate to 1.0 to capture 100% of transactions for tracing.
                // We recommend adjusting this value in production.
                //TracesSampleRate = 1.0,

                // Enable Global Mode since this is a client app
                //IsGlobalModeEnabled = true,

                //TODO: any other options you need go here
            };

            using (SentrySdk.Init(sentryOptions))
            {
                Application.Run(new FormGds());
            }
        }
    }
}
