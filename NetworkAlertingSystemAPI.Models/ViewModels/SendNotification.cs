using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkAlertingSystemAPI.Models.ViewModels
{
    public class SendNotification
    {
        public string AlertTitle { get; set; }

       
        public List<int> UsersIds { get; set; }

    }
}
