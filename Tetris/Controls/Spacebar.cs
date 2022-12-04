using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris.Controls
{
    public class Spacebar : ICommand
    {
        private Figure _figure;

        public Spacebar(Figure figure)
        {
            _figure = figure;
        }

        public void Execute()
        {
            var newFigure = new int[_figure.CurrentFigure.GetLength(1), _figure.CurrentFigure.GetLength(0)];
            for (int row = 0; row < _figure.CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < _figure.CurrentFigure.GetLength(1); col++)
                {
                    newFigure[col, _figure.CurrentFigure.GetLength(0) - row - 1] = _figure.CurrentFigure[row, col];
                }
            }
            _figure.CurrentFigure = newFigure;
        }
    }
}
