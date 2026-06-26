using System;
using System.IO;
using System.Media;

namespace CybersecurityChatBot
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            string path = GetProjectRootPath();
            string audioPath = Path.Combine(path, "GreetingRecording.wav");

            if (!File.Exists(audioPath))
            {
                Console.WriteLine("Audio file not found: " + audioPath);
                return;
            }

            try
            {
                using (SoundPlayer player = new SoundPlayer(audioPath))
                {
                    player.PlaySync(); // Wait until sound finishes
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio playback error: " + ex.Message);
            }
        }

        private string GetProjectRootPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace(@"\bin\Debug\", "").Replace(@"\bin\Release\", "");
            return path.TrimEnd('\\');
        }
    }
}