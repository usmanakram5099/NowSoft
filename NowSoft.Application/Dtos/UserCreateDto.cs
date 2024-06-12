using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Dtos
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; } // Ensure this is hashed in the actual application
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Add other properties necessary for creating a user
    }
}
