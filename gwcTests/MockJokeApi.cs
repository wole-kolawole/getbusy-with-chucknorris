using gwc.Interfaces;
using System.Net;

namespace gwc
{
    public class MockJokeApi : IJokeApi {
        public MockJokeApi(string jokeJson) {
            JokeJson = jokeJson;
        }

        public string JokeJson { get; set; }

        public async Task<HttpResponseMessage> GetAsync(string uri) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) {
                Content = new StringContent(JokeJson, System.Text.Encoding.UTF8, "application/json") };

            return response;
        }
    }
}
