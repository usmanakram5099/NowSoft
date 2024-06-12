using NowSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Dtos
{
    public class AuthenticateRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
