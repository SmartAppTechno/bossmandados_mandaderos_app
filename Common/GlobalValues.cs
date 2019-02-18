using System;
namespace Common
{
    public class GlobalValues
    {
        //public const string AppURL = @"https://bossmandadosapi.azurewebsites.net";

        //TODO: QUITAR EL LOCALHOST
        public const string AppURL = @"http://localhost:50013/";

        //public const string AppURL = @"https://backend.bossmandados.com";
        public const string ListenConnectionString = "Endpoint=sb://bossmandadosnotification.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=YdrSt4M5gVAJuzxzpN+CJRTA9AiYK2dZpmrNiBZ2DbQ=";
        public const string NotificationHubName = "BossMandados_Notification";
    }
}
