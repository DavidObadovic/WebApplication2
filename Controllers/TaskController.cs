using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> lista = new List<TaskItem>();

        [HttpGet]
        public ActionResult<List<TaskItem>> GetTask()
        {
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult<List<Task>> CreateTask(TaskItem task)
        {
            task.Id = lista.Count + 1;
            lista.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

    }
}
