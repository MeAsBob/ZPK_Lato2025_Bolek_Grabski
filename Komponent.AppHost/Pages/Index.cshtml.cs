using TodoAppv2.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Komponent.AppHost.Pages
{
    public class IndexModel : PageModel
    {
        public TodoListComponent Lista { get; set; } = new();
        public StatsComponent Statystyki { get; set; } = new();

        public void OnGet()
        {
            // Przykładowe zadanie
            var zadanie = new TodoItemComponent
            {
                Id = 1,
                Title = "Zadanie testowe",
                Description = "Wygenerowane automatycznie",
                IsCompleted = true
            };

            Lista.Add(zadanie);
            Statystyki.Generate(Lista);
        }
    }
}
