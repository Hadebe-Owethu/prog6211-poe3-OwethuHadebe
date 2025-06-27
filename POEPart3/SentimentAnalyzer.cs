using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart3
{
    /// Represents possible user emotional states.
    public enum UserSentiment
    {
        Neutral,
        Curious,
        Frustrated,
        Worried,
        Confused
    }
    public static class SentimentAnalyzer
    {
        /// Analyze input text and classify the user's sentiment.
        public static UserSentiment DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("worried") || lowerInput.Contains("concerned") ||
                lowerInput.Contains("anxious") || lowerInput.Contains("scared"))
            {
                return UserSentiment.Worried;
            }
            else if (lowerInput.Contains("what") || lowerInput.Contains("how") ||
                lowerInput.Contains("why") || lowerInput.Contains("?"))
            {
                return UserSentiment.Curious;
            }
            else if (lowerInput.Contains("annoyed") || lowerInput.Contains("frustrated") ||
                lowerInput.Contains("upset") || lowerInput.Contains("angry"))
            {
                return UserSentiment.Frustrated;
            }
            else if (lowerInput.Contains("don't understand") || lowerInput.Contains("confused") ||
                lowerInput.Contains("not sure"))
            {
                return UserSentiment.Confused;
            }
            return UserSentiment.Neutral;
        }
        /// Customize a given response based on the user's detected sentiment.
        public static string GetSentimentResponse(UserSentiment sentiment, string originalResponse)
        {
            switch (sentiment)
            {
                case UserSentiment.Worried:
                    return $"I fully understand that this can be worrying. {originalResponse} " +
                        "Remember, you're taking great steps to protect yourself by learning about these risks";

                case UserSentiment.Curious:
                    return $"That's a great question. {originalResponse} " + "Let me know if you'd like more details about this topic.";

                case UserSentiment.Frustrated:
                    return $"I understand your frustation. Cybersecurity can be challenging and a lot to understand. {originalResponse} " +
                        "Would it help if i broke this into simpler terms?";

                case UserSentiment.Confused:
                    return $"Let me try to explain this more clearly. {originalResponse} " +
                        "Would you like me to go over this again?";

                default:
                    return originalResponse;
            }
        }
    }
}
