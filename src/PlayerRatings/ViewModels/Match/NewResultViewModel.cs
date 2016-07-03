using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayerRatings.ViewModels.Match
{
    public class NewResultViewModel
    {
        public NewResultViewModel()
        { }

        public NewResultViewModel(IEnumerable<Models.League> leagues, IEnumerable<Models.ApplicationUser> users)
        {
            Leagues = leagues;

            Users = users;

            Date = DateTimeOffset.UtcNow;
        }

        public IEnumerable<Models.League> Leagues { get; set; }

        public IEnumerable<Models.ApplicationUser> Users { get; set; }

        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:o}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset Date { get; set; }

        [Required]
        public string FirstPlayerId { get; set; }

        [Required]
        public string SecondPlayerId { get; set; }

        [Range(0, int.MaxValue)]
        public int FirstPlayerScore { get; set; }

        [Range(0, int.MaxValue)]
        public int SecondPlayerScore { get; set; }
    }
}
