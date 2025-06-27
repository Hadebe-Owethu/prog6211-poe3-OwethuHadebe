using System;
using System.Collections.Generic;

namespace POEPart3
{
    internal static class ResponseManager
    {
        private static readonly Random rnd = new Random();
        private static readonly Dictionary<string, int> lastUsedTipIndex = new Dictionary<string, int>();

        public static Action<string> SendToChat { get; set; }

        private static string GetPersonalizedResponse(string message)
        {
            return message; // No auto-personalization unless you explicitly include it
        }

        private static int GetNextTipIndex(string topic, int totalTips)
        {
            if (!lastUsedTipIndex.ContainsKey(topic))
                lastUsedTipIndex[topic] = -1;

            int currentIndex = lastUsedTipIndex[topic];
            int nextIndex = (currentIndex + 1 + rnd.Next(1, totalTips)) % totalTips;

            lastUsedTipIndex[topic] = nextIndex;
            return nextIndex;
        }

        private static void SendRandomTip(string topic, List<string> tips)
        {
            int index = GetNextTipIndex(topic, tips.Count);
            SendToChat?.Invoke(GetPersonalizedResponse(tips[index]));
        }

        public static void HandlePhishingResponse()
        {
            var tips = new List<string>
            {
                "Phishing often disguises itself as official communication to trick you into clicking links or revealing information.",
                "Always double-check sender addresses and URLs before responding.",
                "When in doubt, contact the company directly rather than using a link from the message.",
                "Many phishing emails use urgency like 'your account will be closed' to scare you into acting fast."
            };

            SendRandomTip("phishing", tips);
        }

        public static void HandlePasswordResponse()
        {
            var tips = new List<string>
            {
                "Use a password manager to create and store secure, unique passwords.",
                "Avoid using names, birthdays, or common phrases in your passwords.",
                "Consider passphrases—strings of random words—for both strength and memorability.",
                "Change passwords immediately after a known breach or suspicious activity."
            };

            SendRandomTip("password", tips);
        }

        public static void HandleSafeBrowsingResponse()
        {
            var tips = new List<string>
            {
                "Install browser extensions that alert you to malicious websites.",
                "Never ignore browser warnings about untrusted certificates.",
                "Clear your cookies and cache regularly to reduce tracking.",
                "Check the website’s address carefully—look out for small misspellings in domains."
            };

            SendRandomTip("safe_browsing", tips);
        }

        public static void HandleRansomwareResponse()
        {
            var tips = new List<string>
            {
                "Never pay the ransom—there's no guarantee your files will be restored.",
                "Educate yourself and others on spotting suspicious email attachments.",
                "Keep your operating system and applications updated to patch vulnerabilities.",
                "Restrict file execution permissions to prevent unauthorized apps from running."
            };

            SendRandomTip("ransomware", tips);
        }

        public static void HandleSpywareResponse()
        {
            var tips = new List<string>
            {
                "Run regular system scans even if nothing seems wrong.",
                "Be cautious with permission-heavy apps, especially on mobile.",
                "Avoid file-sharing software from unofficial sources.",
                "Use pop-up blockers—some spyware installs via fake alerts."
            };

            SendRandomTip("spyware", tips);
        }

        public static void HandleMITMResponse()
        {
            var tips = new List<string>
            {
                "Use end-to-end encrypted messaging apps when sending sensitive data.",
                "Avoid using public Wi-Fi for banking or confidential activity.",
                "Log out of websites when you're done using them, especially on shared networks.",
                "Always verify website certificates by clicking the padlock in your browser."
            };

            SendRandomTip("mitm", tips);
        }
    }
}
