using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris.Controls
{
    public class RightArrow : ICommand
    {
        private Figure _figure;
        private Settings _settings;

        public RightArrow(Figure figure, Settings settings)
        {
            _figure = figure;
            _settings = settings;
        }

        public void Execute()
        {
            if (_figure.Column < _settings.TetrisColumns - _figure.CurrentFigure.GetLength(1))
            {
                _figure.Column++;
            }
        }
    }
}
