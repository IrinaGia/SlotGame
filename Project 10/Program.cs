 using System;

class SlotGame
{
    const int COLS = 3;
    const int ROWS = 3;
    const int WAGER_3 = 3; // 3 dollar wager
    const int WAGER_5 = 5; // 5 dollar wager
    const int WAGER_10 = 10; // 10 dollar wager
    const int CHOICE_CENTER = 1; // Center
    const int CHOICE_HORIZ = 2; // Horizontal
    const int CHOICE_VERT = 3; // Vertical
    const int CHOICE_DIAG = 4; // Diagonals
    const int CHOICE_ALL = 5; // All
    const int CHECK_ODD_KOEF = 2; // koeficient used in checkCenter
    const int COL_INDEX = 0; // used in checkCenter

    static Random random = new Random();
    static string[] slotValues = { "1", "2", "3" }; // values that slot will be filled with

    static void Main()
    {
        Console.WriteLine("Welcome to the Slot Machine! One spin cost is 50% from your wager");
        Console.WriteLine("Enter your wager amount:" + WAGER_3 + "$; " + WAGER_5 + "$; " + WAGER_10 + "$. " + "Or insert your own wager amount." );
        int userWager = int.Parse(Console.ReadLine());

        Console.WriteLine("Select lines to play: " + CHOICE_CENTER + ": Center. " + CHOICE_HORIZ + ": Horizontal. " + CHOICE_VERT + ": Vertical. " + CHOICE_DIAG + ": Diagonals. " + CHOICE_ALL + ": All.");
        int choice = int.Parse(Console.ReadLine());

        string[,] grid = SpinReels();
        PrintGrid(grid);

        int winnings = userWager * CalculateWinnings(grid, choice);

        Console.WriteLine($"You won: ${winnings}");
        Console.WriteLine($"Net balance: ${(winnings - userWager/2)}");
    }

    static string[,] SpinReels() // filling the grid with values
    {
        string[,] grid = new string[ROWS, COLS];
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                grid[i, j] = slotValues[random.Next(slotValues.Length)];
            }
        }
        return grid;
    }

    static void PrintGrid(string[,] grid) // priting the grid
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static int CalculateWinnings(string[,] grid, int choice)
    {
        int winnings = 0;
        if (choice == CHOICE_CENTER || choice == CHOICE_ALL) winnings += CheckCenter(grid, COLS, ROWS);
        if (choice == CHOICE_HORIZ || choice == CHOICE_ALL)
        {
            winnings += CheckHorizontal(grid, COLS, ROWS);
        }
        if (choice == CHOICE_VERT || choice == CHOICE_ALL)
        {
            winnings += CheckVertical(grid, COLS, ROWS);
        }
        if (choice == CHOICE_DIAG || choice == CHOICE_ALL)
        {
            winnings += CheckMainDiagonal(grid, COLS, ROWS);
            winnings += CheckAntiDiagonal(grid, COLS, ROWS);
        }
        return winnings;
    }

    static int CheckCenter(string[,] grid, int rows, int cols)
    {
        int count = 0;
        bool sameCenter = true;
        int centerRow = rows / CHECK_ODD_KOEF;
        string firstValue = grid[centerRow, COL_INDEX];

        for (int j = 1; j < cols; j++)
        {
            if (grid[centerRow, j] != firstValue)
            {
                sameCenter = false;
                break;
            }
        }
        if (sameCenter) 
        {
            count++;
        }
        return count;
    }


    static int CheckHorizontal(string[,] grid, int rows, int cols)
    {
        int count = 0; // counts how many rows will have same numbers

        // Iterates over each row
        for (int i = 0; i < rows; i++)
        {
            bool allSameInRow = true; // if values are the same

            // Iterate through all columns starting from the second column
            for (int j = 1; j < cols; j++)
            {
                if (grid[i, j] != grid[i, j - 1]) // current column value against previous column in the same row
                {
                    allSameInRow = false; // if values do not match
                    break;
                }
            }

            if (allSameInRow)
            {
                count++;
            }
        }

        return count; 
    }

    static int CheckVertical(string[,] grid, int rows, int cols)
    {
        int count = 0; 
        
        for (int j = 0; j < rows; j++)
        {
            bool allSameInRow = true; 

            
           for (int i = 1; i < cols; i++)
            {
                if (grid[i, j] != grid[i-1, j]) // current column value against previous column in the same row
                {
                    allSameInRow = false; 
                    break;
                }
            }

            if (allSameInRow)
            {
                count++;
            }
        }

        return count; 
    }

    public static int CheckMainDiagonal(string[,] grid, int rows, int cols)
    {
        int count = 0;
        bool allSameInDiagonal = true;
        for (int i = 1; i < rows; i++)  // Start from 1 to compare with previous element
        {
            if (grid[i, i] != grid[i - 1, i - 1])
            {
                allSameInDiagonal = false;
                break;
            }
        }
        if (allSameInDiagonal) 
        {
            count++;
        }

        return count;
    }

    // checks anti-diagonal for the same values
    public static int CheckAntiDiagonal(string[,] grid, int rows, int cols)
    {
        int count = 0;
        bool allSameInAntiDiagonal = true;
        for (int i = 1; i < rows; i++)  // Start from 1 to compare with previous element
        {
            if (grid[i, rows - i - 1] != grid[i - 1, rows - i])
            {
                allSameInAntiDiagonal = false;
                break;
            }
        }
        if (allSameInAntiDiagonal)
        {
            count++;
        }

        return count;
    }
}
