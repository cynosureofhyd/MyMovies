using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyMovies.Models;

namespace MyMovies.ControllersApi
{
    public class MainController : ApiController
    {
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpGet]
        //[ActionName("mymovies")]
        //public IEnumerable<object> Get(int? id)
        //{
        //    MovieEntities db = new MovieEntities();
        //    var result = (from m in db.Movies
        //                  join p in db.PosterInfoes
        //                     on m.ID equals p.MovieId
        //                  select new
        //                  {
        //                      m.Title,
        //                      p.Imdb,
        //                      p.Cover,
        //                      p.LocalPath
        //                  }).Take(100);
        //    Image t = Test(result.First().Imdb);
        //    // Save the image as a JPEG.
        //    t.Save("c:\\button.jpeg", ImageFormat.Jpeg);
        //    dynamic finalresult = new ExpandoObject();
        //    finalresult = result.First();
        //    byte[] temp = GetBytes(result.First().LocalPath);
        //    finalresult.LocalPath = ByteArrayToImage.byteArrayToImage(temp);
        //    return finalresult;
        //}

        [HttpPost]
        [ActionName("getAllImages")]
        public object GetAllImages([FromBody]ImagesInput imagesInput)
        {

            //var test = Get();
            //return test;
            MovieEntities db = new MovieEntities();
            var top100Movies = db.Movies.Take(100).ToList();
            var requiredtop100Movies = from d in db.Movies.Take(10)
                                       join poster in db.PosterInfoes on d.ID equals poster.MovieId
                                       select new
                                       {
                                           Movie = d,
                                           PosterInfo = poster
                                       };
            List<MovieAndPosterInfo> results = new List<MovieAndPosterInfo>();
            foreach (var requiredMovie in requiredtop100Movies)
            {
                results.Add(new MovieAndPosterInfo()
                {
                    Movie = requiredMovie.Movie,
                    Poster = requiredMovie.PosterInfo
                });
            }
            return Json<List<MovieAndPosterInfo>>(results);
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}