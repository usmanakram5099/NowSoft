using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Dtos
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
        public string Device { get; set; }
        // Add other properties that you want to expose to the client
    }
}
