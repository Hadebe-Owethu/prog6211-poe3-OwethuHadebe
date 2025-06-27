using System;
using System.Text.RegularExpressions;

namespace POEPart3
{
    public static class IntentParser
    {
        public static bool TryParseReminder(string input, out string title, out DateTime? reminderDate)
        {
            title = null;
            reminderDate = null;

            input = input.ToLower();

            // Match something like "remind me to X in 3 days" or "remind me to X tomorrow"
            var match = Regex.Match(input, @"remind me to (?<task>.+?)( in (?<days>\d+) days| tomorrow)?");

            if (match.Success)
            {
                title = match.Groups["task"].Value.Trim();

                if (match.Groups["days"].Success && int.TryParse(match.Groups["days"].Value, out int days))
                {
                    reminderDate = DateTime.Now.AddDays(days);
                }
                else if (input.Contains("tomorrow"))
                {
                    reminderDate = DateTime.Now.AddDays(1);
                }

                return true;
            }

            return false;
        }

        public static bool TryParseTask(string input, out string title)
        {
            title = null;

            // Catch things like "add a task to..." or "create a task for..."
            var match = Regex.Match(input, @"(add|create|make) (a )?task (to|for) (?<title>.+)");

            if (match.Success)
            {
                title = match.Groups["title"].Value.Trim();
                return true;
            }

            return false;
        }
    }
}
