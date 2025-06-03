using System;
using System.Text.Json;
using TodoAppv2.Komp;

namespace TodoAppv2.Components
{
    public class TaskDataComponent
    {
        private int id = -1;
        private string name = "";
        private DateTime deadline = DateTime.Today;
        private int priority = 0;
        private bool isDone = false;

        public TaskDataComponent() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Deadline { get => deadline; set => deadline = value; }
        public int Priority { get => priority; set => priority = value; }
        public bool IsDone { get => isDone; set => isDone = value; }

        public string ToJson() => JsonSerializer.Serialize(this);
        public static TaskDataComponent FromJson(string json) => JsonSerializer.Deserialize<TaskDataComponent>(json);
    }
}
