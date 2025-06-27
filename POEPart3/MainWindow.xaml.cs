using System.Windows;
using POEPart3;
using Microsoft.VisualBasic;
using System.Windows.Input;

namespace POEPart3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResponseManager.SendToChat = AppendBotMessage;
            VoiceGreeting.Play();
            string name = Microsoft.VisualBasic.Interaction.InputBox("Welcome! What is your name?","Cybersecurity Assistant");

            if (!string.IsNullOrWhiteSpace(name))
            {
                MemoryStore.Instance.Name = name;
                AppendBotMessage($"Hello, {name}! I'm your cybersecurity assistant. Ask me anything about staying safe online or managing your tasks.");
            }
            else
            {
                AppendBotMessage("Hello! I'm your cybersecurity assistant. Ask me anything about staying safe online or managing your tasks.");
            }

            // Now ask about their interest
            string interest = Microsoft.VisualBasic.Interaction.InputBox(
                "What topic are you most interested in (e.g. phishing, passwords, spyware, etc.)?",
                "Cybersecurity Topics"
            );

            if (!string.IsNullOrWhiteSpace(interest))
            {
                MemoryStore.Instance.Interest = interest;
                AppendBotMessage($"Thanks, {MemoryStore.Instance.Name ?? "there"}! I'll tailor my responses to include tips about {interest}.");
            }
            else
            {
                AppendBotMessage("No worries — feel free to ask about any cybersecurity topic you like.");
            }

        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendUserMessage(input);
            txtInput.Clear();

            if (ErrorHandler.IsInvalidInput(input))
            {
                string botResponse = ErrorHandler.GetDefaultResponse();
                AppendBotMessage(botResponse);
                return;
            }

            if (input.ToLower().Contains("quiz"))
            {
                var quizWindow = new QuizWindow();
                quizWindow.ShowDialog();
                return;
            }

            if (input.Contains("view tasks"))
            {
                new TaskWindow().ShowDialog();
                return;
            }

            string rawResponse = UserInteraction.HandleUserInput(input);
            var sentiment = SentimentAnalyzer.DetectSentiment(input);
            string emotionalResponse = SentimentAnalyzer.GetSentimentResponse(sentiment, rawResponse);

            AppendBotMessage(emotionalResponse);
        }



        private void AppendUserMessage(string message)
        {
            txtConversation.Text += $"You: {message}\n";
        }

        private void AppendBotMessage(string message)
        {
            txtConversation.Text += $"Bot: {message}\n";
        }
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSend_Click(sender, new RoutedEventArgs());
                e.Handled = true; // Prevents 'ding' sound
            }
        }

    }
}
