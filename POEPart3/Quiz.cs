using System;
using System.Collections.Generic;

namespace POEPart3
{
    public static class Quiz
    {
        private static int score = 0;
        private static int currentQuestionIndex = 0;
        private static List<QuizQuestion> questions;

        private class QuizQuestion
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public string Answer { get; set; }
            public string Explanation { get; set; }
        }

        public static void Initialize()
        {
            score = 0;
            currentQuestionIndex = 0;

            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "Which of the following is the most effective way to protect your passwords?",
                    Options = new[] { "A) Use the same password for all accounts", "B) Use complex, unique passwords for each account", "C) Write passwords on a sticky note", "D) Share passwords with friends for safekeeping"},
                    Answer = "B",
                    Explanation = "Unique, complex passwords for each account significantly reduce the risk of multiple accounts being compromised if one password is leaked."
                },
                new QuizQuestion
                {
                    Question = "True or False: Using the same password across multiple sites increases your security risk if one of the sites is compromised.",
                    Options = new[] { "A) True", "B) False" },
                    Answer = "A",
                    Explanation = "If you reuse passwords, a breach on one site can compromise your accounts on others since attackers can try the same password across different services."
                },
                new QuizQuestion
                {
                    Question = "What is a common sign that an email might be a phishing attempt?",
                    Options = new[] { "A) The email is from a known contact", "B) The email is properly formatted", "C) The email asks you to login through your usual sites", "D) The email contains urgent requests for personal information"},
                    Answer = "D",
                    Explanation = "Phishing emails often create a sense of urgency to trick recipients into revealing sensitive info or clicking malicious links."
                },
                new QuizQuestion
                {
                    Question = "Which practice helps ensure safe browsing?",
                    Options = new[] { "A) Visiting well-known websites", "B) Disabling your firewall for faster browsing", "C) Downloading files from unknown sources", "D) Ignoring browser security warnings"},
                    Answer = "A",
                    Explanation = "Sticking to reputable sites reduces the risk of encountering malicious content or phishing pages."
                },
                new QuizQuestion
                {
                    Question = "True or False: MITM attacks typically occur when an attacker secretly intercepts communication between two parties.",
                    Options = new[] { "A) True", "B) False"},
                    Answer = "A",
                    Explanation = "MITM (Man-in-the-Middle) attacks involve an attacker eavesdropping or altering communication between two unsuspecting users."
                },
                new QuizQuestion
                {
                    Question = "True or False: Clicking links in unsolicited emails is a safe way to verify the sender’s identity.",
                    Options = new[] { "A) True", "B) False"},
                    Answer = "B",
                    Explanation = "Phishing emails often contain malicious links. It's safer to verify the sender through trusted methods."
                },
                new QuizQuestion
                {
                    Question = "What does spyware primarily do?",
                    Options = new[] { "A) Protects your computer from malware", "B) Deletes malicious files", "C) Monitors and collects your personal information", "D) Encrypts your data for safety" },
                    Answer = "C",
                    Explanation = "Spyware secretly monitors user activity and collects data without consent."
                },
                new QuizQuestion
                {
                    Question = "True or False: Spyware cannot secretly monitor your activities and send your information to other people without you knowing.",
                    Options = new[] { "A) True", "B) False"},
                    Answer = "B",
                    Explanation = "Spyware is specifically designed to covertly collect personal information and send it to third parties."
                },
                new QuizQuestion
                {
                    Question = "When using public WiFi, what should you do to enhance security?",
                    Options = new[] { "A) Avoid accessing sensitive accounts", "B) Disable your antivirus", "C) Turn off your VPN", "D) Use the same password everywhere" },
                    Answer = "A",
                    Explanation = "Avoiding sensitive activity reduces risk, as public WiFi is vulnerable to data interception."
                },
                new QuizQuestion
                {
                    Question = "True or False: HTTPS indicates that a website has an encrypted connection making it safer to browse.",
                    Options = new[] { "A) True", "B) False"},
                    Answer = "A",
                    Explanation = "HTTPS encrypts your data in transit, helping protect against eavesdropping and MITM attacks."
                }
            };
        }

        public static bool HasNextQuestion() => currentQuestionIndex < questions.Count;

        public static (string, string[]) GetNextQuestion()
        {
            var q = questions[currentQuestionIndex];
            return (q.Question, q.Options);
        }

        public static string SubmitAnswer(string userAnswer)
        {
            var q = questions[currentQuestionIndex];
            string feedback;

            if (userAnswer.Trim().ToUpper() == q.Answer)
            {
                score++;
                feedback = $"✅ Correct! {q.Explanation}";
            }
            else
            {
                feedback = $"❌ Incorrect. {q.Explanation}";
            }

            currentQuestionIndex++;
            return feedback;
        }

        public static string GetFinalScore()
        {
            string summary = $"\nYour final score: {score}/{questions.Count}\n";
            summary += (score >= 7) ? "🎉 Great job! You're a cybersecurity pro!" : "📘 Keep studying — you’re on the right path!";
            return summary;
        }

        public static int GetCurrentScore() => score;
    }
}
