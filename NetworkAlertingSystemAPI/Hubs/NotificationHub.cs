using Microsoft.AspNetCore.SignalR;
using NetworkAlertingSystemAPI.DataAccess.Repository.IRepository;
using NetworkAlertingSystemAPI.Models;
using System.Text.RegularExpressions;

namespace NetworkAlertingSystemAPI.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task JoinPublisher(int userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "publishers");

            var user =  _unitOfWork.User.GetFirstOrDefault(u=>u.Id==userId);
            user.IsOnline = true;
             _unitOfWork.User.Update(user);

            await Clients.Group("subscribers").SendAsync("UpdateOnlineStatus", userId, true);
        }

        public async Task LeavePublisher(int userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "publishers");

            var user =  _unitOfWork.User.GetFirstOrDefault(u=>u.Id==userId);
            user.IsOnline = false;
             _unitOfWork.User.Update(user);

            await Clients.Group("subscribers").SendAsync("UpdateOnlineStatus", userId, false);
        }

        public async Task SendNotification(Notification notification)
        {
             _unitOfWork.Notification.Add(notification);

            await Clients.Group("subscribers").SendAsync("ReceiveNotification", notification);
        }
    }

}
