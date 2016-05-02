using FluentAssertions;
using PlayerRatings.Engine.Stats;
using PlayerRatings.Models;
using Xunit;

namespace PlayerRatings.UnitTests.Engine.Stats
{
    public class GoalsForAgainsStatTests
    {
        private readonly ApplicationUser _player1;
        private readonly ApplicationUser _player2;

        public GoalsForAgainsStatTests()
        {
            _player1 = new ApplicationUser
            {
                Id = "1"
            };
            _player2 = new ApplicationUser
            {
                Id = "2"
            };
        }

        [Fact]
        public void CalculationTest()
        {
            // Arrange
            var stat = new GoalsForAgainstStat();

            // Act
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 5,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 1,
                SecondPlayerScore = 7,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });

            // Assert
            stat.GetResult(_player1).Should().Be(.67.ToString("N2"));
            stat.GetResult(_player2).Should().Be(1.5.ToString("N2"));
        }

        [Fact]
        public void ZeroTest()
        {
            // Arrange
            var stat = new GoalsForAgainstStat();

            // Act
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 0,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });

            // Assert
            stat.GetResult(_player1).Should().Be("∞");
            stat.GetResult(_player2).Should().Be(0.ToString("N2"));
        }
    }
}
