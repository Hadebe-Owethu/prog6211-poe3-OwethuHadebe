using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using POEPart3;

public class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ReminderDate { get; set; }
    public bool IsCompleted { get; set; } = false;
}


public static class TaskManager
{
    private static List<TaskItem> tasks = new List<TaskItem>();
    private static readonly string FilePath = "tasks.json";

    static TaskManager()
    {
        LoadTasks();
    }

    public static string AddTask(string title, string description, DateTime? reminderDate = null)
    {
        MemoryStore.Instance.AddToActivityLog($"Task added: {title} {(reminderDate != null ? $"(Reminder in {((DateTime)reminderDate - DateTime.Now).Days} days)" : "")}");

        tasks.Add(new TaskItem
        {
            Title = title,
            Description = description,
            ReminderDate = reminderDate
        });

        SaveTasks();

        if (reminderDate.HasValue)
        {
            return $"Task added: {title}\nYou’ll be reminded on {reminderDate.Value.ToShortDateString()}.";
        }
        return $"Task added: {title}";
    }

    public static string GetTasksAsString()
    {
        if (tasks.Count == 0)
            return "You currently have no tasks.";

        var output = "\nYour Tasks:\n";
        for (int i = 0; i < tasks.Count; i++)
        {
            var t = tasks[i];
            output += $"\n[{i + 1}] Title: {t.Title}\n";
            output += $"    Description: {t.Description}\n";
            output += $"    Status: {(t.IsCompleted ? "Completed" : "Pending")}\n";
            if (t.ReminderDate.HasValue)
                output += $"    Reminder: {t.ReminderDate.Value.ToShortDateString()}\n";
        }

        return output;
    }

    public static string MarkTaskComplete(int index)
    {
        if (IsValidIndex(index))
        {
            tasks[index - 1].IsCompleted = true;
            SaveTasks();
            return $"Task [{index}] is marked as completed.";
        }
        return "Task number is invalid.";
    }

    public static string DeleteTask(int index)
    {
        if (IsValidIndex(index))
        {
            string title = tasks[index - 1].Title;
            tasks.RemoveAt(index - 1);
            SaveTasks();
            return $"Task [{index}] '{title}' has been deleted.";
        }
        return "Task number is invalid.";
    }

    private static bool IsValidIndex(int index)
    {
        return index >= 1 && index <= tasks.Count;
    }

    private static void SaveTasks()
    {
        try
        {
            string json = JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch
        {
            // Silent error in GUI mode for now; could log
        }
    }

    private static void LoadTasks()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                tasks = new List<TaskItem>();
            }
        }
    }

    public static List<TaskItem> GetTasks()
    {
        return tasks;
    }
}
