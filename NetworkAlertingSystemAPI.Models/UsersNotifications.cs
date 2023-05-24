using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkAlertingSystemAPI.Models
{
    public class UsersNotifications
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null;
        [ForeignKey("NotificationId")]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; } = null;

        public DateTime? SeenTime { get; set; }
    }
}
