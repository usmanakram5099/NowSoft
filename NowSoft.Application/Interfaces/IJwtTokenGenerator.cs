using NowSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
    }

}
