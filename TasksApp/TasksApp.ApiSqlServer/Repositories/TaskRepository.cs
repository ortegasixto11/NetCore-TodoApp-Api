using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksApp.ApiSqlServer.Data;

namespace TasksApp.ApiSqlServer.Repositories
{
    public class TaskRepository : ITask
    {
        public async Task DeleteAsync(Models.Task task)
        {
            using var _context = new ApplicationDbContext();
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            using var _context = new ApplicationDbContext();
            _context.Tasks.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Models.Task>> GetAllAsync()
        {
            using var _context = new ApplicationDbContext();
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Models.Task> GetByIdAsync(string id)
        {
            using var _context = new ApplicationDbContext();
            return await _context.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Models.Task task)
        {
            using var _context = new ApplicationDbContext();
            task.IsActive = true;
            task.CreatedAt = DateTime.Now;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.Task task)
        {
            using var _context = new ApplicationDbContext();
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
