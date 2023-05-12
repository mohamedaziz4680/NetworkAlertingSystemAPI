using System.ComponentModel.DataAnnotations;

namespace NetworkAlertingSystemAPI.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string AlertTitle { get; set; }
        public DateTime SentTime { get; set; }
        public string Status { get; set; }
        public List<UsersNotifications> UserNotifications { get; set; }

    }
}
