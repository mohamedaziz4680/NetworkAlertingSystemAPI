using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NetworkAlertingSystemAPI.DataAccess.Repository.IRepository;
using NetworkAlertingSystemAPI.Hubs;
using NetworkAlertingSystemAPI.Models;
using NetworkAlertingSystemAPI.Models.ViewModels;
using System.Collections.Generic;


namespace NetworkAlertingSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> SendNotification(SendNotification sendNotification)
        {
            var dbUsers = new List<User>();
            sendNotification.UsersIds.ForEach(i=> {
                dbUsers.Add(_unitOfWork.User.GetFirstOrDefault(u => u.Id == i));
            });
            Notification notification = new Notification();
            notification.AlertTitle = sendNotification.AlertTitle;
            notification.SentTime = new DateTime().ToLocalTime();
            notification.Status = "false";
            _unitOfWork.Notification.Add(notification);
            dbUsers.ForEach(i =>{
                UsersNotifications usersNotifications = new UsersNotifications();
                usersNotifications.User = i;
                usersNotifications.Notification = notification;
                _unitOfWork.UserNotification.Add(usersNotifications);
            });

            _unitOfWork.Save();
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);

            return Ok(notification);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotificationStatus(int id)
        {
            var notification =  _unitOfWork.Notification.GetFirstOrDefault(u=>u.Id==id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationSummary()
        {
            var notifications =  _unitOfWork.Notification.GetAll();

            return Ok(notifications);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AcknowledgeRead(int id)
        {
            var notification =  _unitOfWork.Notification.GetFirstOrDefault(u=>u.Id==id);

            if (notification == null)
            {
                return NotFound();
            }

            notification.Status = "Seen";

             _unitOfWork.Notification.Update(notification);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
