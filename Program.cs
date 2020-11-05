using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmailDadJoke
{
    class Program
    {
        const string emailSenderAdd = "omerdekel92@gmail.com";
 
        /// <summary>
        /// Main program. Asking the email address of the user, then sending him a random joke. 
        /// </summary>
        /// <param name="args"></param>
        /// <returns>1 if the joke was send the the email, else 0</returns>
        static async Task<int> Main(string[] args)
        {
            DadJokeGenerator jokeGenerator = new DadJokeGenerator();
            string joke = await jokeGenerator.GetJokeAsync();
            EmailSend emailSend = new EmailSend();
            // asking the reciever email address
            Console.WriteLine("Please enter your email address");
            string userEmail = Console.ReadLine();
            int emailSentCode = await emailSend.SendMailAsync(joke,emailSenderAdd, userEmail);
            return emailSentCode;
        }
    }
}
