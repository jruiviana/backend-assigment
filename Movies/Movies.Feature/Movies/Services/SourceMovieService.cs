using Movies.Core.Interfaces;
using Movies.Core.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Movies.Feature.Services
{
    public class SourceMovieService : ISourceMovieService
    {
        private readonly IHttpHandler _httpHandler;
        private readonly IMovieSources _sources;

        public SourceMovieService(IHttpHandler httpHandler, IMovieSources sources)
        {
            _httpHandler = httpHandler;
            _sources = sources;
        }

        public async Task<SourceMovieDto> GetMovieAsync(string movieTitle)
        {
            foreach (var source in _sources.Sources)
            {
                var result = await GetMovieFromSourceAsync(string.Format(source, movieTitle));
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private async Task<SourceMovieDto> GetMovieFromSourceAsync(string source)
        {
            using (var response = await _httpHandler.GetAsync(source))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (apiResponse.Contains("Movie not found!"))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<SourceMovieDto>(apiResponse);
            }
        }
    }
}
