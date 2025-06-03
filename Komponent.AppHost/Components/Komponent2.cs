using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TodoAppv2.Components
{
    /// <summary>
    /// Komponent zarządzający listą zadań TODO.
    /// Pozwala na dodawanie, usuwanie, filtrowanie i analizę zadań.
    /// </summary>
    public class TodoListComponent
    {
        // ==== Prywatne pola ====
        private List<TodoItemComponent> _items = new List<TodoItemComponent>();
        private string _owner = "Brak właściciela";
        private DateTime _createdAt = DateTime.Now;
        private string _listName = "Nowa lista";
        private bool _archived = false;

        // ==== Konstruktor bezargumentowy ====
        public TodoListComponent() { }

        // ==== Gettery i Settery ====
        public List<TodoItemComponent> Items { get => _items; set => _items = value; }
        public string Owner { get => _owner; set => _owner = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public string ListName { get => _listName; set => _listName = value; }
        public bool Archived { get => _archived; set => _archived = value; }

        // ==== Funkcje użytkowe ====

        /// <summary>
        /// Dodaje zadanie do listy.
        /// </summary>
        public void Add(TodoItemComponent item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Usuwa zadanie o danym ID.
        /// </summary>
        public bool Remove(int id)
        {
            var found = _items.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                _items.Remove(found);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Zwraca wszystkie ukończone zadania.
        /// </summary>
        public List<TodoItemComponent> GetCompleted()
        {
            return _items.Where(x => x.IsCompleted).ToList();
        }

        /// <summary>
        /// Zwraca wszystkie nieukończone zadania.
        /// </summary>
        public List<TodoItemComponent> GetPending()
        {
            return _items.Where(x => !x.IsCompleted).ToList();
        }

        /// <summary>
        /// Serializuje całą listę do JSON.
        /// </summary>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Tworzy listę na podstawie danych JSON.
        /// </summary>
        public static TodoListComponent FromJson(string json)
        {
            return JsonSerializer.Deserialize<TodoListComponent>(json);
        }
    }
}
