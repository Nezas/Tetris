namespace Tetris.Models
{
    public class Figure
    {
        public int[,] CurrentFigure { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public Figure(int[,] currentFigure, int column, int row)
        {
            CurrentFigure = currentFigure;
            Column = column;
            Row = row;
        }
    }
}
