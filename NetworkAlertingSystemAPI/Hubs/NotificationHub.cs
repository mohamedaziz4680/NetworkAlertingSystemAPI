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
            // Add the user to the "publishers" group
            await Groups.AddToGroupAsync(Context.ConnectionId, "publishers");

            // Update the user's online status
            var user =  _unitOfWork.User.GetFirstOrDefault(u=>u.Id==userId);
            user.IsOnline = true;
             _unitOfWork.User.Update(user);

            // Notify all connected subscribers of the user's online status change
            await Clients.Group("subscribers").SendAsync("UpdateOnlineStatus", userId, true);
        }

        public async Task LeavePublisher(int userId)
        {
            // Remove the user from the "publishers" group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "publishers");

            // Update the user's online status
            var user =  _unitOfWork.User.GetFirstOrDefault(u=>u.Id==userId);
            user.IsOnline = false;
             _unitOfWork.User.Update(user);

            // Notify all connected subscribers of the user's online status change
            await Clients.Group("subscribers").SendAsync("UpdateOnlineStatus", userId, false);
        }

        public async Task SendNotification(Notification notification)
        {
            // Save the notification to the database
             _unitOfWork.Notification.Add(notification);

            // Notify all connected subscribers of the new notification
            await Clients.Group("subscribers").SendAsync("ReceiveNotification", notification);
        }
    }

}
