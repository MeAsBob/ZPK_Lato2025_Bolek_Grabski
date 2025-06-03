using System.Threading.Tasks;
using TodoAppv2.Komp;
using Microsoft.EntityFrameworkCore;

namespace TodoAppv2.Komp
{
    public class TaskManagerComponent
    {
        private readonly TodoContext _context;

        public TaskManagerComponent(TodoContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(TodoItem task)
        {
            task.IsDone = false;
            _context.TodoItems.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CompleteTaskAsync(int id)
        {
            var task = await _context.TodoItems.FindAsync(id);
            if (task == null) return false;
            task.IsDone = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.TodoItems.FindAsync(id);
            if (task == null) return false;
            _context.TodoItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}