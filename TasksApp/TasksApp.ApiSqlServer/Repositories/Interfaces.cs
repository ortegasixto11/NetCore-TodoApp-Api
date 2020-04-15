using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.ApiSqlServer.Repositories
{
    public interface ITask
    {
        Task<List<Models.Task>> GetAllAsync();
        Task<Models.Task> GetByIdAsync(string id);
        Task InsertAsync(Models.Task task);
        Task UpdateAsync(Models.Task task);
        Task DeleteAsync(Models.Task task);
        Task DeleteByIdAsync(string id);
    }
}
