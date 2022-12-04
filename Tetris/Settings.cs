namespace Tetris
{
    public class Settings
    {
        public int TetrisRows { get; init; }
        public int TetrisColumns { get; init; }
        public int InfoColumns { get; init; }
        public int ConsoleRows { get; init; }
        public int ConsoleColumns { get; init; }
        public int FramesToMoveFigure { get; init; }

        public int[] ScorePerLines { get; } = { 0, 40, 100, 300, 1200 };

        public Settings(int tetrisRows, int tetrisColumns, int infoColumns, int consoleRows, int consoleColumns, int framesToMoveFigure)
        {
            TetrisRows = tetrisRows;
            TetrisColumns = tetrisColumns;
            InfoColumns = infoColumns;
            ConsoleRows = consoleRows;
            ConsoleColumns = consoleColumns;
            FramesToMoveFigure = framesToMoveFigure;
            InitializeGame();
        }

        private void InitializeGame()
        {
            Console.Title = "Tetris";
            Console.CursorVisible = false;
            Console.WindowHeight = ConsoleRows + 1;
            Console.WindowWidth = ConsoleColumns;
            Console.BufferHeight = ConsoleRows + 1;
            Console.BufferWidth = ConsoleColumns;
        }
    }
}
