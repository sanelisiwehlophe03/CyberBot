# CyberBot 🛡️

A simple, friendly console chatbot that teaches the basics of cybersecurity awareness. CyberBot greets you by name, plays an optional voice greeting, and answers common questions about staying safe online.

## Features

- **Interactive Q&A** on core cybersecurity topics:
  - Phishing
  - Password security
  - Malware (including ransomware, viruses, spyware)
  - Firewalls
  - VPNs
  - Two-factor authentication (2FA)
- **Personalized experience** — asks for your name and uses it throughout the conversation
- **Voice greeting** on startup (plays `greeting.wav` if present, skips gracefully if not)
- **Typewriter-style console output** with color-coded messages
- **ASCII art banner** and bordered title for a polished console UI
- **Simple commands**: `help`, `exit`, `quit`, `bye`

##Actions 


<img width="637" height="109" alt="image" src="https://github.com/user-attachments/assets/4b1e32a5-c2fa-473b-a546-1aab8043152e" />

## Video Walkthrough

https://advtechonline-my.sharepoint.com/:v:/g/personal/st10500376_rcconnect_edu_za/IQDr0W9i4NKaRK7OqlnteZOVAeVcmKfHycXcE7YpBaUJVQc?e=S1l7IG

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later recommended)
- Windows OS (the audio playback feature uses `System.Media.SoundPlayer`, which is Windows-only)

## Project Structure

```
CyberBot/
├── Program.cs        # Application entry point
├── AudioPlayer.cs     # Handles the optional startup voice greeting
├── Services.cs         # Chatbot class — core conversation logic
├── User.cs              # User model
├── Utils.cs               # Console helper methods (typing effect, colors, ASCII art)
└── greeting.wav        # (Optional) audio file played on startup
```

## Getting Started

1. Clone or download this repository.
2. (Optional) Place a `greeting.wav` file in the application's working directory to enable the voice greeting. If it's missing, CyberBot will simply skip it and continue.
3. Build and run the project:

   ```bash
   dotnet build
   dotnet run
   ```

4. Enter your name when prompted, then start asking cybersecurity questions!

## Example Usage

```
You (Alex) > What is phishing?
Bot: Phishing is an attempt to trick you into revealing sensitive information...

You (Alex) > How does a firewall work?
Bot: A firewall monitors network traffic and applies security rules...

You (Alex) > exit
Bot: Goodbye, Alex! Remember: think before you click.
```

## Available Commands

| Command | Description |
|---|---|
| `help` | Shows the list of topics and example questions |
| `exit` / `quit` / `bye` | Ends the conversation |

## How It Works

- **`Program.cs`** starts the application and hands control to the `Chatbot` service.
- **`Chatbot` (`Services.cs`)** manages the conversation loop: it greets the user, collects their name, and matches user input against known phrases to return relevant cybersecurity guidance.
- **`ConsoleHelper` (`Utils.cs`)** provides reusable console output helpers — typing animation, colored messages, ASCII art, and bordered titles.
- **`AudioPlayer.cs`** plays an optional `.wav` greeting on startup, failing silently (with a notice) if the file isn't found.
- **`User.cs`** is a lightweight model representing the person chatting with the bot.

## Notes

- Input matching is keyword/phrase-based (not true NLP), so CyberBot works best with direct questions like *"What is malware?"* or *"How can I prevent phishing?"*
- If a question isn't recognized, CyberBot will offer a helpful fallback suggestion.

## License

This project is provided as-is for educational purposes.
