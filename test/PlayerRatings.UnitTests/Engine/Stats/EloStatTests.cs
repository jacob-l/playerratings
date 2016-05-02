using FluentAssertions;
using PlayerRatings.Engine.Stats;
using PlayerRatings.Models;
using Xunit;

namespace PlayerRatings.UnitTests.Engine.Stats
{
    public class EloStatTests
    {
        [Fact]
        public void CalculationTest()
        {
            // Arrange
            var player1 = new ApplicationUser
            {
                Id = "1"
            };
            var player2 = new ApplicationUser
            {
                Id = "2"
            };
            var eloStat = new EloStat();

            // Act
            eloStat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 5,
                FirstPlayer = player1,
                SecondPlayer = player2
            });
            eloStat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 2,
                FirstPlayer = player1,
                SecondPlayer = player2
            });
            eloStat.AddMatch(new Match
            {
                FirstPlayerScore = 1,
                SecondPlayerScore = 7,
                FirstPlayer = player1,
                SecondPlayer = player2
            });

            // Assert
            eloStat[player1].Should().Be(1407);
            eloStat[player2].Should().Be(1393);
        }
    }
}
