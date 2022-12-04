using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris.Controls
{
    public class DownArrow : ICommand
    {
        private Figure _figure;

        public DownArrow(Figure figure)
        {
            _figure = figure;
        }

        public void Execute()
        {
            _figure.Row++;
        }
    }
}
