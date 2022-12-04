using Tetris.Controls;
using Tetris.Interfaces;
using Tetris.Models;
using Xunit;

namespace Tetris.UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void LeftArrow_OnClick_GoesLeft()
        {
            Figure figure = new(null, 2, 0);
            ICommand leftArrow = new LeftArrow(figure);

            leftArrow.Execute();

            Assert.Equal(1, figure.Column);
            Assert.Equal(0, figure.Row);
        }

        [Fact]
        public void RightArrow_OnClick_GoesRight()
        {
            Figure figure = new(new int[,] { { 1, 1, 1, 1 } }, 0, 0);
            ICommand rightArrow = new RightArrow(figure, 20);

            rightArrow.Execute();

            Assert.Equal(1, figure.Column);
            Assert.Equal(0, figure.Row);
        }

        [Fact]
        public void DownArrow_OnClick_GoesDown()
        {
            Figure figure = new(null, 0, 0);
            ICommand downArrow = new DownArrow(figure);

            downArrow.Execute();

            Assert.Equal(0, figure.Column);
            Assert.Equal(1, figure.Row);
        }

        [Fact]
        public void AddDownScore_OnDownClick_AddPoints()
        {
            Game game = new();

            game.AddDownScore();

            Assert.Equal(1, game.Score);
        }
    }
}