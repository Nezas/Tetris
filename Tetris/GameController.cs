using Tetris.Interfaces;

namespace Tetris
{
    public class GameController
    {
        public void Submit(ICommand command)
        {
            command.Execute();
        }
    }
}
