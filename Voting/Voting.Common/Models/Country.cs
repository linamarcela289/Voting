using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Common.Models
{
   public class Country
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cities")]
        public List<City> Cities { get; set; }

        [JsonProperty("numberCities")]
        public int NumberCities { get; set; }

    }
}
