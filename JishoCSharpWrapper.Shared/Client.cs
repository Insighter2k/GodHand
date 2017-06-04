using System;
using System.Net;
using JishoCSharpWrapper.Shared.Models;
using JishoCSharpWrapper.Shared.Models.API;

namespace JishoCSharpWrapper.Shared
{
    public class Client
    {
        public static Message<RootObject> RequestValuesFromJisho(string value, bool autoLatinToKana)
        {
            Message<RootObject> message = new Message<RootObject>();

            try
            {
                if (!autoLatinToKana) value = $"\"{value}\"";

                var url = $"http://jisho.org/api/v1/search/words?keyword={value}";

                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

                webClient.Encoding = System.Text.Encoding.UTF8;

                string result = webClient.DownloadString(url);
                message = IO.Convert.JsonToObject(result);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return message;

        }
    }
}
