using Movies.Core.Models;
using System.Threading.Tasks;

namespace Movies.Core.Interfaces
{
    public interface ISourceMovieService
    {
        Task<SourceMovieDto> GetMovieAsync(string movieTitle);
    }
}
