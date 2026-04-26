using Data.Enums.Authantication;
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
    public static class UserSeeder
    {
        public static async Task SeedUser(UserManager<User> _userManager)
        {
            var usercount = await _userManager.Users.CountAsync();
            if (usercount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    Gender = Gender.Male,
                    BirthDate = new DateOnly(2002, 12, 1),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true

                };
                await _userManager.CreateAsync(defaultuser);
                await _userManager.AddToRoleAsync(defaultuser, "admin");
            }
                
        }
    }
}
