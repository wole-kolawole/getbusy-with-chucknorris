using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using gwc.Interfaces;

namespace gwc
{
    public class JokeProvider : IJokeProvider
    {
        private List<IJoke> _jokes = new List<IJoke>();
        private IJokeApi _jokesApi;
        private int _currentJoke = -1;

        public JokeProvider(IJokeApi jokesApi) {
            _jokesApi = jokesApi;
        }

        /// <summary>
        /// Gets a new joke from the Joke API
        /// </summary>
        /// <returns>A <code>Joke</code> object, or <code>Null</code> if a joke could not be retrieved.</returns>
        public async Task<IJoke> GetNewJoke() {
            IJoke? joke = null;
            try {
                HttpResponseMessage response = await _jokesApi.GetAsync("https://api.chucknorris.io/jokes/random");
                joke = await response.Content.ReadFromJsonAsync<Joke>();
            } catch { }

            if (joke != null) {
                _jokes.Add(joke);
                _currentJoke = _jokes.Count - 1;
            }
            return joke!;
        }

        /// <summary>
        /// Gets the previous joke that was retrieved from the Joke API
        /// </summary>
        /// <returns>A <code>Joke</code> object, or <code>Null</code> if no previous joke exists.</returns>
        public IJoke GetPreviousJoke() {
            if (_currentJoke <= 0) {
                return null!;
            }

            return _jokes[--_currentJoke];
        }

        /// <summary>
        /// Gets the next joke that was retrieved from the Joke API
        /// </summary>
        /// <returns>A <code>Joke</code> object, or <code>Null</code> if no next joke exists.</returns>
        public IJoke GetNextJoke() {
            if (_currentJoke >= _jokes.Count - 1) {
                return null!;
            }

            return _jokes[++_currentJoke];
        }
    }
}
