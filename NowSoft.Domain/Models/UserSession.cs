using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Domain.Models
{
    public class UserSession
    {
        public int Id { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to User
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
    }

}
