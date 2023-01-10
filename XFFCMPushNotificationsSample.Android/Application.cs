using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace XFFCMPushNotificationsSample.Droid
{
    [Application]
    public class MainApplication : Android.App.Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public async override void OnCreate()
        {
            base.OnCreate();

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";

                FirebasePushNotificationManager.DefaultNotificationChannelImportance = NotificationImportance.Max;
            }

#if DEBUG
            FirebasePushNotificationManager.Initialize(this,
                          new NotificationUserCategory[]
                          {
                    new NotificationUserCategory("message",new List<NotificationUserAction> {
                        new NotificationUserAction("Reply","Reply", NotificationActionType.Foreground),
                        new NotificationUserAction("Forward","Forward", NotificationActionType.Foreground)

                    }),
                    new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept", NotificationActionType.Default, "check"),
                    new NotificationUserAction("Reject","Reject", NotificationActionType.Default, "cancel")
                    })
                          }, true);
#else
                        FirebasePushNotificationManager.Initialize(this,
                new NotificationUserCategory[]
                {
                    new NotificationUserCategory("message",new List<NotificationUserAction> {
                        new NotificationUserAction("Reply","Reply", NotificationActionType.Foreground),
                        new NotificationUserAction("Forward","Forward", NotificationActionType.Foreground)

                    }),
                    new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept", NotificationActionType.Default, "check"),
                    new NotificationUserAction("Reject","Reject", NotificationActionType.Default, "cancel")
                    })
                }, false);
#endif

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived_Android;

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Notification action tapped");


                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }

                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
        }
        async void Current_OnNotificationReceived_Android(object source, FirebasePushNotificationDataEventArgs e)
        {
            string body = "";
            string title = "";
            foreach (var data in e.Data)
            { 
              
                if (data.Key == "title")
                {
                    title = data.Value.ToString();
                }if (data.Key == "body")
                {
                    body = data.Value.ToString();
                }
                  
                System.Diagnostics.Debug.WriteLine($" {data.Key} : {data.Value}");
            }
            Device.BeginInvokeOnMainThread(async () => {
                bool answer = await App.Current.MainPage.DisplayAlert($"{title}", $"{body}", "Aceitar", "Não Posso");
                System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            });
            
        }

    }
}
