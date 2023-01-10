using System;
using System.Xml.Linq;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFCMPushNotificationsSample.ViewModels;

namespace XFFCMPushNotificationsSample
{
    public partial class App : Application
    {

        public string textTest;
        public App()
        {
            InitializeComponent();

           
            CrossFirebasePushNotification.Current.Subscribe("all");

            
            MainPage = new MainPage();

        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
          //  MainViewModel.TextTest = e.Token;
           // (MainPage)App.Current.MainPage += (MainPage).BindingContext = MainViewModel.TextTest;
        }

        protected override void OnStart()
        {
            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;

          
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }
    }
}
