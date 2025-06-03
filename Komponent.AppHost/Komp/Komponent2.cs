using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAppv2.Komp;

namespace TodoAppv2.Components
{
    public class TaskQueryComponent
    {
        public string? SearchTerm { get; set; }
        public string? StatusFilter { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; private set; }

        public async Task<List<TodoItem>> GetFilteredTasksAsync(TodoContext context)
        {
            IQueryable<TodoItem> query = context.TodoItems;

            if (!string.IsNullOrWhiteSpace(SearchTerm))
                query = query.Where(t => t.Name.Contains(SearchTerm));

            if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "all")
            {
                if (StatusFilter == "done") query = query.Where(t => t.IsDone);
                else if (StatusFilter == "todo") query = query.Where(t => !t.IsDone);
            }

            int pageSize = 10;
            int totalItems = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (PageIndex < 1) PageIndex = 1;
            if (TotalPages > 0 && PageIndex > TotalPages) PageIndex = TotalPages;

            return await query.OrderBy(t => t.Id)
                .Skip((PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
