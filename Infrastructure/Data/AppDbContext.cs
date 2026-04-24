using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public  class AppDbContext:IdentityDbContext<User
        ,Role,int,IdentityUserClaim<int>,IdentityUserRole<int>,IdentityUserLogin<int>,
        IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public AppDbContext()
        {
             
        }


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
             
        }

        public DbSet<User> Users { get;set; }
        public DbSet<RefreshToken> RefreshTokens { get;set; }
    }
}
