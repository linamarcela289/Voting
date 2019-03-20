
namespace Voting.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly UserManager<User> userManager;

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


            var user = await this.userHelper.GetUserByEmailAsync("linagaleano0@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Lina",
                    LastName = "Galeano",
                    Email = "linagaleano0@gmail.com",
                    UserName = "linagaleano0@gmail.com"
                };

                var result = await this.userManager.CreateAsync(user, "123456");
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
                   " typesetting industry.",user);
                this.AddEvent(
                    "Second Event",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.", user);
                this.AddEvent(
                    "Third Event",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddEvent(string name, string descripton, User user)
        {
            this.context.Events.Add(new Events
            {
                Name = name,
                Decription = descripton,
                User = user
            });
        }



    }

}
