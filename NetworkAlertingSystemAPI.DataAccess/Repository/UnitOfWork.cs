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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            User = new UserRepository(_db);
            Notification = new NotificationRepository(_db);
            UserNotification = new UserNotificationRepository(_db);
        }
        public IUserRepository User { get; private set; }
        public INotificationRepository Notification { get; private set; }
        public IUserNotificationRepository UserNotification { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
