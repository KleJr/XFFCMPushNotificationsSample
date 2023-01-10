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
            var registrationToken = "e68UzY__Rj-EpYahqX2efU:APA91bEaJ9s3pdu6nVyZzFapqrt1OnVG8QwzsGdScecUxO_xYzN237tGYRS2NH3aoqoAVTjYjjYPdxbZ_fWo-D0n07mir-9_6D4wofMQ4aFH1Ho1RZup5WP8VxXm-uE12gZ3QroX2nt8";


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
