using Movies.Core.Interfaces;
using System.Collections.Generic;

namespace Movies.Core.Models
{
    public class MovieSources : IMovieSources
    {
        public IEnumerable<string> Sources { get; set; }
    }
}
