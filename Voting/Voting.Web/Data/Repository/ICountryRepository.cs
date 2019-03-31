﻿namespace Voting.Web.Data.Repository
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;
    using Voting.Web.Models;

    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();

        Task<Country> GetCountryWithCitiesAsync(int id);

        Task<City> GetCityAsync(int id);

        Task AddCityAsync(CityViewModel model);

        Task<int> UpdateCityAsync(City city);

        Task<int> DeleteCityAsync(City city);
    }

}
