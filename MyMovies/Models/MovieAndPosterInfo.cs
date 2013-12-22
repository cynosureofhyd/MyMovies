using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMovies.Models
{
    public class MovieAndPosterInfo
    {
        public Movie Movie { get; set; } // TODO : Change this to movie model later on

        public PosterInfo Poster { get; set; } // TODO: Change this to postermodel later on
    }
}