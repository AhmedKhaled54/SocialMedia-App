using Data.Enums.Authantication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
    public class User : IdentityUser<int>
    {
        public string? Bio { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Image { get; set; }
        public Gender Gender { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }


    }
}
