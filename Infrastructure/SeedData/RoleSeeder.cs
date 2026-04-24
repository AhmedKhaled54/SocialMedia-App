using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static  class RoleSeeder
    {
        public static async Task SeedRole(RoleManager<Role> roleManager)
        {
            var rolecount = await roleManager.Roles.CountAsync();
            if (rolecount <= 0)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "user"
                });

                await roleManager.CreateAsync(new Role()
                {
                    Name = "admin"
                });

            }
           
        }
    }
}
