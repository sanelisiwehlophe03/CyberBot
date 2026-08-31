using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CyberBot
{
    // ---------------- USER ----------------
    public class User
    {
        public string Name { get; private set; }

        public User(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Friend" : name.Trim();
        }
    }

    // ---------------- AUDIO PLAYER ----------------
    public static class AudioPlayer
    {
        public static void PlayGreeting(string fileName = "greeting.wav")
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[Notice] '{fileName}' not found — skipping voice greeting.");
                    Console.ResetColor();
                    return;
                }

                using SoundPlayer player = new SoundPlayer(fileName);
                player.PlaySync(); // Blocks until playback finishes
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing audio: {e.Message}");
                Console.ResetColor();
            }
        }
    }

    // ---------------- CHATBOT ----------------
    public class Chatbot
    {
        private User? _user;
        private bool _running = true;

        public void Start()
        {
            AudioPlayer.PlayGreeting();

            ShowAsciiArt();
            ShowBorderTitle("CYBERSECURITY CHATBOT");

            TypeEffect("Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.\n");

            AskName();
            ShowHelp();

            while (_running)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"\n{_user!.Name} > ");
                Console.ResetColor();

                string? rawInput = Console.ReadLine();
                HandleInput(rawInput);
            }
        }

        // STEP 3: USER INTERACTION
        private void AskName()
        {
            string? name;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Please enter your name: ");
                Console.ResetColor();
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter something so I know what to call you.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(name));

            _user = new User(name);
            TypeEffect($"\nHello {_user.Name}! Welcome to the CyberSecurity Bot,how can i help you but......\n");
        }

        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("You can ask me things like:");
            Console.WriteLine("  - \"How are you?\"");
            Console.WriteLine("  - \"What is your purpose?\"");
            Console.WriteLine("  - \"What is phishing / a password / malware / a firewall / a VPN / 2FA?\"");
            Console.WriteLine("  - Type \"exit\" or \"quit\" to leave.");
            Console.ResetColor();
        }

        // STEP 5: INPUT VALIDATION
        private void HandleInput(string? rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter something.");
                Console.ResetColor();
                return;
            }

            string input = rawInput.Trim().ToLower();
            string response = GetResponse(input);
            TypeEffect(response);
        }

        // STEP 4: BASIC RESPONSE SYSTEM
        private string GetResponse(string input)
        {
            if (input is "exit" or "quit" or "bye")
            {
                _running = false;
                return $"Goodbye, {_user!.Name}! Stay safe online.\n";
            }

            if (input.Contains("how are you"))
                return "I'm just code, but I'm here to help you!\n";

            if (input.Contains("your purpose") || input.Contains("purpose of this chat") || input.Contains("what can you do"))
                return "My purpose is to raise cybersecurity awareness and answer your questions about staying safe online.\n";

            if (input.Contains("your name") || input.Contains("who are you"))
                return "I'm CyberBot, your friendly cybersecurity awareness assistant.\n";

            if (input.Contains("phishing"))
                return "Phishing is when attackers send fake emails or messages pretending to be trustworthy sources " +
                       "to trick you into revealing sensitive information. Never click suspicious links, and always verify the sender.\n";

            if (input.Contains("password"))
                return "Use strong, unique passwords with a mix of letters, numbers and symbols. " +
                       "Consider a password manager, and enable two-factor authentication wherever possible.\n";

            if (input.Contains("malware") || input.Contains("virus"))
                return "Malware is malicious software designed to damage or gain unauthorized access to a system. " +
                       "Keep your antivirus updated and avoid downloading files from untrusted sources.\n";

            if (input.Contains("firewall"))
                return "A firewall monitors and filters incoming and outgoing network traffic, acting as a barrier between a trusted " +
                       "network and untrusted ones like the internet.\n";

            if (input.Contains("vpn"))
                return "A VPN (Virtual Private Network) encrypts your internet connection, helping protect your data and privacy, " +
                       "especially on public Wi-Fi.\n";

            if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("two factor"))
                return "Two-Factor Authentication (2FA) adds an extra layer of security by requiring a second verification step, " +
                       "like a code sent to your phone, in addition to your password.\n";

            if (input.Contains("social engineering"))
                return "Social engineering is manipulating people into giving up confidential information, often by impersonating " +
                       "someone trustworthy. Always verify identities before sharing sensitive details.\n";

            if (input.Contains("help"))
            {
                ShowHelp();
                return "\n";
            }

            return "I didn't quite understand that. Try asking about phishing, passwords, malware, firewalls, VPNs, or 2FA.\n";
        }

        // STEP 2: ASCII ART
        private void ShowAsciiArt()
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

        // STEP 6: ENHANCED CONSOLE UI
        private void ShowBorderTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string border = new string('=', title.Length + 8);
            Console.WriteLine(border);
            Console.WriteLine($"    {title}");
            Console.WriteLine(border);
            Console.ResetColor();
        }

        private void TypeEffect(string text, int delayMs = 15)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.ResetColor();
        }
    }

    // ---------------- ENTRY POINT ----------------
    class Program
    {
        static void Main(string[] args)
        {
            Chatbot bot = new Chatbot();
            bot.Start();
        }
    }
}