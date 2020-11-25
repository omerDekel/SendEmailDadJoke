using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmailDadJoke
{
    /// <summary>
    /// Sending email class using sendGrid API
    /// </summary>
    class EmailSend
    {
        // consts
        const string accessToken = "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";
        const string sendGridUrl = "https://api.sendgrid.com/v3/mail/send";
        //member
        private HttpClient httpClient;
        public EmailSend()
        {
            this.httpClient = new HttpClient();
            //Authorization Header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Creating a body for the post request to sendGrid api
        /// </summary>
        /// <param name="joke">joke to email content</param>
        /// <param name="reciever">email address of the reciever</param>
        /// <returns>string of the body  post request</returns>
        private string createSendGridBodyReq(string joke, string reciever,string sender)
        {
            string payload = JsonConvert.SerializeObject(new
            {
                from = new
                {
                    email = sender,
                },
                subject = "Dad Joke",
                personalizations = new JArray
                {
                    new JObject
                    {
                        new JProperty
                        ("to",new JArray
                        {
                            new JObject
                            {
                                new JProperty("email",reciever)
                            }

                        }

                        )
                    }

                },
                content = new JArray
                {
                    new JObject
                    {
                        new JProperty("type","text/plain"),
                        new JProperty("value",joke)
                    }
                  }
            });
            return payload;
        }
        /// <summary>
        /// sending post request to send the mail
        /// </summary>
        /// <param name="jsonStr">body of the request json represented in string</param>
        /// <param name="url">url for the request </param>
        /// <param name="httpClient">the client to send the request with</param>
        /// <returns>the data from the requrst</returns>
         private async Task<int> POSTData(string jsonStr, string url)
        {
            using (var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = httpClient.PostAsync(url, content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    return 1;
                string returnValue = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                return 0;
            }
        }
       /// <summary>
       /// Sending an email message from a sender address to reciever address
       /// </summary>
       /// <param name="message"></param>
       /// <param name="senderAddress"></param>
       /// <param name="recieverAddress"></param>
       /// <returns></returns>
        public async Task<int> SendMailAsync(string message,string senderAddress,string recieverAddress)
        {
            string payload = createSendGridBodyReq(message, recieverAddress, senderAddress);
            int ret = await POSTData(payload, sendGridUrl);
            return ret;
        }

    }
}
