using Exercise_21.Data;
using Exercise_21.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace Exercise_21
{
    public static class DbInitializer
    {
        public static void Initialize(PhoneBookContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                Role adminRole = new Role { Name = "admin" };
                Role userRole = new Role { Name = "user" };
                context.Roles.Add(adminRole);
                context.Roles.Add(userRole);
            }
            if (!context.Users.Any())
            {
                string adminEmail = "admin@mail.ru";
                string adminPassword = "123456";

                User adminUser = new User { Email = adminEmail, PasswordHash = adminPassword.ToSHA256String() };
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