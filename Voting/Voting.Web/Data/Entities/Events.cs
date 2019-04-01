namespace Voting.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Events: IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Required]
        public string Decription { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Star Date")]
        public string StarDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        [Display(Name = "# Candidates")]
        public int NumberCandidates { get { return this.Candidates == null ? 0 : this.Candidates.Count; } }

        public User User { get; set; }

    }
}
