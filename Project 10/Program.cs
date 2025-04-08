 using System;

class SlotGame
{
    const int COLS = 4;
    const int ROWS = 3;
    const int CHOICE_1 = 1; // Center
    const int CHOICE_2 = 2; // All Horizontal
    const int CHOICE_3 = 3; // All Vertical
    const int CHOICE_4 = 4; // Diagonals
    const int CHOICE_5 = 5; // All

    static Random random = new Random();
    static string[] slotValues = { "1", "2", "3", "4", "5" }; // values that slot will be filled with

    static void Main()
    {
        Console.WriteLine("Welcome to the Slot Machine!");
        Console.Write("Enter your wager amount: $");
        int user_wager = int.Parse(Console.ReadLine());

        Console.WriteLine("Select lines to play: " + CHOICE_1 + ": Center. " + CHOICE_2+  ": All Horizontal. " + CHOICE_3+ ": All Vertical. " + CHOICE_4 + ": Diagonals. " + CHOICE_5 + ": All.");
        int choice = int.Parse(Console.ReadLine());

        string[,] grid = SpinReels();
        PrintGrid(grid);

        int winnings = CalculateWinnings(grid, choice);

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
        if (choice == CHOICE_1 || choice == CHOICE_5) winnings += CheckHorizontal(grid, 1);
        if (choice == CHOICE_2 || choice == CHOICE_5)
        {
            winnings += CheckHorizontal(grid, 0);
            winnings += CheckHorizontal(grid, 1);
            winnings += CheckHorizontal(grid, 2);
        }
        if (choice == 3 || choice == 5)
        {
            winnings += CheckVertical(grid, 0);
            winnings += CheckVertical(grid, 1);
            winnings += CheckVertical(grid, 2);
        }
        if (choice == 4 || choice == 5)
        {
            winnings += CheckDiagonal(grid, true);
            winnings += CheckDiagonal(grid, false);
        }
        return winnings;
    }

    static int CheckHorizontal(string[,] grid, int row)
    {
        if (grid[row, 0] == grid[row, 1] && grid[row, 0] == grid[row, 2])
        {
            return 3; // Payout per line
        }
        return 0;
    }

    static int CheckVertical(string[,] grid, int col)
    {
        if (grid[0, col] == grid[1, col] && grid[0, col] == grid[2, col])
        {
            return 3; // Payout per vertical line
        }
        return 0;
    }
    static int CheckDiagonal(string[,] grid, bool isMain)
    {
        if (isMain)
        {
            return (grid[0, 0] == grid[1, 1] && grid[0, 0] == grid[2, 2]) ? 3 : 0;
        }
        else
        {
            return (grid[0, 2] == grid[1, 1] && grid[0, 2] == grid[2, 0]) ? 3 : 0;
        }
    }
}
