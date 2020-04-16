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
            return Ok(new Helpers.ResponseApiHelper().Success<Models.Task>(await _repo.GetAllAsync()));
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(string id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound(new Helpers.ResponseApiHelper().Error(404, "The task does not exist"));
            return Ok(new Helpers.ResponseApiHelper().Success<Models.Task>(task));
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, Models.Task task)
        {
            try
            {
                if(string.IsNullOrEmpty(id)) return BadRequest(new Helpers.ResponseApiHelper().Error("Id is required"));
                var result = await _repo.GetByIdAsync(id);
                if (result == null) return NotFound(new Helpers.ResponseApiHelper().Error(404, "The task does not exist"));
                if (!string.IsNullOrEmpty(task.Name)) result.Name = task.Name;
                result.IsActive = task.IsActive;
                await _repo.UpdateAsync(result);
                return Ok(new Helpers.ResponseApiHelper().Success("Task updated"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Helpers.ResponseApiHelper().Error(500, ex.Message));
            }
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            if (string.IsNullOrEmpty(task.Name)) return BadRequest(new Helpers.ResponseApiHelper().Error("Name is required"));
            try
            {
                await _repo.InsertAsync(task);
                return Ok(new Helpers.ResponseApiHelper().Success("Task created"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Helpers.ResponseApiHelper().Error(500, ex.Message));
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
