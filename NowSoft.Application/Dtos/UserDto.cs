using NowSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Dtos
{
    public class UserDto
    {
        public UserDto(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
