namespace Tetris.Interfaces
{
    public interface IGameRenderer
    {
        void DrawBorder(int columns, int rows, int infoColumns);
        void DrawCurrentFigure(int[,] currentFigure, int currentFigureCol, int currentFigureRow);
        void DrawGameOver(int score);
        void DrawInfo(int columns, int level, int score);
        void DrawTetrisField(int[,] tetrisField);
    }
}