using System.Media;


namespace POEPart3
{
    internal static class VoiceGreeting
    {
        public static void Play()
        {
            string filepath = "C:\\Users\\lab_services_student\\Documents\\PROG6211-Folder\\POEPart3\\voiceRecording.wav";

            using (SoundPlayer player = new SoundPlayer(filepath))
            {
                player.PlaySync();
            }
        }
    }
}
