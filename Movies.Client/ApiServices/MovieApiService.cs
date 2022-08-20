using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {

            // 1. "retrieve" our api credentials. This must be registered on Identity Server!

            var client = new HttpClient();
            // just checks if we can reach the Discovery document. Not 100% needed but..
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (disco.IsError)
            {
                return null; // throw 500 error
            }

            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "movieClient",
                ClientSecret = "secret",
                Scope = "movieAPI"
            };

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError)
            {
                return null;
            }

            // Another HttpClient for talking now with our Protected API
            var apiClient = new HttpClient();

            // 3. Set the access_token in the request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // 4. Send a request to our Protected API
            var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movieList;

            //var movieList = new List<Movie>();
            //movieList.Add(new Movie{
            //    Id = 1,
            //    Genre = "Comics",
            //    Title = "asd",
            //    Rating = "9.2",
            //    ImageUrl = "images/src",
            //    ReleaseDate = DateTime.Now,
            //    Owner = "testuser"
            //});

            //return await Task.FromResult(movieList);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
