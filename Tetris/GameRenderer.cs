using System.Diagnostics;
using Tetris.Interfaces;

namespace Tetris
{
    public class GameRenderer
    {
        private readonly IWriter _writer;
        private readonly Stopwatch _timer;

        public GameRenderer(IWriter writer)
        {
            _writer = writer;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void DrawInfo(int columns, int level, int score)
        {
            _writer.Write("Level:", 1, 3 + columns);
            _writer.Write(level.ToString(), 2, 3 + columns);
            _writer.Write("Score:", 4, 3 + columns);
            _writer.Write(score.ToString(), 5, 3 + columns);
            _writer.Write("Time:", 7, 3 + columns);
            _writer.Write(_timer.Elapsed.ToString(@"mm\:ss"), 8, 3 + columns);
            _writer.Write("Controls:", 16, 3 + columns);
            _writer.Write($"  ^ ", 18, 3 + columns);
            _writer.Write($"<   > ", 19, 3 + columns);
            _writer.Write($"  v  ", 20, 3 + columns);
        }

        public void DrawBorder(int columns, int rows, int infoColumns)
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', columns);

            line += "╦";
            line += new string('═', infoColumns);
            line += "╗";
            _writer.Write(line);

            for (int i = 0; i < rows; i++)
            {
                string middleLine = "║";
                middleLine += new string(' ', columns);
                middleLine += "║";
                middleLine += new string(' ', infoColumns);
                middleLine += "║";
                _writer.Write(middleLine);
            }

            string endLine = "╚";
            endLine += new string('═', columns);
            endLine += "╩";
            endLine += new string('═', infoColumns);
            endLine += "╝";
            _writer.Write(endLine);
        }

        public void DrawTetrisField(int[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    if (tetrisField[row, col] == 1)
                    {
                        line += "*";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                _writer.Write(line, row + 1, 1);
            }
        }

        public void DrawCurrentFigure(int[,] currentFigure, int currentFigureCol, int currentFigureRow)
        {
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col] == 1)
                    {
                        _writer.Write("*", row + 1 + currentFigureRow, 1 + currentFigureCol + col);
                    }
                }
            }
        }
    }
}
