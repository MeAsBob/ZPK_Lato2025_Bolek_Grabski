using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoAppv2.Components
{
    /// <summary>
    /// Komponent analizujący dane listy TODO.
    /// Generuje statystyki na podstawie listy zadań.
    /// </summary>
    public class StatsComponent
    {
        // ==== Prywatne pola ====
        private int _totalTasks = 0;
        private int _completedTasks = 0;
        private int _pendingTasks = 0;
        private double _completionRate = 0.0;
        private DateTime _lastUpdated = DateTime.Now;

        // ==== Konstruktor bezargumentowy ====
        public StatsComponent() { }

        // ==== Gettery i Settery ====
        public int TotalTasks { get => _totalTasks; set => _totalTasks = value; }
        public int CompletedTasks { get => _completedTasks; set => _completedTasks = value; }
        public int PendingTasks { get => _pendingTasks; set => _pendingTasks = value; }
        public double CompletionRate { get => _completionRate; set => _completionRate = value; }
        public DateTime LastUpdated { get => _lastUpdated; set => _lastUpdated = value; }

        // ==== Funkcja główna ====

        /// <summary>
        /// Generuje statystyki na podstawie komponentu listy.
        /// </summary>
        public void Generate(TodoListComponent list)
        {
            _totalTasks = list.Items.Count;
            _completedTasks = list.Items.Count(x => x.IsCompleted);
            _pendingTasks = _totalTasks - _completedTasks;
            _completionRate = _totalTasks > 0 ? (_completedTasks * 100.0 / _totalTasks) : 0.0;
            _lastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Zwraca skrócone podsumowanie statystyk.
        /// </summary>
        public string Summary()
        {
            return $"Zadań: {_totalTasks}, Zrobione: {_completedTasks}, Otwarte: {_pendingTasks}, Ukończono: {_completionRate:F1}%";
        }
    }
}
