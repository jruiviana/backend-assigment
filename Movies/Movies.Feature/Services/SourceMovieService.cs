using Movies.Core.Interfaces;
using Movies.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movies.Feature.Services
{
    public class SourceMovieService : ISourceMovieService
    {
        public async Task<SourceMovieDto> GetMovieAsync(string movieTitle)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://www.omdbapi.com/?t={movieTitle}&i=tt3896198&apikey=e3ec1445"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if(apiResponse.Contains("Movie not found!"))
                    {
                        return default;
                    }

                    return JsonConvert.DeserializeObject<SourceMovieDto>(apiResponse);
                }
            }
        }
    }
}
