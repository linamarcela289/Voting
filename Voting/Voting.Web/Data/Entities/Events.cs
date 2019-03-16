namespace Voting.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Events
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required]
        public string Decription { get; set; }

        [Display(Name = "Star Date")]
        public DateTime? StarDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
    }
}
