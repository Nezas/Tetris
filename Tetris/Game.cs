using Tetris.Controls;
using Tetris.Helpers;
using Tetris.Interfaces;
using Tetris.Models;

namespace Tetris
{
    public class Game
    {
        public int Score { get; private set; }
        public int Frame { get; private set; }
        public int Level { get; private set; }
        public Settings Settings { get; init; }
        public Figure Figure { get; private set; }
        public GameController GameController { get; private set; }
        //public int[,] CurrentFigure { get; private set; }
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
        private readonly Validator _validator;
        private readonly IGameRenderer _gameRenderer;

        public Game(Validator validator, IGameRenderer gameRenderer)
        {
            Score = 0;
            Frame = 0;
            Level = 1;
            Settings = new Settings(20, 20, 10, 22, 33, 16);
            _validator = validator;
            _gameRenderer = gameRenderer;
            GameController = new GameController();
            TetrisField = new int[Settings.TetrisRows, Settings.TetrisColumns];
            Figure = new(TetrisFigures[_random.Next(0, TetrisFigures.Count)], 0, 0);
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
                        GameController.Submit(new LeftArrow(Figure));
                    }
                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        GameController.Submit(new RightArrow(Figure, Settings));
                    }
                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        GameController.Submit(new DownArrow(Figure));
                        Score += Level;
                    }
                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        GameController.Submit(new Spacebar(Figure));
                    }
                }

                if (Frame % (Settings.FramesToMoveFigure - Level) == 0)
                {
                    Figure.Row++;
                    Frame = 0;
                }

                if (Collision(Figure.CurrentFigure))
                {
                    AddCurrentFigureToTetrisField();
                    int lines = _validator.CheckForFullLines(TetrisField);
                    Score += Settings.ScorePerLines[lines] * Level;
                    Figure.CurrentFigure = TetrisFigures[_random.Next(0, TetrisFigures.Count)];
                    Figure.Row = 0;
                    Figure.Column = 0;
                    if (Collision(Figure.CurrentFigure))
                    {
                        _gameRenderer.DrawGameOver(Score);
                        Thread.Sleep(100000);
                        return;
                    }
                }

                _gameRenderer.DrawBorder(Settings.TetrisColumns, Settings.TetrisRows, Settings.InfoColumns);
                _gameRenderer.DrawInfo(Settings.TetrisColumns, Level, Score);
                _gameRenderer.DrawTetrisField(TetrisField);
                _gameRenderer.DrawCurrentFigure(Figure.CurrentFigure, Figure.Column, Figure.Row);
                Thread.Sleep(40);
            }
        }

        private void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < Figure.CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < Figure.CurrentFigure.GetLength(1); col++)
                {
                    if (Figure.CurrentFigure[row, col] == 1)
                    {
                        TetrisField[Figure.Row + row, Figure.Column + col] = 1;
                    }
                }
            }
        }

        private bool Collision(int[,] figure)
        {
            if (Figure.Column > Settings.TetrisColumns - figure.GetLength(1))
            {
                return true;
            }

            if (Figure.Row + figure.GetLength(0) == Settings.TetrisRows)
            {
                return true;
            }

            for (int row = 0; row < figure.GetLength(0); row++)
            {
                for (int col = 0; col < figure.GetLength(1); col++)
                {
                    if (figure[row, col] == 1 &&
                        TetrisField[Figure.Row + row + 1, Figure.Column + col] == 1)
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
