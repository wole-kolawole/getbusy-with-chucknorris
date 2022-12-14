using gwc.Interfaces;

namespace gwc
{
    public class JokeApi : IJokeApi
    {
        public async Task<HttpResponseMessage> GetAsync(string uri) {
            return await new HttpClient().GetAsync(uri);
        }
    }
}
