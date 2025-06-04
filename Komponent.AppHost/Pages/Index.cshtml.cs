using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppv2.Components;

namespace Komponent.AppHost.Pages
{
    public class IndexModel : PageModel
    {
        public List<TodoItemComponent> Tasks { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        [BindProperty]
        public TodoItemComponent NewTask { get; set; } = new();

        private static List<TodoItemComponent> TaskStorage { get; } = new();

        public async Task OnGetAsync()
        {
            IEnumerable<TodoItemComponent> query = TaskStorage;

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(t => t.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "all")
            {
                if (StatusFilter == "done")
                    query = query.Where(t => t.IsCompleted);
                else if (StatusFilter == "todo")
                    query = query.Where(t => !t.IsCompleted);
            }

            int pageSize = 10;
            int totalItems = query.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (PageIndex < 1) PageIndex = 1;
            if (TotalPages > 0 && PageIndex > TotalPages) PageIndex = TotalPages;

            Tasks = query
                .OrderBy(t => t.Id)
                .Skip((PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            NewTask.Id = TaskStorage.Any() ? TaskStorage.Max(t => t.Id) + 1 : 1;
            NewTask.IsCompleted = false;
            TaskStorage.Add(NewTask);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var task = TaskStorage.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
            }

            return RedirectToPage("/Index", new { SearchTerm, StatusFilter, PageIndex });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = TaskStorage.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                TaskStorage.Remove(task);
            }

            return RedirectToPage("/Index", new { SearchTerm, StatusFilter, PageIndex });
        }
    }
}
