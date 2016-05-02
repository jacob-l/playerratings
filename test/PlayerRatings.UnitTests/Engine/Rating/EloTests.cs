using FluentAssertions;
using PlayerRatings.Engine.Rating;
using Xunit;

namespace PlayerRatings.UnitTests.Engine.Rating
{
    public class EloTests
    {
        [Theory]
        [InlineData(1400, 1400, 1, 0, 8)]
        [InlineData(1000, 4000, 1, 0, 16)]
        [InlineData(4000, 1000, 1, 0, 0)]
        [InlineData(1300, 1500, 1, 0, 12)]
        [InlineData(1300, 1500, 0, 1, -4)]
        [InlineData(1400, 1400, 0.5, 0.5, 0)]
        [InlineData(1500, 1500, 0.5, 0.5, 0)]
        [InlineData(1500, 1400, 0.5, 0.5, -2)]
        public void CalculationTest(int playerRatingA, int playerRatingB, double playerAScore, double playerBScore, int shift)
        {
            // Act
            var result = new Elo(playerRatingA, playerRatingB, playerAScore, playerBScore);

            // Assert
            result.NewRatingAPlayer.Should().Be(playerRatingA + shift);
            result.NewRatingBPlayer.Should().Be(playerRatingB - shift);
            result.ShiftRatingAPlayer.Should().Be(shift);
            result.ShiftRatingBPlayer.Should().Be(-shift);
        }
    }
}
