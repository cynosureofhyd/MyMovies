using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMovies.ExternalApis
{
    public class MovieApi
    {
        public static void Get()
        {
            var request = System.Net.WebRequest.Create("http://themoviedb.apiary.io/3/movie/{id}") as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentLength = 0;
            string responseContent = null;
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }
            }
        }
    }
}