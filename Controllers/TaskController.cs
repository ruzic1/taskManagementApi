using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        #region ReadData

        [Authorize]
        [HttpPost("GetTasks")]
        public IActionResult GetTasks()
        {
            var getTasks = _taskService.GetTasks();
            if (getTasks.Count == 0)
            {
                return NoContent();
            }
            return Ok(getTasks);
            //if (getTasks == null)
            //{

            //}
            //if (User.)
            //return Ok(new { messsage = "Ulazi se u task endpoint!!!" });
        }


        [Authorize]
        [HttpPost("GetUsersByCompany")]
        public IActionResult GetUsersByCompany()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var callForService = _taskService.GetUsersByCompanyService(userId);
            if (callForService.Count==0)
            {
                return NoContent();
            }
            return Ok(callForService);
        }

        #endregion

        #region InsertData

        [Authorize]
        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] CreateTaskDTO newTaskData)
        {
            var service=_taskService.CreateTask(newTaskData);
            if (service)
            {
                return Ok(new { message = "Uspesno unet task, samo jos da se doda u tabeli TASKASSIGNMENT" });
            }
            else return BadRequest();

        }
        #endregion


        //[Authorize]
        //[HttpPost("AddTask")]
        //public IActionResult AddTask([FromBody])
        //{

        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
