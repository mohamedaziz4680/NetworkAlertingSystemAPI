
using NetworkAlertingSystemAPI.Data;
using NetworkAlertingSystemAPI.DataAccess.Repository.IRepository;
using NetworkAlertingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkAlertingSystemAPI.DataAccess.Repository
{
    public class UserNotificationRepository : Repository<UsersNotifications>, IUserNotificationRepository
    {
        private ApplicationDbContext _db;
        public UserNotificationRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(UsersNotifications usersNotifications)
        {
            _db.UserNotifications.Update(usersNotifications);
        }
    }
}
