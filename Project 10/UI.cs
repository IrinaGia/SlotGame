using System;

public static class UI
{
    public static void Start()
    {
        Console.WriteLine("Welcome to the Slot Machine! One spin costs 50% of your wager.");

        int netBalance = 0;

        while (true)
        {
            int userWager = AskForWager();
            int choice = AskForLineChoice();

            string[,] grid = Logic.SpinReels();
            PrintGrid(grid);

            int winnings = userWager * Logic.CalculateWinnings(grid, choice);
            int spinResult = winnings - userWager / 2;

            netBalance += spinResult;

            Console.WriteLine($"\nRound result: ${spinResult}");
            Console.WriteLine($"Total net balance: ${netBalance}");

            Console.WriteLine("\nDo you want to play again? (y/n)");
            string input = Console.ReadLine().ToLower();

            if (input != "y" && input != "yes")
            {
                Console.WriteLine("Thank you for playing! Goodbye.");
                break;
            }
        }
    }

    private static int AskForWager()
    {
        Console.WriteLine("Enter your wager amount: 3$, 5$, 10$, or your own value.");

        int wager;
        while (!int.TryParse(Console.ReadLine(), out wager))
        {
            Console.WriteLine("Invalid input. Please enter a number:");
        }
        return wager;
    }

    private static int AskForLineChoice()
    {
        Console.WriteLine("Select lines to play: ");
        Console.WriteLine("1: Center");
        Console.WriteLine("2: Horizontal");
        Console.WriteLine("3: Vertical");
        Console.WriteLine("4: Diagonals");
        Console.WriteLine("5: All");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid choice. Please choose 1–5:");
        }

        return choice;
    }

    private static void PrintGrid(string[,] grid)
    {
        Console.WriteLine("\n--- Slot Machine ---");

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
