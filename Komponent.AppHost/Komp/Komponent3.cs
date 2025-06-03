using System;
using System.Text.Json;

namespace TodoAppv2.Komp
{
    public class TaskDataComponent
    {
        private int id = -1;
        private string name = string.Empty;
        private DateTime? deadline = null;
        private int priority = 1;
        private bool isDone = false;

        public TaskDataComponent() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime? Deadline { get => deadline; set => deadline = value; }
        public int Priority { get => priority; set => priority = value; }
        public bool IsDone { get => isDone; set => isDone = value; }

        public string ToJson() => JsonSerializer.Serialize(this);
        public static TaskDataComponent? FromJson(string json) => JsonSerializer.Deserialize<TaskDataComponent>(json);
    }
}