using System.ComponentModel.Design.Serialization;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedTables(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {

                var roles = new List<AppRole>{
                new AppRole{Name="Admin"},
                new AppRole{Name="Member"},
                new AppRole{Name="Moderator"}
            };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }


                var adminUser = new AppUser { UserName = "admin" };
                await userManager.CreateAsync(adminUser, "password");
                await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Moderator" });

                //ajout utilisateur étudiant
                var studentUser = new AppUser { UserName = "student" };
                await userManager.CreateAsync(studentUser, "password");
                await userManager.AddToRoleAsync(studentUser, "Moderator");


                var banks = new List<Bank>()
                      {
                        new Bank { Name = "Ecobank" },
                        new Bank { Name = "NSIA" },
                        new Bank { Name = "SIB" },
                         new Bank { Name = "SGBCI" }
                        };
                context.AddRange(banks);

                //ajout des usertypes
                context.UserTypes.AddRange(
                    new UserType { Name = "admin" },
                    new UserType { Name = "employé" },
                    new UserType { Name = "étudiant" }
                );
                context.SaveChanges();

            }

            if (!context.Countries.Any())
            {
                //enregistrement des pays
                var countriesData = await System.IO.File.ReadAllTextAsync("Data/SeedData/countries.json");
                var countriesDto = JsonSerializer.Deserialize<List<CountryInsertionDto>>(countriesData);
                foreach (var c in countriesDto)
                {
                    context.Countries.Add(
                        new Country { Name = c.Name, Code = c.Code }
                    );
                    await context.SaveChangesAsync();
                }
            }
           

            if (!context.DocTypes.Any())
            {
                context.DocTypes.AddRange(
                    new DocType { Name = "CNI" },
                    new DocType { Name = "CV" },
                    new DocType { Name = "Passport" }
                );
                await context.SaveChangesAsync();
            }

        }
    }
}