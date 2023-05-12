using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetworkAlertingSystemAPI.DataAccess.Repository.IRepository;
using NetworkAlertingSystemAPI.Hubs;
using NetworkAlertingSystemAPI.Models;

namespace NetworkAlertingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public UserController(IUnitOfWork unitOfWork,IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users =  _unitOfWork.User.GetAll();
            return Ok(users);
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user =  _unitOfWork.User.GetFirstOrDefault(u => u.Id==id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        //[Route("Post")]
        //[HttpPost]
        //public void Connect(int id)
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //}
    }
}

