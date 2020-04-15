using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksApp.ApiSqlServer.Data;
using TasksApp.ApiSqlServer.Models;
using TasksApp.ApiSqlServer.Repositories;

namespace TasksApp.ApiSqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskRepository _repo;

        public TasksController()
        {
            _repo = new TaskRepository();
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _repo.GetAllAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(string id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound();
            return task;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, Models.Task task)
        {
            //if (id != task.Id) return BadRequest("");
            try
            {
                await _repo.UpdateAsync(task);
                return Ok("");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            try
            {
                await _repo.InsertAsync(task);
                return Ok("");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Task>> DeleteTask(string id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound();
            await _repo.DeleteByIdAsync(id);
            return task;
        }
    }
}
