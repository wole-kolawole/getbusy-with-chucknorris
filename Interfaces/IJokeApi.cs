namespace gwc.Interfaces
{
    public interface IJokeApi
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}