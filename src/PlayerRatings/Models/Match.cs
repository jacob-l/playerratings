using System;

namespace PlayerRatings.Models
{
    public class Match
    {
        public Guid Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public int FirstPlayerScore { get; set; }

        public int SecondPlayerScore { get; set; }

        public double? Factor { get; set; }

        public Guid LeagueId { get; set; }

        public string CreatedByUserId { get; set; }

        public string FirstPlayerId { get; set; }

        public string SecondPlayerId { get; set; }

        public virtual ApplicationUser FirstPlayer { get; set; }

        public virtual ApplicationUser SecondPlayer { get; set; }

        public virtual League League { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public string GetDescription()
        {
            if (FirstPlayerScore > SecondPlayerScore)
            {
                return FirstPlayer.DisplayName + " - " + SecondPlayer.DisplayName;
            }

            return SecondPlayer.DisplayName + " - " + FirstPlayer.DisplayName;
        }

        public string GetScore()
        {
            if (FirstPlayerScore > SecondPlayerScore)
            {
                return FirstPlayerScore + " : " + SecondPlayerScore;
            }

            return SecondPlayerScore + " : " + FirstPlayerScore;
        }
    }
}
