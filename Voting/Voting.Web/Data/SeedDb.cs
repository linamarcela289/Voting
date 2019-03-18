
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
        private readonly UserManager<User> userManager;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;

        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userManager.FindByEmailAsync("linagaleano0@gmail.com");
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
