using System;
using System.Text.RegularExpressions;

namespace POEPart3
{
    public static class UserInteraction
    {
        private static readonly string[] FallbackMessages = new[]
        {
            "I'm here to help with cybersecurity tips or task management — feel free to ask!",
            "Not sure I understood that, but I can help you learn about online safety.",
            "Let me know if you'd like to talk about phishing, passwords, or how to stay safe online.",
            "I'm not quite sure how to respond, but I'm ready to help with anything cybersecurity-related.",
            "You can ask me about safe browsing, your tasks, or cybersecurity threats anytime!"
        };

        public static string HandleUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "I didn't catch that — could you rephrase it?";

            var state = MemoryStore.Instance.TaskInProgress;
            input = input.ToLower().Trim();

            // Task creation stateful flow
            if (state.IsActive)
            {
                if (!state.TitleSet)
                {
                    state.Title = input;
                    return "Would you like to add a description for this task? (yes/no)";
                }

                if (!state.DescriptionSet)
                {
                    if (input.Contains("no"))
                    {
                        state.Description = "No description provided.";
                        return "Would you like to set a reminder? (yes/no)";
                    }

                    if (input.Contains("yes"))
                        return "Sure! What should the task description be?";

                    state.Description = input;
                    return "Would you like to set a reminder? (yes/no)";
                }

                if (!state.ReminderSet)
                {
                    if (input == "no")
                    {
                        state.ReminderDate = null;
                        state.IsActive = false;
                        return TaskManager.AddTask(state.Title, state.Description);
                    }

                    if (input == "yes")
                        return "How many days from now should I remind you?";

                    if (int.TryParse(Regex.Match(input, @"\d+").Value, out int days))
                    {
                        state.ReminderDate = DateTime.Now.AddDays(days);
                        state.IsActive = false;
                        return TaskManager.AddTask(state.Title, state.Description, state.ReminderDate);
                    }

                    return "I didn't catch how many days. Please enter a number like 2 or 5.";
                }
            }

            // Quick reminder/task parsing
            if (IntentParser.TryParseReminder(input, out string reminderTitle, out DateTime? when))
                return TaskManager.AddTask(reminderTitle, "Added via quick reminder phrase.", when);

            if (IntentParser.TryParseTask(input, out string taskTitle))
            {
                state.IsActive = true;
                state.Title = taskTitle;
                return "Got it! Would you like to add a description?";
            }

            // Identity & personalization
            if (input.StartsWith("my name is "))
            {
                string name = input.Replace("my name is", "").Trim();
                MemoryStore.Instance.Name = name;
                return $"Nice to meet you, {name}!";
            }

            if (input.StartsWith("i am interested in "))
            {
                string interest = input.Replace("i am interested in", "").Trim();
                MemoryStore.Instance.Interest = interest;
                return $"Great! I’ll keep your interest in {interest} in mind.";
            }

            if (input.Contains("what is my name") || input.Contains("do you remember my name"))
            {
                var name = MemoryStore.Instance.Name;
                return string.IsNullOrEmpty(name) ? "I don’t think you’ve told me your name yet." : $"Of course! Your name is {name}.";
            }

            if (input.Contains("who am i"))
            {
                var name = MemoryStore.Instance.Name;
                return string.IsNullOrEmpty(name) ? "I'm not sure I know your name yet. Want to tell me?" : $"You're {name}, my favorite cybersecurity learner!";
            }

            if (input.Contains("what is my interest") || input.Contains("do you remember my interest"))
            {
                var interest = MemoryStore.Instance.Interest;
                return string.IsNullOrEmpty(interest) ? "I don't believe you've shared what you're interested in yet." : $"Yes! You mentioned you're interested in {interest}.";
            }

            // Cybersecurity topic responses
            if (input.Contains("phishing")) { ResponseManager.HandlePhishingResponse(); return ""; }
            if (input.Contains("password")) { ResponseManager.HandlePasswordResponse(); return ""; }
            if (input.Contains("safe browsing")) { ResponseManager.HandleSafeBrowsingResponse(); return ""; }
            if (input.Contains("ransomware")) { ResponseManager.HandleRansomwareResponse(); return ""; }
            if (input.Contains("spyware")) { ResponseManager.HandleSpywareResponse(); return ""; }
            if (input.Contains("mitm") || input.Contains("man in the middle")) { ResponseManager.HandleMITMResponse(); return ""; }

            // Follow-up tip requests
            if (input.Contains("something else about phishing") || input.Contains("more about phishing")) { ResponseManager.HandlePhishingResponse(); return ""; }
            if (input.Contains("something else about password") || input.Contains("more about password")) { ResponseManager.HandlePasswordResponse(); return ""; }
            if (input.Contains("something else about safe browsing") || input.Contains("more about safe browsing")) { ResponseManager.HandleSafeBrowsingResponse(); return ""; }
            if (input.Contains("something else about ransomware") || input.Contains("more about ransomware")) { ResponseManager.HandleRansomwareResponse(); return ""; }
            if (input.Contains("something else about spyware") || input.Contains("more about spyware")) { ResponseManager.HandleSpywareResponse(); return ""; }
            if (input.Contains("something else about mitm") || input.Contains("more about man in the middle")) { ResponseManager.HandleMITMResponse(); return ""; }

            // Task commands
            if (input.Contains("add task") || input.Contains("add tasks"))
            {
                state.IsActive = true;
                state.Title = null;
                state.Description = null;
                state.ReminderDate = null;
                return "Sure! What’s the title of your new task?";
            }

            if (input.Contains("view tasks"))
                return TaskManager.GetTasksAsString();

            if (input.Contains("delete task") && int.TryParse(input.Replace("delete task", "").Trim(), out int deleteNum))
            {
                MemoryStore.Instance.AddToActivityLog("User deleted their task.");
                return TaskManager.DeleteTask(deleteNum);
            }

            if (input.Contains("complete task") && int.TryParse(input.Replace("complete task", "").Trim(), out int completeNum))
            {
                MemoryStore.Instance.AddToActivityLog("User completed their task.");
                return TaskManager.MarkTaskComplete(completeNum);
            }

            // Miscellaneous triggers
            if (input.Contains("quiz") || input.Contains("mini-game"))
            {
                MemoryStore.Instance.AddToActivityLog("User started the cybersecurity quiz.");
                return "Launching the cybersecurity quiz...";
            }

            if (input.Contains("how are you"))
                return "I'm wired with curiosity and running at full capacity!";

            if (input.Contains("activity log") || input.Contains("what have you done") || input.Contains("show my log"))
            {
                var log = MemoryStore.Instance.ActivityLog;
                return log.Count == 0 ? "No recent activity to show yet." : "Here’s what I’ve done for you recently:\n" + string.Join("\n", log);
            }

            if (input == "exit" || input == "quit")
                return "Goodbye! Stay safe online.";

            if (input.Contains("cancel task") || input == "cancel" || input == "nevermind")
            {
                if (state.IsActive)
                {
                    state.IsActive = false;
                    state.Title = null;
                    state.Description = null;
                    state.ReminderDate = null;
                    return "Task creation cancelled. Let me know when you're ready to add one!";
                }
                return "There’s no task being created at the moment.";
            }

            // Fallback
            var random = new Random();
            var fallback = FallbackMessages[random.Next(FallbackMessages.Length)];
            return fallback;
        }
    }
}
