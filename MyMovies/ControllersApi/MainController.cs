using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyMovies.Models;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace MyMovies.ControllersApi
{
    public class MainController : ApiController
    {
        [HttpPost]
        [ActionName("getAllImages")]
        public HttpResponseMessage GetAllImages([FromBody]ImagesInput imagesInput)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                MyMovieEntities db = new MyMovieEntities();
                var top100Movies = db.Movies.Take(100).ToList();
                db.Configuration.ProxyCreationEnabled = false;
                var requiredtop100Movies = from d in db.Movies.Take(100)
                                           join poster in db.PosterInfoes on d.ID equals poster.MovieId
                                           select new
                                           {
                                               Movie = d,
                                               PosterInfo = poster
                                           };

                //string yourJson1 = JsonConvert.SerializeObject(requiredtop100Movies, Formatting.None,
                //           new JsonSerializerSettings()
                //           {
                //               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //           });

                List<MovieAndPosterInfo> results = new List<MovieAndPosterInfo>();
                foreach (var requiredMovie in requiredtop100Movies)
                {
                    results.Add(new MovieAndPosterInfo()
                    {
                        Movie = requiredMovie.Movie,
                        Poster = requiredMovie.PosterInfo
                    });
                }

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                //string json = jsSerializer.Serialize(results);



                string temp = JsonConvert.SerializeObject(results.First(), Formatting.None,
                             new JsonSerializerSettings()
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });


                string yourJson = JsonConvert.SerializeObject(results, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });

                //var response = new HttpResponseMessage<MovieAndPosterInfo>(yourJson, HttpStatusCode.Created);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                return message;
                Console.WriteLine(ex.StackTrace);
            }
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}