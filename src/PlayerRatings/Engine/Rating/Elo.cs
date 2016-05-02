using System;

namespace PlayerRatings.Engine.Rating
{
    public class Elo
    {
        private const double Denominator = 400;

        public const int K = 16;
        public const int DefaultRating = 1400;

        private readonly int _oldRatingPlayerA;
        private readonly int _oldRatingPlayerB;

        public Elo(int playerARating, int playerBRating, double playerAScore, double playerBScore, double k)
        {
            _oldRatingPlayerA = playerARating;
            _oldRatingPlayerB = playerBRating;

            var expectedScoreA = 1 / (1 + Math.Pow(10, (playerBRating - playerARating) / Denominator));
            var expectedScoreB = 1 / (1 + Math.Pow(10, (playerARating - playerBRating) / Denominator));

            NewRatingAPlayer = (int)Math.Round(playerARating + k * (playerAScore - expectedScoreA));
            NewRatingBPlayer = (int)Math.Round(playerBRating + k * (playerBScore - expectedScoreB));
        }

        public Elo(int playerARating, int playerBRating, double playerAScore, double playerBScore)
            : this(playerARating, playerBRating, playerAScore, playerBScore, K)
        { }

        public int NewRatingAPlayer { get; }

        public int NewRatingBPlayer { get; }

        public int ShiftRatingAPlayer => NewRatingAPlayer - _oldRatingPlayerA;

        public int ShiftRatingBPlayer => NewRatingBPlayer - _oldRatingPlayerB;
    }
}
