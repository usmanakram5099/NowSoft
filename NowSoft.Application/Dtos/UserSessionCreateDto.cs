using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Dtos
{
    public class UserSessionCreateDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
        public string Device { get; set; }
    }
}
