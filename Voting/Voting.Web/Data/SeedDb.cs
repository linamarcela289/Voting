
namespace Voting.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly DateTime date;
        private readonly DateTime dateUser;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.date = DateTime.Now;
            this.dateUser = DateTime.Now;
        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();
            await this.CheckRoles();
            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }
            
            var user = await this.CheckUserAsync(
                "linagaleano0@gmail.com", "Lina", "Galeano",
                "Admin", "Desarrollo", 3, 2, dateUser);
            if (!this.context.Events.Any())
            {
                this.AddEvent(
                    "Best character of Dragon ball",
                    "Lorem Ipsum is simply dummy text of the printing and" +
                    " typesetting industry.", user, date, date.AddDays(3));
                this.AddEvent(
                    "feminine character of more powerful throne game",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.", user, date, date.AddDays(3));
                this.AddEvent(
                    "Construction of mystical cabins in Medellin",
                    "Lorem Ipsum is simply dummy text of the printing and " +
                    "typesetting industry.", user, date, date.AddDays(3));
                await this.context.SaveChangesAsync();
            }
        }
        private async Task<User> CheckUserAsync(
            string userName, string firstName,
            string lastName, string role,
            string ocupation, int Stratum,
            int Gender, DateTime Birthdate)
        {
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(
                    userName, firstName,
                    lastName, role,
                    ocupation, Stratum,
                    Gender, Birthdate);
            }
            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }
            return user;
        }
        private async Task<User> AddUser(
            string userName, string firstName,
            string lastName, string role,
            string ocupation, int Stratum,
            int Gender, DateTime Birthdate)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                Ocupation = ocupation,
                Stratum = Stratum,
                Gender = Gender,
                Birthdate = Birthdate,
                CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
            };
            var result = await this.userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);
            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }
        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            this.AddCountry("Panamá", new string[] { "Chitré", "Santiago", "La Arena", "Agua Dulce", "Monagrillo", "Ciudad de Panamá", "Colón", "Los Santos" });
            this.AddCountry("México", new string[] { "Guadalajara", "Ciudad de México", "Monterrey", "Ciudad Obregón", "Hermosillo", "La Paz", "Culiacán", "Los Mochis" });
            this.AddCountry("Belgica", new string[] { "Aalst", "Bruselas", "Gante", "Brujas", "Gante", "Genk", "Hasselt", "Lovaina" });
            this.AddCountry("Bulgaria", new string[] { "Blagoevgrad", "Burgas", "Dobrich", "Gabrovo", "Pernik", "Pleven", "Razgrad", "Ruse" });
            this.AddCountry("Costa Rica", new string[] { "Alajuela", "Cartago", "Cañas", "Chacarita", "Esparza", "Liberia", "Nicoya", "Paraíso" });
            this.AddCountry("Eslovenia", new string[] { "Ajdovscina", "Izola", "Litija", "Liubliana", "Logatec", "Podhom", "Postojna", "Ravne" });
            await this.context.SaveChangesAsync();
        }
        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }
        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
        }
        private void AddEvent(string name, string descripton, User user, DateTime starDate, DateTime endDate)
        {
            this.context.Events.Add(new Events
            {
                Name = name,
                Decription = descripton,
                User = user,
                StarDate = starDate,
                EndDate = endDate
            });
        }
    }
}
