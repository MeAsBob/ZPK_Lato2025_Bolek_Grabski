using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppv2.Komp
{
    public class IndexModel : PageModel
    {
        private readonly TodoContext _context;
        private readonly TaskManagerComponent _taskManager;
        private readonly TaskQueryComponent _taskQuery;

        public List<TodoItem> Tasks { get; set; } = new();

        [BindProperty]
        public TodoItem NewTask { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages => _taskQuery.TotalPages;

        public IndexModel(TodoContext context)
        {
            _context = context;
            _taskManager = new TaskManagerComponent(_context);
            _taskQuery = new TaskQueryComponent();
        }

        public async Task OnGetAsync()
        {
            _taskQuery.SearchTerm = SearchTerm;
            _taskQuery.StatusFilter = StatusFilter;
            _taskQuery.PageIndex = PageIndex;

            Tasks = await _taskQuery.GetFilteredTasksAsync(_context);
            PageIndex = _taskQuery.PageIndex;
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _taskManager.AddTaskAsync(NewTask);
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            await _taskManager.CompleteTaskAsync(id);
            return RedirectToPage("/Index", new { SearchTerm, StatusFilter, PageIndex });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _taskManager.DeleteTaskAsync(id);
            return RedirectToPage("/Index", new { SearchTerm, StatusFilter, PageIndex });
        }
    }
}
