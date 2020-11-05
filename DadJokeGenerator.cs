using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmailDadJoke
{
    /// <summary>
    /// Genearting dad joke class using https://icanhazdadjoke.com/ API
    /// </summary>
    class DadJokeGenerator
    {
        const string dadJokedUrl = "https://icanhazdadjoke.com/";
        //member
        private HttpClient httpClient;
        public DadJokeGenerator()
        {
            this.httpClient = new HttpClient(); 
            //ACCEPT header
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        /// <summary>
        /// get a random joke with get request to the API
        /// </summary>
        /// <returns>string joke </returns>
        public async Task<string> GetJokeAsync()
        {
            using (var response = await httpClient.GetStreamAsync(dadJokedUrl))
                {
                    using (var sr = new StreamReader(response))
                    {
                    string joke = sr.ReadToEnd();
                    return joke;
                    }
                }
            
        }
    }
}
