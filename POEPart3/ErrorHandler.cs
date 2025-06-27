using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart3
{
    public static class ErrorHandler
    {
        private static readonly List<string> DefaultResponses = new List<string>
        {
            "I'm not sure that I understand properly, can you please rephrase your question?",
            "Can you please clarify again what you would like to know?",
            "I want to make sure I address your concern correctly. Could you please rephrase?"
        };

        public static string GetDefaultResponse()
        {
            Random rnd = new Random();
            return DefaultResponses[rnd.Next(DefaultResponses.Count)];
        }
        public static bool IsInvalidInput(string input)
        {
            return string.IsNullOrWhiteSpace(input) ||
                input.Length < 2 ||
                input.ToLower().Split(' ').Length < 2;
        }
    }
}
