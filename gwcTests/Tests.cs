using gwc;
using gwc.Interfaces;

namespace gwcTests
{
    public class Tests
    {

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void NewJokeIsReturned() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";
            string expected = "Chuck Norris can play the violin...on the piano.";

            IJokeProvider jokeProvider = new JokeProvider(new MockJokeApi(jokeJson));

            Assert.That(jokeProvider.GetNewJoke().Result.Value, Is.EqualTo(expected));
        }

        [Test]
        public void NoPreviousJokeIsReturnedWhenNoJokesExist() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";

            IJokeProvider jokeProvider = new JokeProvider(new MockJokeApi(jokeJson));

            Assert.That(jokeProvider.GetPreviousJoke(), Is.EqualTo(null));
        }

        [Test]
        public void NoPreviousJokeIsReturnedWhenOneJokeExists() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";

            IJokeProvider jokeProvider = new JokeProvider(new MockJokeApi(jokeJson));
            jokeProvider.GetNewJoke();

            Assert.That(jokeProvider.GetPreviousJoke(), Is.EqualTo(null));
        }

        [Test]
        public void NoNextJokeIsReturnedWhenNoJokesExist() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";

            IJokeProvider jokeProvider = new JokeProvider(new MockJokeApi(jokeJson));

            Assert.That(jokeProvider.GetNextJoke(), Is.EqualTo(null));
        }

        [Test]
        public void NoNextJokeIsReturnedWhenNewJokeHasJustBeenRetrieved() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";

            IJokeProvider jokeProvider = new JokeProvider(new MockJokeApi(jokeJson));
            jokeProvider.GetNewJoke();

            Assert.That(jokeProvider.GetNextJoke(), Is.EqualTo(null));
        }

        [Test]
        public void ExpectedPreviousJokeIsReturnedWhenTwoJokesExist() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";
            MockJokeApi mockJokeApi = new MockJokeApi(jokeJson);

            IJokeProvider jokeProvider = new JokeProvider(mockJokeApi);
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"dHEMmAtTQfya36jMyy70lg\",\"url\" : \"https://api.chucknorris.io/jokes/dHEMmAtTQfya36jMyy70lg\",\"value\" : \"When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.\"}";
            jokeProvider.GetNewJoke();

            string expected = "Chuck Norris can play the violin...on the piano.";
            Assert.That(jokeProvider.GetPreviousJoke().Value, Is.EqualTo(expected));
        }

        [Test]
        public void ExpectedPreviousJokeIsReturnedWhenMultipleJokesExist() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";
            MockJokeApi mockJokeApi = new MockJokeApi(jokeJson);

            IJokeProvider jokeProvider = new JokeProvider(mockJokeApi);
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"dHEMmAtTQfya36jMyy70lg\",\"url\" : \"https://api.chucknorris.io/jokes/dHEMmAtTQfya36jMyy70lg\",\"value\" : \"When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.\"}";
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"P-tx2WHeTrm2ccB9Hwu9GA\",\"url\" : \"https://api.chucknorris.io/jokes/P-tx2WHeTrm2ccB9Hwu9GA\",\"value\" : \"Chuck Norris made Ferris work on his day off.\"}";
            jokeProvider.GetNewJoke();

            string expected = "When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.";
            Assert.That(jokeProvider.GetPreviousJoke().Value, Is.EqualTo(expected));

            expected = "Chuck Norris can play the violin...on the piano.";
            Assert.That(jokeProvider.GetPreviousJoke().Value, Is.EqualTo(expected));
        }

        [Test]
        public void ExpectedNextJokeIsReturnedWhenTwoJokesExistAndPreviousJokeWasRetrieved() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";
            MockJokeApi mockJokeApi = new MockJokeApi(jokeJson);

            IJokeProvider jokeProvider = new JokeProvider(mockJokeApi);
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"dHEMmAtTQfya36jMyy70lg\",\"url\" : \"https://api.chucknorris.io/jokes/dHEMmAtTQfya36jMyy70lg\",\"value\" : \"When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.\"}";
            jokeProvider.GetNewJoke();

            jokeProvider.GetPreviousJoke();

            string expected = "When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.";
            Assert.That(jokeProvider.GetNextJoke().Value, Is.EqualTo(expected));
        }

        [Test]
        public void ExpectedNextJokeIsReturnedWhenMultipleJokesExistAndPreviousJokeWasRetrieved() {
            string jokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"_MxwlZxARfm1vFPigz9B3A\",\"url\" : \"\",\"value\" : \"Chuck Norris can play the violin...on the piano.\"}";
            MockJokeApi mockJokeApi = new MockJokeApi(jokeJson);

            IJokeProvider jokeProvider = new JokeProvider(mockJokeApi);
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"dHEMmAtTQfya36jMyy70lg\",\"url\" : \"https://api.chucknorris.io/jokes/dHEMmAtTQfya36jMyy70lg\",\"value\" : \"When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.\"}";
            jokeProvider.GetNewJoke();

            mockJokeApi.JokeJson = "{\"icon_url\" : \"https://assets.chucknorris.host/img/avatar/chuck-norris.png\",\"id\" : \"P-tx2WHeTrm2ccB9Hwu9GA\",\"url\" : \"https://api.chucknorris.io/jokes/P-tx2WHeTrm2ccB9Hwu9GA\",\"value\" : \"Chuck Norris made Ferris work on his day off.\"}";
            jokeProvider.GetNewJoke();

            jokeProvider.GetPreviousJoke();
            jokeProvider.GetPreviousJoke();

            string expected = "When life gives Chuck Norris lemons, he makes lemonade...and then goes to sunbathe on the beach.";
            Assert.That(jokeProvider.GetNextJoke().Value, Is.EqualTo(expected));

            expected = "Chuck Norris made Ferris work on his day off.";
            Assert.That(jokeProvider.GetNextJoke().Value, Is.EqualTo(expected));
        }
    }
}