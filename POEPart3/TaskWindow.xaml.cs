using System.Windows;
using POEPart3;

namespace POEPart3
{
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            lstTasks.Items.Clear();
            var tasks = TaskManager.GetTasks();
            if (tasks.Count == 0)
            {
                lstTasks.Items.Add("You currently have no tasks.");
                lstTasks.IsEnabled = false;
            }
            else
            {
                lstTasks.IsEnabled = true;
                for (int i = 0; i < tasks.Count; i++)
                {
                    var t = tasks[i];
                    string item = $"[{i + 1}] {t.Title} — {(t.IsCompleted ? "✅ Completed" : "❌ Pending")}";
                    if (t.ReminderDate.HasValue)
                        item += $" (Remind: {t.ReminderDate.Value.ToShortDateString()})";
                    lstTasks.Items.Add(item);
                }
            }
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                TaskManager.MarkTaskComplete(lstTasks.SelectedIndex + 1);
                LoadTasks();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                TaskManager.DeleteTask(lstTasks.SelectedIndex + 1);
                LoadTasks();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
