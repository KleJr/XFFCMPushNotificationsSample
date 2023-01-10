using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace XFFCMPushNotificationsSample.Send
{
    class Program
    {
        static void Main(string[] args)
        {
           
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("private_key.json")
            });
            // This registration token comes from the client FCM SDKs.
            var registrationToken = "c8Y-_jR-T5q_s7hzs1bPz1:APA91bEyT3CmjfpH2TkfWHzU0-dRirFMcDm6P2tBCskMSgf2OHfkxP4LP-hH4Bb_AASc0F12PRitwnWQipuP3OlgdXFXqlcvNVnWo2dYnSE80lNYNLhdy1_YR4IO98WxFa70n7d8RB_l";


            // See documentation on defining a message payload.
            var message = new Message()
            {
                Token = registrationToken,
                
                Data = new Dictionary<string, string>()
                {
                    { "Hatzala", "Emergencia" },
                   
                },
               //Topic = "all",
               //Condition = "general",
                Notification = new Notification()
                {
                    Title = "Emergencia na Ponte",
                    Body = "Batida de Carro sentido Rio Preto!",
                    ImageUrl = "https://hatzala.com.br/wp-content/uploads/2022/08/logo-hatzala.png"

                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response =  FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            Console.WriteLine($"{response} messages were sent successfully");
            // Response is a message ID string.

        }


    }
}
