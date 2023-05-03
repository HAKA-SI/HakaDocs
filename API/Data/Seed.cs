using System.ComponentModel.Design.Serialization;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Text.Json;
using API.Dtos;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedTables(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {

                //ajout d'un client pour les tests
                var hakaClient = new HaKaDocClient
                {
                    Name = "test",
                    Description = "compte client test",
                    CountryCode = "000",

                };
                context.HaKaDocClients.Add(hakaClient);
                await context.SaveChangesAsync();

                var roles = new List<AppRole>{
                new AppRole{Name="Admin"},
                new AppRole{Name="Member"}
                      };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }


                var adminUser = new AppUser { UserName = "admin", HaKaDocClientId = 1 };
                await userManager.CreateAsync(adminUser, "password");
                await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Member" });

                //ajout d'un store tes


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

            if (!context.MaritalStatuses.Any())
            {
                var maritalsStatus = new List<MaritalStatus>() {
                    new MaritalStatus{Name="Célbataire"},
                    new MaritalStatus{Name="Marié(e)"},
                    new MaritalStatus{Name="Veuf/veuve"},
                    new MaritalStatus{Name="Divorcé(e)"}
                };
                context.AddRange(maritalsStatus);
            }

            if (!context.Cities.Any())
            {
                var cities = new List<City>(){
                new City{Name="Abidjan",CountryId=1},
                new City{Name="Yamoussokro",CountryId=1},
                new City{Name="Bouake",CountryId=1},
                };

                context.AddRange(cities);
                await context.SaveChangesAsync();
            }
            if (!context.ProductGroups.Any())
            {
                var groups = new List<ProductGroup>(){
                    new ProductGroup{Name="Physical"},
                    new ProductGroup{Name="Digital"}
                };
                context.ProductGroups.AddRange(groups);
                await context.SaveChangesAsync();
            }
            if (!context.Districts.Any())
            {
                context.Districts.Add(
                    new District { Name = "Abidjan", CityId = 1 }
                );
                await context.SaveChangesAsync();
            }
            if (!context.Stores.Any())
            {
                context.Stores.Add(
                    new Store { Name = "MagTest", DistrictId = 1, HaKaDocClientId = 1 }

                );
                await context.SaveChangesAsync();
            }
            if (!context.InventOpTypes.Any())
            {
                var inventOpTypes = new List<InventOpType>(){
                new InventOpType{Name="entrée Stock"},
                new InventOpType{Name="approvisionnement"},
                new InventOpType{Name="échange Produit"},
                new InventOpType{Name="vente direct"},
                new InventOpType{Name="vente en ligne"}
              };
                context.InventOpTypes.AddRange(inventOpTypes);
                context.SaveChanges();
            }
            if (!context.CustomerCodes.Any())
            {
                var customerCode = new CustomerCode
                {
                    HaKaDocClientId = 1,
                    CodeLevel = 1
                };
                context.Add(customerCode);
                await context.SaveChangesAsync();

            }
            if (!context.StockHistoryActions.Any())
            {
                var histories = new List<StockHistoryAction>(){
                new StockHistoryAction{Name="INPUT"},
                new StockHistoryAction{Name="OUTPUT"}
              };
                context.StockHistoryActions.AddRange(histories);
                context.SaveChanges();
            }

            if (!context.InvoiceTemplates.Any())
            {
                context.InvoiceTemplates.Add(
                    new InvoiceTemplate
                    {
                        HaKaDocClientId = 1,
                        Content = "invoice content",
                        Name = "template 1"

                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.OrderTypes.Any())
            {
                context.OrderTypes.AddRange(
                   new OrderType { Name = "Vente directe" },
                   new OrderType { Name = "Vente en ligne" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.NotificationTypes.Any())
            {
                context.NotificationTypes.AddRange(
                new NotificationType { Name = "Alerte stock" }
                );
                await context.SaveChangesAsync();
            }


        }
    }
}