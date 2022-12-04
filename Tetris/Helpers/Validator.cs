﻿namespace Tetris.Helpers
{
    public class Validator
    {
        public int CheckForFullLines(int[,] tetrisField)
        {
            int lines = 0;

            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                bool rowIsFull = true;
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    if (tetrisField[row, col] == 0)
                    {
                        rowIsFull = false;
                        break;
                    }
                }

                if (rowIsFull)
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int col = 0; col < tetrisField.GetLength(1); col++)
                        {
                            tetrisField[rowToMove, col] = tetrisField[rowToMove - 1, col];
                        }
                    }
                    lines++;
                }
            }

            return lines;
        }
    }
}
