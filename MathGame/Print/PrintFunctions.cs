using MathGame.Game;

namespace MathGame.Print;

public static class PrintFunctions{
    // Font Credits: patorjk.com 
    public const string title = "       __  __   _ _____ _  _    ___   _   __  __ ___         \r\n      |  \\/  | /_\\_   _| || |  / __| /_\\ |  \\/  | __|        \r\n      | |\\/| |/ _ \\| | | __ | | (_ |/ _ \\| |\\/| | _|         \r\n      |_|  |_/_/ \\_\\_| |_||_|  \\___/_/ \\_\\_|  |_|___|        \r\n                                                     \r\n";

    public static void PrintMenu(GameManager game)
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("\t\t\t[N]ew Game");
        Console.WriteLine($"\t\t\t[C]hange Difficulty (Current: {game.difficulty})");
        Console.WriteLine("\t\t\t[H]istory");
        Console.WriteLine("\t\t\t[Q]uit");
    }    

    public static void PrintHistory(GameManager game)
    {
        Console.Clear();
        Console.Write(title);

        foreach (var r in game.getHistory())
        {
            Console.WriteLine($"\t\t{r.n1.ToString("N0")} {r.operation} {r.n2.ToString("N0")} = \\" +
                $"{r.answer}. You answer was: {(r.win ? "correct!" : "incorrect...")} ");
        }

        Console.Write($"You scored a total of {game.CalculatePoints()}!");
        Console.WriteLine();
        Console.WriteLine();
        Console.Write("\t\tPress any key to go back to the main menu...");
        Console.ReadKey();
    }

    public static void PrintChangeDiffMenu()
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("\t\tSelect your difficulty: ");

        int count = 0;
        foreach (var dif in Enum.GetNames<Difficulty>())
        {
            int difVal = (int) Enum.Parse<Difficulty>(dif);
            Console.WriteLine($"\t\t\t[{++count}]-{dif}\t(0 to {(Math.Pow(10, difVal).ToString("N0"))})");
        }
        Console.Write($"\t\t\t");
    }
}
