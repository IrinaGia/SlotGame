 using System;

class SlotGame
{
    const int COLS = 3;
    const int ROWS = 3;
    const int CHOICE_1 = 1; // Center
    const int CHOICE_2 = 2; // Horizontal
    const int CHOICE_3 = 3; // Vertical
    const int CHOICE_4 = 4; // Diagonals
    const int CHOICE_5 = 5; // All

    static Random random = new Random();
    static string[] slotValues = { "1", "2", "3" }; // values that slot will be filled with

    static void Main()
    {
        Console.WriteLine("Welcome to the Slot Machine!");
        Console.Write("Enter your wager amount: $");
        int user_wager = int.Parse(Console.ReadLine());

        Console.WriteLine("Select lines to play: " + CHOICE_1 + ": Center. " + CHOICE_2+  ": Horizontal. " + CHOICE_3+ ": Vertical. " + CHOICE_4 + ": Diagonals. " + CHOICE_5 + ": All.");
        int choice = int.Parse(Console.ReadLine());

        string[,] grid = SpinReels();
        PrintGrid(grid);

        int winnings = user_wager * CalculateWinnings(grid, choice);

        Console.WriteLine($"You won: ${winnings}");
        Console.WriteLine($"Net balance: ${(winnings - user_wager)}");
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
        if (choice == CHOICE_1 || choice == CHOICE_5) winnings += CheckHorizontal(grid, COLS, ROWS);
        if (choice == CHOICE_2 || choice == CHOICE_5)
        {
            winnings += CheckHorizontal(grid, COLS, ROWS);
        }
        if (choice == CHOICE_3 || choice == CHOICE_5)
        {
            winnings += CheckVertical(grid, COLS, ROWS);
        }
        /*if (choice == CHOICE_4 || choice == CHOICE_5)
        {
            winnings += CheckDiagonal(grid, true);
            winnings += CheckDiagonal(grid, false);
        }*/
        return winnings;
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

    public static bool CheckMainDiagonaL(string[,] grid, int rows, int cols)
    {
        bool allSameInDiagonal = true;
        for (int i = 1; i < rows; i++)  // Start from 1 to compare with previous element
        {
            if (grid[i, i] != grid[i - 1, i - 1])
            {
                allSameInDiagonal = false;
                break;
            }
        }

        return allSameInDiagonal;
    }

    // checks anti-diagonal for the same values
    public static bool CheckAntiDiagonal(string[,] grid, int rows, int cols)
    {
        bool allSameInAntiDiagonal = true;
        for (int i = 1; i < rows; i++)  // Start from 1 to compare with previous element
        {
            if (grid[i, rows - i - 1] != grid[i - 1, rows - i])
            {
                allSameInAntiDiagonal = false;
                break;
            }
        }

        return allSameInAntiDiagonal;
    }
}
