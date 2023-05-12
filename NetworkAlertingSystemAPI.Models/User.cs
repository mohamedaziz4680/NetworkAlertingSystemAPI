using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetworkAlertingSystemAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        public List<UsersNotifications> UserNotifications { get; set; }
        [DefaultValue(false)]
        public bool IsOnline { get; set; }

    }
}
