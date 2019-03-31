
namespace Voting.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;


    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
      //  private readonly UserManager<User> userManager;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
          

        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });

                await this.context.SaveChangesAsync();
            }

            var user = await this.userHelper.GetUserByEmailAsync("linagaleano0@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Lina",
                    LastName = "Galeano",
                    Email = "linagaleano0@gmail.com",
                    UserName = "linagaleano0@gmail.com",
                    PhoneNumber = "350 634 2747",
                    Address = "Calle Luna Calle Sol",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await this.userHelper.AddUserToRoleAsync(user, "Admin");

            }
            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }



            if (!this.context.Events.Any())
            {
                this.AddEvent(
                   "First Event",
                   "Lorem Ipsum is simply dummy text of the printing and" +
                   " typesetting industry.");
                this.AddEvent(
                    "Second Event",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.");
                this.AddEvent(
                    "Third Event",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name, string descripton)
        {
            this.context.Events.Add(new Events
            {
                Name = name,
                Decription = descripton,
             //   User = user
            });
        }



    }

}
