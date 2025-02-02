﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.FirebasePushNotification;
using Android.Media;

namespace XFFCMPushNotificationsSample.Droid
{
    [Activity(Label = "XFFCMPushNotificationsSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            FirebasePushNotificationManager.ProcessIntent(this, Intent);

            //if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            //{
            //    // Notification channels are new in API 26 (and not a part of the
            //    // support library). There is no need to create a notification 
            //    // channel on older versions of Android.
            //    return;
            //}

            //var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
            //{
            //    Description = "Firebase Cloud Messages appear in this channel"
            //};

            //var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            //notificationManager.CreateNotificationChannel(channel);
           
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
