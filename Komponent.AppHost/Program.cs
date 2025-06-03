using System;
using TodoAppv2.Components;

class Program
{
    static void Main(string[] args)
    {
        // Tworzymy jedno zadanie
        var zadanie = new TodoItemComponent
        {
            Id = 1,
            Title = "Napisz testowy kod",
            Description = "SprawdŸ, czy komponenty dzia³aj¹",
            IsCompleted = true
        };

        // Tworzymy listê i dodajemy zadanie
        var lista = new TodoListComponent
        {
            Owner = "Adrian",
            ListName = "Moje zadania"
        };

        lista.Add(zadanie);

        // Generujemy statystyki
        var statystyki = new StatsComponent();
        statystyki.Generate(lista);

        // Wyœwietlamy wszystko
        Console.WriteLine("Zadanie:");
        Console.WriteLine(zadanie.Summary());

        Console.WriteLine("\nLista:");
        Console.WriteLine($"Nazwa: {lista.ListName}, W³aœciciel: {lista.Owner}");

        Console.WriteLine("\nStatystyki:");
        Console.WriteLine(statystyki.Summary());
    }
}
