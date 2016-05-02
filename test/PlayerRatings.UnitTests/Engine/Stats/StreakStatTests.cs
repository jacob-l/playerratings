using FluentAssertions;
using PlayerRatings.Engine.Stats;
using PlayerRatings.Models;
using Xunit;

namespace PlayerRatings.UnitTests.Engine.Stats
{
    public class StreakStatTests
    {
        private readonly ApplicationUser _player1;
        private readonly ApplicationUser _player2;

        public StreakStatTests()
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
        public void WinCalculationTest()
        {
            // Arrange
            var stat = new StreakStat(true);

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
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 3,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 6,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });

            // Assert
            stat.GetResult(_player1).Should().Be("2");
            stat.GetResult(_player2).Should().Be("0");
        }

        [Fact]
        public void LooseCalculationTest()
        {
            // Arrange
            var stat = new StreakStat(false);

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
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 3,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });
            stat.AddMatch(new Match
            {
                FirstPlayerScore = 7,
                SecondPlayerScore = 6,
                FirstPlayer = _player1,
                SecondPlayer = _player2
            });

            // Assert
            stat.GetResult(_player1).Should().Be("0");
            stat.GetResult(_player2).Should().Be("2");
        }
    }
}
