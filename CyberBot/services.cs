using System;
using System.Collections.Generic;
using CyberBot.Models;
using CyberBot.Utils;

namespace CyberBot.Services
{
    /// <summary>
    /// Main cybersecurity chatbot.
    /// Handles user input and generates appropriate responses.
    /// </summary>
    public class Chatbot
    {
        private User? _user;
        private bool _running = true;

        /// <summary>
        /// Starts the chatbot application.
        /// </summary>
        public void Start()
        {
            Console.Title = "CyberBot - Cybersecurity Awareness";

            Console.Clear();

            ConsoleHelper.ShowAsciiArt();
            ConsoleHelper.ShowBorderTitle("CYBERSECURITY AWARENESS CHATBOT");

            ConsoleHelper.ShowMessage(
                "Bot",
                "Hello! Welcome to CyberBot. I'm here to help you become safer online.",
                ConsoleColor.Cyan);

            AudioPlayer.PlayGreeting();

            AskName();
            ShowHelp();

            while (_running)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\nYou ({_user!.Name}) > ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                HandleInput(input);
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nThank you for using CyberBot!");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the user for their name.
        /// </summary>
        private void AskName()
        {
            string? name;

            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nPlease enter your name: ");
                Console.ResetColor();

                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid name.");
                    Console.ResetColor();
                }

            } while (string.IsNullOrWhiteSpace(name));

            _user = new User(name.Trim());

            ConsoleHelper.ShowMessage(
                "Bot",
                $"Nice to meet you, {_user.Name}! How can I help you today?",
                ConsoleColor.Cyan);
        }

        /// <summary>
        /// Displays available commands and topics.
        /// </summary>
        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("\nYou can ask me about:");

            Console.WriteLine("  • Phishing");
            Console.WriteLine("  • Password security");
            Console.WriteLine("  • Malware");
            Console.WriteLine("  • Firewalls");
            Console.WriteLine("  • VPNs");
            Console.WriteLine("  • Two-factor authentication");

            Console.WriteLine("\nExample questions:");

            Console.WriteLine("  • What is phishing?");
            Console.WriteLine("  • How can I protect myself from phishing?");
            Console.WriteLine("  • What makes a password strong?");
            Console.WriteLine("  • What is malware?");
            Console.WriteLine("  • How does a firewall work?");
            Console.WriteLine("  • What is a VPN?");
            Console.WriteLine("  • Why should I use 2FA?");


            Console.WriteLine("\nCommands:");
            Console.WriteLine("  • help");
            Console.WriteLine("  • exit");
            Console.WriteLine("  • quit");
            Console.WriteLine("  • bye");

            Console.ResetColor();
        }

        /// <summary>
        /// Processes user input.
        /// </summary>
        private void HandleInput(string? rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                ConsoleHelper.ShowMessage(
                    "Bot",
                    "I didn't receive anything. Please type a question.",
                    ConsoleColor.Cyan);

                return;
            }

            string input = NormaliseInput(rawInput);

            string response = GetResponse(input);

            ConsoleHelper.ShowMessage(
                "Bot",
                response,
                ConsoleColor.Cyan);
        }

        /// <summary>
        /// Normalises user input.
        /// </summary>
        private string NormaliseInput(string input)
        {
            input = input.Trim().ToLowerInvariant();

            char[] punctuation =
            {
                '.', ',', '!', '?', ':', ';',
                '"', '\'', '(', ')'
            };

            foreach (char character in punctuation)
            {
                input = input.Replace(character, ' ');
            }

            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }

            return input.Trim();
        }

        /// <summary>
        /// Determines the correct response to the user's question.
        /// </summary>
        private string GetResponse(string input)
        {
            // ==========================================
            // EXIT
            // ==========================================

            if (IsExactMatch(input, "exit", "quit", "bye"))
            {
                _running = false;

                return $"Goodbye, {_user?.Name}! Remember: think before you click.";
            }

            // ==========================================
            // GREETINGS
            // ==========================================

            if (IsGreeting(input))
            {
                return "Hello! I'm CyberBot. What cybersecurity question can I help you with?";
            }

            // ==========================================
            // HOW ARE YOU
            // ==========================================

            if (ContainsPhrase(
                input,
                "how are you",
                "how are u",
                "how do you feel"))
            {
                return "I'm doing great! I'm ready to help you learn about cybersecurity.";
            }

            // ==========================================
            // THANK YOU
            // ==========================================

            if (ContainsPhrase(
                input,
                "thank you",
                "thanks",
                "thank"))
            {
                return "You're welcome! Feel free to ask me another cybersecurity question.";
            }

            // ==========================================
            // IDENTITY
            // ==========================================

            if (ContainsPhrase(
                input,
                "your name",
                "who are you",
                "what are you"))
            {
                return "I'm CyberBot, a cybersecurity awareness assistant. I can explain common cybersecurity threats and help you develop safer online habits.";
            }

            // ==========================================
            // PURPOSE
            // ==========================================

            if (ContainsPhrase(
                input,
                "your purpose",
                "what is your purpose",
                "what can you do",
                "what do you do"))
            {
                return "My purpose is to help you understand cybersecurity concepts such as phishing, passwords, malware, firewalls, VPNs and two-factor authentication.";
            }

            // ==========================================
            // HELP
            // ==========================================

            if (IsExactMatch(input, "help") ||
                ContainsPhrase(input, "what can i ask"))
            {
                ShowHelp();

                return "Those are the main topics I can help you with. Ask me a specific cybersecurity question.";
            }

            // ==========================================
            // PHISHING
            // ==========================================

            if (ContainsPhrase(
                input,
                "phishing",
                "phishing attack",
                "phishing email",
                "phishing emails"))
            {
                return GetPhishingResponse(input);
            }

            // ==========================================
            // PASSWORDS
            // ==========================================

            if (ContainsPhrase(
                input,
                "password",
                "passwords",
                "strong password",
                "password security",
                "secure password"))
            {
                return GetPasswordResponse(input);
            }

            // ==========================================
            // MALWARE
            // ==========================================

            if (ContainsPhrase(
                input,
                "malware",
                "computer virus",
                "virus",
                "ransomware",
                "spyware"))
            {
                return GetMalwareResponse(input);
            }

            // ==========================================
            // FIREWALL
            // ==========================================

            if (ContainsPhrase(
                input,
                "firewall",
                "firewalls"))
            {
                return GetFirewallResponse(input);
            }

            // ==========================================
            // VPN
            // ==========================================

            if (ContainsPhrase(
                input,
                "vpn",
                "vpns",
                "virtual private network"))
            {
                return GetVpnResponse(input);
            }

            // ==========================================
            // TWO FACTOR AUTHENTICATION
            // ==========================================

            if (ContainsPhrase(
                input,
                "2fa",
                "two factor",
                "two factor authentication",
                "multi factor",
                "multi factor authentication",
                "multifactor",
                "mfa"))
            {
                return Get2FAResponse(input);
            }

            // ==========================================
            // DEFAULT
            // ==========================================

            return GetDefaultResponse(input);
        }

        // =========================================================
        // PHISHING RESPONSES
        // =========================================================

        private string GetPhishingResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "how can i prevent",
                "how do i prevent",
                "how can i avoid",
                "how do i avoid",
                "protect myself",
                "stay safe",
                "stop phishing"))
            {
                return "To protect yourself from phishing, don't click unexpected links or attachments, check the sender carefully, avoid entering passwords through links in suspicious messages, and verify urgent requests using a trusted method.";
            }

            if (ContainsPhrase(
                input,
                "example",
                "examples",
                "look like",
                "signs",
                "warning signs"))
            {
                return "Common phishing signs include unexpected messages, urgent requests, suspicious links, requests for passwords or payment information, unusual sender addresses and messages containing unexpected attachments.";
            }

            if (ContainsPhrase(
                input,
                "clicked",
                "click a link",
                "clicked a link",
                "opened a phishing"))
            {
                return "If you clicked a suspicious phishing link, don't enter any information. Close the page, run a security scan, change any password you may have entered, enable multi-factor authentication and report the message to the appropriate organisation.";
            }

            if (ContainsPhrase(
                input,
                "what is",
                "what are",
                "define",
                "meaning",
                "explain"))
            {
                return "Phishing is a cyberattack where criminals pretend to be a trusted person or organisation to trick you into revealing information, clicking malicious links, opening attachments or sending money.";
            }

            return "Phishing is a cyberattack that uses deceptive messages to trick people into revealing information or performing unsafe actions. If you want, ask me how to prevent phishing or how to recognise a phishing message.";
        }

        // =========================================================
        // PASSWORD RESPONSES
        // =========================================================

        private string GetPasswordResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "how can i make",
                "how do i make",
                "create a strong",
                "strong password",
                "secure password"))
            {
                return "Create a long, unique password or passphrase for each account. Avoid names, birthdays and common words. A password manager can help you generate and store unique passwords.";
            }

            if (ContainsPhrase(
                input,
                "password manager",
                "manage passwords"))
            {
                return "A password manager securely stores your passwords and can generate strong, unique passwords for different accounts. This reduces the need to reuse passwords.";
            }

            if (ContainsPhrase(
                input,
                "reuse",
                "same password",
                "different accounts"))
            {
                return "You should avoid reusing passwords. If one account is compromised, attackers may try the same password on your other accounts.";
            }

            if (ContainsPhrase(
                input,
                "what is",
                "what makes",
                "define",
                "explain"))
            {
                return "A strong password is long, unique and difficult to guess. It should not contain easily available personal information and should ideally be different for every account.";
            }

            return "Use long, unique passwords for every account and consider using a password manager. Ask me about password managers, password reuse or creating a strong password.";
        }

        // =========================================================
        // MALWARE RESPONSES
        // =========================================================

        private string GetMalwareResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "how can i prevent",
                "how do i prevent",
                "protect against",
                "avoid malware"))
            {
                return "To reduce malware risk, keep your operating system and applications updated, use reputable security software, avoid suspicious downloads and attachments, and only install software from trusted sources.";
            }

            if (ContainsPhrase(
                input,
                "ransomware"))
            {
                return "Ransomware is malware that can encrypt files or systems and demand payment from victims. Regular offline or otherwise protected backups, software updates and careful handling of suspicious files can reduce the risk.";
            }

            if (ContainsPhrase(
                input,
                "virus"))
            {
                return "A computer virus is a type of malicious software that can reproduce or spread by attaching itself to files or programs. Viruses are one category of malware.";
            }

            if (ContainsPhrase(
                input,
                "spyware"))
            {
                return "Spyware is malicious software designed to secretly monitor activity or collect information from a device.";
            }

            if (ContainsPhrase(
                input,
                "what is",
                "what are",
                "define",
                "meaning",
                "explain"))
            {
                return "Malware means malicious software. It is designed to damage systems, steal information, disrupt operations or gain unauthorised access. Examples include viruses, ransomware and spyware.";
            }

            return "Malware is malicious software that can damage systems, steal information or provide attackers with unauthorised access. Ask me about viruses, ransomware or spyware for more information.";
        }

        // =========================================================
        // FIREWALL RESPONSES
        // =========================================================

        private string GetFirewallResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "how does",
                "how do",
                "work"))
            {
                return "A firewall monitors network traffic and applies security rules to decide which connections should be allowed or blocked. It acts as an important layer of defence between trusted and untrusted networks.";
            }

            if (ContainsPhrase(
                input,
                "why do i need",
                "why use",
                "important"))
            {
                return "A firewall can help prevent unauthorised network connections and reduce exposure to certain network-based threats. It is one layer of security and should be combined with updates, strong passwords and other protections.";
            }

            if (ContainsPhrase(
                input,
                "what is",
                "what are",
                "define",
                "explain"))
            {
                return "A firewall is a security system that monitors and controls network traffic based on predefined rules. It can allow legitimate connections and block unauthorised or suspicious traffic.";
            }

            return "A firewall monitors network traffic and can block unauthorised connections. Ask me how a firewall works if you want a more detailed explanation.";
        }

        // =========================================================
        // VPN RESPONSES
        // =========================================================

        private string GetVpnResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "what is",
                "define",
                "meaning",
                "explain"))
            {
                return "A VPN, or Virtual Private Network, creates an encrypted connection between your device and a VPN server. It can help protect your traffic from local network observers, especially on untrusted networks.";
            }

            if (ContainsPhrase(
                input,
                "public wifi",
                "public wi fi",
                "public network"))
            {
                return "A reputable VPN can provide additional protection when using public Wi-Fi by encrypting traffic between your device and the VPN server. You should still use HTTPS and follow normal security practices.";
            }

            if (ContainsPhrase(
                input,
                "anonymous",
                "completely private",
                "hide me"))
            {
                return "A VPN can improve privacy, but it does not make you completely anonymous online. Websites, services and other parties may still collect information about your activity.";
            }

            return "A VPN creates an encrypted connection between your device and a VPN server. It can improve privacy and provide additional protection on public networks.";
        }

        // =========================================================
        // 2FA RESPONSES
        // =========================================================

        private string Get2FAResponse(string input)
        {
            if (ContainsPhrase(
                input,
                "why",
                "important",
                "should i use",
                "should i enable"))
            {
                return "Two-factor authentication adds an additional verification step after your password. This makes it harder for someone to access your account using a stolen password alone.";
            }

            if (ContainsPhrase(
                input,
                "how does",
                "how do",
                "work"))
            {
                return "Two-factor authentication requires two different types of verification, such as something you know like a password and something you have like an authentication device or app.";
            }

            if (ContainsPhrase(
                input,
                "what is",
                "what are",
                "define",
                "explain"))
            {
                return "Two-factor authentication, or 2FA, is a security method that requires an additional verification step beyond your password. It provides an extra layer of protection for your accounts.";
            }

            return "2FA adds an additional verification step after your password, making your account harder to compromise if your password is stolen.";
        }

        // =========================================================
        // DEFAULT RESPONSE
        // =========================================================

        private string GetDefaultResponse(string input)
        {
            if (input.Contains("can you"))
            {
                return "Yes, I can help with cybersecurity awareness topics. Try asking me a specific question about phishing, passwords, malware, firewalls, VPNs or 2FA.";
            }

            if (input.Contains("what"))
            {
                return "I want to make sure I give you a useful answer. Try asking your question with a cybersecurity topic, for example: 'What is phishing?' or 'How can I protect my passwords?'";
            }

            if (input.Contains("how"))
            {
                return "I can help explain cybersecurity topics step by step. Try asking something like 'How can I prevent phishing?' or 'How does a firewall work?'";
            }

            return "I'm not sure I understood your question. I can help with phishing, passwords, malware, firewalls, VPNs and two-factor authentication. Try asking a specific question.";
        }

        // =========================================================
        // HELPER METHODS
        // =========================================================

        /// <summary>
        /// Checks whether the input exactly matches one of the supplied phrases.
        /// </summary>
        private bool IsExactMatch(
            string input,
            params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (input.Equals(
                    phrase,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether the input contains one of the supplied phrases.
        /// </summary>
        private bool ContainsPhrase(
            string input,
            params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (input.Contains(
                    phrase,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Detects common greetings.
        /// </summary>
        private bool IsGreeting(string input)
        {
            return IsExactMatch(
                input,
                "hello",
                "hi",
                "hey",
                "good morning",
                "good afternoon",
                "good evening")
                ||
                input.StartsWith("hello ", StringComparison.OrdinalIgnoreCase)
                ||
                input.StartsWith("hi ", StringComparison.OrdinalIgnoreCase)
                ||
                input.StartsWith("hey ", StringComparison.OrdinalIgnoreCase);
        }
    }
}