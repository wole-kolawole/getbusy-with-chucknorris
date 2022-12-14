using gwc;
using gwc.Interfaces;

string[] instructions = {
    "Welcome to the GetBusy with Chuck Norris \"productivity\" app",
    "",
    "\tEnter one of the following commands:",
    "\t\tj - Retrieve a random Chuck Norris joke",
    "\t\tp - Retrieve the previous joke (if any)",
    "\t\tn - Retrieve the next joke (if any)",
    "\t\tq - Exit this app"
};

Utils.DisplayText(instructions);

string[] validInput = { "j", "p", "n", "q" };
string aCharacter;

IJokeProvider jokeProvider = new JokeProvider(new JokeApi());
IJoke? joke = null;
string jokeType = string.Empty;

do {
    aCharacter = Utils.GetOneCharacter(false);

    if (Utils.IsValidateInput(aCharacter, validInput) == false) {
        Utils.DisplayErrorText("Invalid Input");
        Utils.DisplayErrorText(instructions.TakeLast(instructions.Length - 2).ToArray());
    }
    else {
        if (aCharacter != "q") {
            switch (aCharacter) {
                case "j":
                    Utils.StartSpinner();
                    joke = await jokeProvider.GetNewJoke();
                    Utils.StopSpinner();

                    jokeType = "new";
                    break;
                case "p":
                    joke = jokeProvider.GetPreviousJoke();
                    jokeType = "previous";
                    break;
                case "n":
                    joke = jokeProvider.GetNextJoke();
                    jokeType = "next";
                    break;
            }

            if (joke == null) {
                Utils.DisplayErrorText($"No {jokeType} joke found!");
            }
            else {
                Utils.DisplayText(joke.ToString()!);
            }
        }

    }
}
while (aCharacter != "q");

