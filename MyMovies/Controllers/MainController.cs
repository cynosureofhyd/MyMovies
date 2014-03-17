using MyMovies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyMovies.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            MyMovieEntities db = new MyMovieEntities();

            db.Configuration.ProxyCreationEnabled = false;

            var top100Movies = db.Movies.Take(100).ToList();

            var requiredtop100Movies = from d in db.Movies.Take(100)
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

            var serializer = new JavaScriptSerializer();
            var test = serializer.Serialize(results);

            return Json(results, JsonRequestBehavior.AllowGet);
        }
	}
}