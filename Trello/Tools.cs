using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ServiceHook_Trello.Trello
{
    public static class Tools
    {


        private static string _apiKey = "TRELLO_API_KEY";
        private static string _token = "TRELLO_TOKEN";
        private static string _listID = "TRELLO_LIST_ID";
        public static string CreateCard(string Title,string Description)
        {
            string post = string.Empty;

            string VariaveisPOST = string.Format("name={0}&desc={1}&key={2}&token={3}",Title,Description,_apiKey,_token);
            var vPOST = Encoding.UTF8.GetBytes(VariaveisPOST);
            var request = WebRequest.CreateHttp(string.Format("https://api.trello.com/1/lists/{0}/cards", _listID));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = vPOST.Length;
            request.UserAgent = "TrelloIntegration";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(vPOST, 0, vPOST.Length);
                stream.Close();
            }
            using (var resposta = request.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                post = objResponse.ToString();
                streamDados.Close();
                resposta.Close();
            }

            return post;
        }
    }
}
