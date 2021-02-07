using System.Collections.Generic;

namespace Movies.Core.Interfaces
{
    public interface IMovieSources
    {
        IEnumerable<string> Sources { get; set; }
    }
}
