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
        [Display(Name = "Description")]
        public string Decription { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime? StarDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        [Display(Name = "# Candidates")]
        public int NumberCandidates { get { return this.Candidates == null ? 0 : this.Candidates.Count; } }

        public User User { get; set; }

        public ICollection<Vote> Vote { get; set; }

        [Display(Name = "# Vote")]
        public int NumberVotes { get { return this.Vote == null ? 0 : this.Vote.Count; } }

    }
}
