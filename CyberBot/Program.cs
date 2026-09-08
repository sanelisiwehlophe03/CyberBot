using CyberBot.Services;

namespace CyberBot
{
    /// <summary>
    /// Entry point for the Cybersecurity Awareness Chatbot.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Chatbot bot = new Chatbot();
            bot.Start();
        }
    }
}