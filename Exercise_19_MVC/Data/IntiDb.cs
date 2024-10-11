using Exercise_21.Data;
using Exercise_21.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_21
{
    public static class DbInitializer
    {

        public static async Task Initialize(IWebHost init)
        {
            var s = init.Services.CreateScope().ServiceProvider;
            var context = s.GetRequiredService<PhoneBookContext>();
            context.Database.EnsureCreated();
            var _roleManager = s.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "admin", "user" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var _userManager = s.GetRequiredService<UserManager<User>>();

            if (! await _userManager.Users.AnyAsync())
            {
                var adminUser = new User { UserName = "admin" };               
                var createResult1 = await _userManager.CreateAsync(adminUser, "Qwerty_123");
                var addToRoleAdmin = await _userManager.AddToRoleAsync(adminUser, "admin");

                var user = new User { UserName = "user" };
                var createResult2 = await _userManager.CreateAsync(user, "Qwerty_123");
                var addToRoleUser = await _userManager.AddToRoleAsync(adminUser, "user");
            }
            if (context.Notes.Any()) return;

            var sections = new List<Note>()
            {
                new Note(){LastName="Иванов", FirstName="Иван", Patronymic="Иванович", Address="111", Phone="111", Description="111"}
            };
            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var section in sections)
                {
                    context.Notes.Add(section);
                }

                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Notes] ON");
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Notes] OFF");
                trans.Commit();
            }


        }
    }
}