using gwc.Interfaces;

namespace gwc
{
    internal static class Utils {
        const string _spinnerSequence = @"/-\|";
        static bool _spinnerRunning = false;
        static bool _stopSpinner = false;
        static Thread? _spinnerThread;

        /// <summary>
        /// Writes the supplied text to the Console. Future work can extend this to write to a Stream of some sort.
        /// </summary>
        public static void WriteText(params string[] textToWrite) {
            Console.WriteLine();
            foreach (string text in textToWrite) {
                Console.WriteLine(text);
            }
        }

        /// <summary>
        /// Reads a sngle character from the Console. Future work can extend this to read from a Stream of some sort.
        /// </summary>
        /// <param name="allowModifiers">Whether or not to accept a character when pressed along with Shift, Ctrl or Alt</param>
        /// <returns>A single character read from the console</returns>
        public static string GetOneCharacter(bool acceptModifiers) {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            string keyPressed = keyInfo.KeyChar.ToString();
            if (acceptModifiers == false
                && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift | ConsoleModifiers.Control | ConsoleModifiers.Alt)) {
                keyPressed = string.Empty;
            }

            return keyPressed;
        }

        /// <summary>
        /// Checks that <paramref name="input"/> is a valid character
        /// </summary>
        /// <param name="input">The string to check</param>
        /// <returns>True if <paramref name="input"/> is a valid character, false otherwise</returns>
        public static bool IsValidateInput(string input, string[] validInput) {
            return validInput.Contains(input.ToLower());
        }

        /// <summary>
        /// Starts a Console spinner on a separate thread
        /// </summary>
        public static void StartSpinner() {
            if (_spinnerRunning) {
                return;
            }

            _spinnerRunning = true;
            _spinnerThread = new Thread(Spinner);
            _spinnerThread.Start();
        }

        /// <summary>
        /// Writes the spinner to the console until asked to stop
        /// </summary>
        private static void Spinner() {
            int i = 0;
            Console.CursorVisible = false;
            Console.Write(" ");
            while (_stopSpinner == false) {
                Console.Write(_spinnerSequence[++i % _spinnerSequence.Length]);
                if (_stopSpinner == false) {
                    // Note: This is to stop the cursor position from being updated if StopSpinner() has already been called.
                    //       It doesn't always work and sometimes leaves a minor cursor artifact on the line where the command key
                    //       character was entered.
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
                Thread.Sleep(100);
            }
            Console.CursorVisible = true;

            _spinnerRunning = false;
        }

        /// <summary>
        /// Stops the spinner
        /// </summary>
        public static void StopSpinner() {
            _stopSpinner = true;
        }
    }
}
