using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TodoAppv2.Components
{
    /// <summary>
    /// Komponent reprezentujący jedno zadanie TODO.
    /// Spełnia zasady komponentowości i umożliwia pełną personalizację.
    /// </summary>
    public class TodoItemComponent
    {
        // ==== Prywatne pola ====
        private int _id = 0;
        private string _title = "Nowe zadanie";
        private string _description = "Brak opisu";
        private bool _isCompleted = false;
        private DateTime _createdAt = DateTime.Now;

        // ==== Konstruktor bezargumentowy ====
        public TodoItemComponent() { }

        // ==== Gettery i Settery ====
        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }

        // ==== Funkcje użytkowe ====

        /// <summary>
        /// Zwraca skrócony opis zadania.
        /// </summary>
        public string Summary()
        {
            return $"[{_id}] {_title} - {(IsCompleted ? "Zrobione" : "Do zrobienia")}";
        }

        /// <summary>
        /// Serializuje komponent do JSON.
        /// </summary>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Tworzy komponent na podstawie danych JSON.
        /// </summary>
        public static TodoItemComponent FromJson(string json)
        {
            return JsonSerializer.Deserialize<TodoItemComponent>(json);
        }
    }
}