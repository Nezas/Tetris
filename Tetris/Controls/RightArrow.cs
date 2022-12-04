using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris.Controls
{
    public class RightArrow : ICommand
    {
        private Figure _figure;
        private int _tetrisColumns;

        public RightArrow(Figure figure, int tetrisColumns)
        {
            _figure = figure;
            _tetrisColumns = tetrisColumns;
        }

        public void Execute()
        {
            if (_figure.Column < _tetrisColumns - _figure.CurrentFigure.GetLength(1))
            {
                _figure.Column++;
            }
        }
    }
}
