using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using WindowsAzure.Messaging;
using System.Collections.Generic;
using Common;
using CoreLogic;

namespace BossMandadero.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "FCM token: " + refreshedToken);
            SendRegistrationToServer();
        }

        void SendRegistrationToServer()
        {
            // Register with Notification Hubs
            hub = new NotificationHub(GlobalValues.NotificationHubName,
                                      GlobalValues.ListenConnectionString, this);

            var tags = new List<string>() { };
            var regID = hub.Register(User.Usuario.Correo, tags.ToArray()).RegistrationId;

            Log.Debug(TAG, $"Successful registration of ID {regID}");
        }
    }
}
