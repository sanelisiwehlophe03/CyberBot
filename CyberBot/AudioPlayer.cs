using System;
using System.Media;

namespace CyberBot.Services
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string audioPath = "greeting.wav";

                using SoundPlayer player = new SoundPlayer("greeting.wav");

                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio error: {ex.Message}");
            }
        }
    }
}