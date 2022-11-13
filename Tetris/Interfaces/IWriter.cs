namespace Tetris.Interfaces
{
    public interface IWriter
    {
        void Write(string value);
        void Write(string text, int row, int col);
    }
}
