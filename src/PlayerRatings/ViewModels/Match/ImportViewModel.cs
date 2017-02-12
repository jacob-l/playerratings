using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PlayerRatings.ViewModels.Match
{
    public class ImportViewModel
    {
        public Guid LeagueId { get; set; }

        [Required]
        public int DateIndex { get; set; }

        [Required]
        public int FirstPlayerEmailIndex { get; set; }

        [Required]
        public int SecondPlayerEmailIndex { get; set; }

        [Required]
        public int FirstPlayerScoreIndex { get; set; }

        [Required]
        public int SecondPlayerScoreIndex { get; set; }

        public int? FactorIndex { get; set; }

        [Required]
        public string DateTimeFormat { get; set; }

        public IFormFile File { get; set; }
    }
}
