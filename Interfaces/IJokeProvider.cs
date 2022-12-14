namespace gwc.Interfaces
{
    public interface IJokeProvider
    {
        Task<IJoke> GetNewJoke();
        IJoke GetPreviousJoke();
        IJoke GetNextJoke();
    }
}