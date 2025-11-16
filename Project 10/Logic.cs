using System;

public static class Logic
{

    static readonly Random random = new Random();
    static readonly string[] slotValues = { "1", "2", "3" };

    public static string[,] SpinReels()
    {
        string[,] grid = new string[Constants.ROWS, Constants.COLS];

        for (int i = 0; i < Constants.ROWS; i++)
        {
            for (int j = 0; j < Constants.COLS; j++)
            {
                grid[i, j] = slotValues[random.Next(slotValues.Length)];
            }
        }

        return grid;
    }

    public static int CalculateWinnings(string[,] grid, int choice)
    {
        int wins = 0;

        if (choice == Constants.CHOICE_CENTER || choice == Constants.CHOICE_ALL)
            wins += CheckCenter(grid);

        if (choice == Constants.CHOICE_HORIZ || choice == Constants.CHOICE_ALL)
            wins += CheckHorizontal(grid);

        if (choice == Constants.CHOICE_VERT || choice == Constants.CHOICE_ALL)
            wins += CheckVertical(grid);

        if (choice == Constants.CHOICE_DIAG || choice == Constants.CHOICE_ALL)
        {
            wins += CheckMainDiagonal(grid);
            wins += CheckAntiDiagonal(grid);
        }

        return wins;
    }

    static int CheckCenter(string[,] grid)
    {
        int centerRow = Constants.ROWS / Constants.CHECK_ODD_KOEF;
        string firstValue = grid[centerRow, Constants.COL_INDEX];

        for (int col = 1; col < Constants.COLS; col++)
        {
            if (grid[centerRow, col] != firstValue)
                return 0;
        }

        return 1;
    }

    static int CheckHorizontal(string[,] grid)
    {
        int count = 0;

        for (int row = 0; row < Constants.ROWS; row++)
        {
            bool allSame = true;

            for (int col = 1; col < Constants.COLS; col++)
            {
                if (grid[row, col] != grid[row, col - 1])
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame)
                count++;
        }

        return count;
    }

    static int CheckVertical(string[,] grid)
    {
        int count = 0;

        for (int col = 0; col < Constants.COLS; col++)
        {
            bool allSame = true;

            for (int row = 1; row < Constants.ROWS; row++)
            {
                if (grid[row, col] != grid[row - 1, col])
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame)
                count++;
        }

        return count;
    }

    static int CheckMainDiagonal(string[,] grid)
    {
        for (int i = 1; i < Constants.ROWS; i++)
        {
            if (grid[i, i] != grid[i - 1, i - 1])
                return 0;
        }
        return 1;
    }

    static int CheckAntiDiagonal(string[,] grid)
    {
        for (int i = 1; i < Constants.ROWS; i++)
        {
            if (grid[i, Constants.ROWS - i - 1] != grid[i - 1, Constants.ROWS - i])
                return 0;
        }
        return 1;
    }
}