using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris.Controls
{
    public class LeftArrow : ICommand
    {
        private Figure _figure;

        public LeftArrow(Figure figure)
        {
            _figure = figure;
        }

        public void Execute()
        {
            if (_figure.Column >= 1)
            {
                _figure.Column--;
            }
        }
    }
}
