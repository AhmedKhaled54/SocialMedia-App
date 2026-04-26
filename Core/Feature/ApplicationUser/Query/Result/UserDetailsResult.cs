using Data.Enums.Authantication;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationUser.Query.Result
{
    public  class UserDetailsResult
    {
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string Email {get; set; }
        public DateOnly BirthDate { get; set; }
        public string ImgaeProfile { get; set; }
        public string Gender { get; set; }

    }
}
