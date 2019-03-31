﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Voting.Web.Data.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities{get; set;}

        [Display(Name = "# Cities")]
        public int NumberCities { get { return this.Cities == null ? 0 : this.Cities.Count; } }

    }
}
