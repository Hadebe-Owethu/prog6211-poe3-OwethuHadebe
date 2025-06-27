using System.Windows;
using POEPart3;

namespace POEPart3
{
    public partial class QuizWindow : Window
    {
        public QuizWindow()
        {
            InitializeComponent();
            Quiz.Initialize();      // Start the quiz logic
            ShowNextQuestion();     // Display the first question
        }

        private void ShowNextQuestion()
        {
            if (Quiz.HasNextQuestion())
            {
                var (question, options) = Quiz.GetNextQuestion();
                txtQuestion.Text = question;
                lstOptions.Items.Clear();
                txtFeedback.Text = "";
                foreach (var option in options)
                    lstOptions.Items.Add(option);

                btnSubmit.IsEnabled = true;
                btnNext.IsEnabled = false;
            }
            else
            {
                txtQuestion.Text = "Quiz Complete!";
                lstOptions.Visibility = Visibility.Collapsed;
                btnSubmit.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Collapsed;
                txtFeedback.Text = Quiz.GetFinalScore();
            }
            txtScore.Text = $"Score: {Quiz.GetCurrentScore()} / 10";

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (lstOptions.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            string selected = lstOptions.SelectedItem.ToString();
            string answer = selected.Substring(0, 1); // Extract A/B/C/D

            string feedback = Quiz.SubmitAnswer(answer);
            txtFeedback.Text = feedback;

            // Style feedback color
            if (feedback.StartsWith("✅"))
            {
                txtFeedback.Foreground = System.Windows.Media.Brushes.DarkGreen;
                lstOptions.Foreground = System.Windows.Media.Brushes.Green;
            }
            else if (feedback.StartsWith("X"))
            {
                txtFeedback.Foreground = System.Windows.Media.Brushes.DarkRed;
                lstOptions.Foreground = System.Windows.Media.Brushes.Red;
            }

            btnSubmit.IsEnabled = false;
            btnNext.IsEnabled = true;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}
