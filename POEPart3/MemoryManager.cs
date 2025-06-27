using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart3
{
    /// Manages user data (name, interest, etc.) and provides personalized messaging.

    public class MemoryManager
    {
        private const int MaxLogSize = 10;
        public List<string> ActivityLog { get; } = new List<string>();

        public void AddToActivityLog(string message)
        {
            ActivityLog.Add($"{DateTime.Now:HH:mm} - {message}");

            // Limit to the last 10 entries
            if (ActivityLog.Count > MaxLogSize)
                ActivityLog.RemoveAt(0);
        }
        public class TaskCreationState
        {
            public bool IsActive { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? ReminderDate { get; set; }
            public bool TitleSet => !string.IsNullOrWhiteSpace(Title);
            public bool DescriptionSet => !string.IsNullOrWhiteSpace(Description);
            public bool ReminderSet => ReminderDate.HasValue;
        }


        public TaskCreationState TaskInProgress { get; } = new TaskCreationState();

        // Case-insensitive dictionary for storing user info
        private Dictionary<string, string> userData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Property to get/set user’s name
        public string Name
        {
            get => userData.TryGetValue("name", out string name) ? name : null;
            set => userData["name"] = value;
        }

        // Property to get/set user’s interest topic
        public string Interest
        {
            get => userData.TryGetValue("interest", out string interest) ? interest : null;
            set => userData["interest"] = value;
        }

        // Save custom key-value data to memory
        public void Remember(string key, string value)
        {
            userData[key.ToLower()] = value;
        }

        // Retrieve previously stored data by key
        public string Recall(string key)
        {
            return userData.TryGetValue(key.ToLower(), out string value) ? value : null;
        }

        // Create personalized message using name and/or interest
        public string PersonalizeResponse(string message)
        {
            return message; // Don’t include name or interest automatically
        }


    }
}
