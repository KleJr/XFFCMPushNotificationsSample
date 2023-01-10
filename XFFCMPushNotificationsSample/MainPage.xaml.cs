using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFCMPushNotificationsSample.ViewModels;

namespace XFFCMPushNotificationsSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage 
    {
     
        public MainPage()
        {
            InitializeComponent();
      

            BindingContext = MainViewModel.TextTest ;

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            }   
        }

        async void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
           string body = "";
            string title = "";
            foreach (var data in e.Data)
            {

                if (data.Key == "title")
                {
                    title = data.Value.ToString();
                }
                if (data.Key == "body")
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
