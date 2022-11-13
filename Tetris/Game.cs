using Tetris.Interfaces;

namespace Tetris
{
    public class Game
    {
        public int Score { get; private set; }
        public int Frame { get; private set; }
        public int Level { get; private set; }
        public Settings Settings { get; init; }
        public int[,] CurrentFigure { get; private set; }
        public int CurrentFigureRow { get; private set; }
        public int CurrentFigureCol { get; private set; }
        public int[,] TetrisField { get; init; }

        public List<int[,]> TetrisFigures = new List<int[,]>()
            {
                new int[,]
                {
                    { 1, 1, 1, 1 }
                },
                new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
                new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                },
                new int[,]
                {
                    { 0, 1, 1, },
                    { 1, 1, 0, },
                },
                new int[,]
                {
                    { 1, 1, 0 },
                    { 0, 1, 1 },
                },
                new int[,]
                {
                    { 1, 0, 0 },
                    { 1, 1, 1 }
                },
                new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 }
                },
            };

        private readonly Random _random = new Random();
        private readonly GameRenderer _gameRenderer;
        private readonly IWriter _writer;

        public Game(GameRenderer gameRenderer, IWriter writer)
        {
            Score = 0;
            Frame = 0;
            Level = 1;
            Settings = new Settings(20, 20, 10, 22, 33, 16);
            _gameRenderer = gameRenderer;
            _writer = writer;
            TetrisField = new int[Settings.TetrisRows, Settings.TetrisColumns];
            InitializeGame();
            CurrentFigure = TetrisFigures[_random.Next(0, TetrisFigures.Count)];
            CurrentFigureRow = 0;
            CurrentFigureCol = 0;
    }

        private void InitializeGame()
        {
            Console.Title = "Tetris";
            Console.CursorVisible = false;
            Console.WindowHeight = Settings.ConsoleRows + 1;
            Console.WindowWidth = Settings.ConsoleColumns;
            Console.BufferHeight = Settings.ConsoleRows + 1;
            Console.BufferWidth = Settings.ConsoleColumns;
        }

        public void Start()
        {
            while (true)
            {
                Frame++;
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                    {
                        if (CurrentFigureCol >= 1)
                        {
                            CurrentFigureCol--;
                        }
                    }
                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        if (CurrentFigureCol < Settings.TetrisColumns - CurrentFigure.GetLength(1))
                        {
                            CurrentFigureCol++;
                        }
                    }
                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        Frame = 1;
                        Score += Level;
                        CurrentFigureRow++;
                    }
                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        RotateCurrentFigure();
                    }
                }

                if (Frame % (Settings.FramesToMoveFigure - Level) == 0)
                {
                    CurrentFigureRow++;
                    Frame = 0;
                }

                if (Collision(CurrentFigure))
                {
                    AddCurrentFigureToTetrisField();
                    int lines = CheckForFullLines();
                    Score += Settings.ScorePerLines[lines] * Level;
                    CurrentFigure = TetrisFigures[_random.Next(0, TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;
                    if (Collision(CurrentFigure))
                    {
                        var scoreAsString = Score.ToString();
                        scoreAsString += new string(' ', 7 - scoreAsString.Length);
                        _writer.Write("╔═════════╗", 5, 5);
                        _writer.Write("║  Game   ║", 6, 5);
                        _writer.Write("║  Over!  ║", 7, 5);
                        _writer.Write($"║ {scoreAsString} ║", 8, 5);
                        _writer.Write("╚═════════╝", 9, 5);
                        Thread.Sleep(100000);
                        return;
                    }
                }

                _gameRenderer.DrawBorder(Settings.TetrisColumns, Settings.TetrisRows, Settings.InfoColumns);
                _gameRenderer.DrawInfo(Settings.TetrisColumns, Level, Score);
                _gameRenderer.DrawTetrisField(TetrisField);
                _gameRenderer.DrawCurrentFigure(CurrentFigure, CurrentFigureCol, CurrentFigureRow);
                Thread.Sleep(40);
            }
        }

        private void RotateCurrentFigure()
        {
            var newFigure = new int[CurrentFigure.GetLength(1), CurrentFigure.GetLength(0)];
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    newFigure[col, CurrentFigure.GetLength(0) - row - 1] = CurrentFigure[row, col];
                }
            }

            if (!Collision(newFigure))
            {
                CurrentFigure = newFigure;
            }
        }

        private int CheckForFullLines()
        {
            int lines = 0;

            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                bool rowIsFull = true;
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row, col] == 0)
                    {
                        rowIsFull = false;
                        break;
                    }
                }

                if (rowIsFull)
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int col = 0; col < TetrisField.GetLength(1); col++)
                        {
                            TetrisField[rowToMove, col] = TetrisField[rowToMove - 1, col];
                        }
                    }

                    lines++;
                }
            }

            return lines;
        }

        private void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col] == 1)
                    {
                        TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = 1;
                    }
                }
            }
        }

        private bool Collision(int[,] figure)
        {
            if (CurrentFigureCol > Settings.TetrisColumns - figure.GetLength(1))
            {
                return true;
            }

            if (CurrentFigureRow + figure.GetLength(0) == Settings.TetrisRows)
            {
                return true;
            }

            for (int row = 0; row < figure.GetLength(0); row++)
            {
                for (int col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col] == 1 &&
                        TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col] == 1)
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
