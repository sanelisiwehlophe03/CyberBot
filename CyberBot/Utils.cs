using System;
using System.Threading;

namespace CyberBot.Utils
{
    /// <summary>
    /// Provides reusable methods for displaying information in the console.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Displays text one character at a time.
        /// </summary>
        public static void TypeEffect(
            string text,
            int delayMs = 10,
            ConsoleColor color = ConsoleColor.DarkYellow)
        {
            Console.ForegroundColor = color;

            foreach (char character in text)
            {
                Console.Write(character);

                if (delayMs > 0)
                {
                    Thread.Sleep(delayMs);
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Displays a message showing who is speaking.
        /// </summary>
        public static void ShowMessage(
            string speaker,
            string message,
            ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"{speaker}: ");
            Console.ResetColor();

            TypeEffect(message + Environment.NewLine, 8, ConsoleColor.Gray);
        }

        /// <summary>
        /// Displays the chatbot's ASCII logo.
        /// </summary>
        public static void ShowAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@"
   _____      _               ____        _
  / ____|    | |             |  _ \      | |
 | |    _   _| |__   ___ _ __| |_) | ___ | |_
 | |   | | | | '_ \ / _ \ '__|  _ < / _ \| __|
 | |___| |_| | |_) |  __/ |  | |_) | (_) | |_
  \_____\__, |_.__/ \___|_|  |____/ \___/ \__|
         __/ |
        |___/
");

            Console.ResetColor();
        }

        /// <summary>
        /// Displays a bordered title.
        /// </summary>
        public static void ShowBorderTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            string border = new string('=', title.Length + 8);

            Console.WriteLine(border);
            Console.WriteLine($"    {title}");
            Console.WriteLine(border);

            Console.ResetColor();
        }
    }
}