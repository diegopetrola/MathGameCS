
using MathGame;

// Font Credits: patorjk.com 
const string title = "       __  __   _ _____ _  _    ___   _   __  __ ___         \r\n      |  \\/  | /_\\_   _| || |  / __| /_\\ |  \\/  | __|        \r\n      | |\\/| |/ _ \\| | | __ | | (_ |/ _ \\| |\\/| | _|         \r\n      |_|  |_/_/ \\_\\_| |_||_|  \\___/_/ \\_\\_|  |_|___|        \r\n                                                     \r\n";

GameManager game = new();

void printMenu()
{
    Console.Clear();
    Console.WriteLine(title);
    Console.WriteLine("\t\t\t[N]ew Game");
    Console.WriteLine("\t\t\t[H]istory");
    Console.WriteLine("\t\t\t[Q]uit");
}

void setupGame()
{
    Dictionary<ConsoleKey, string> operatorsMap = new()
    {
        { ConsoleKey.D1, "+" },
        { ConsoleKey.D2, "-" },
        { ConsoleKey.D3, "*" },
        { ConsoleKey.D4, "/" },
        { ConsoleKey.NumPad1, "+" },
        { ConsoleKey.NumPad2, "-" },
        { ConsoleKey.NumPad3, "*" },
        { ConsoleKey.NumPad4, "/" },
    };

    string msg = "";
    int rounds = 0;
    ConsoleKey key = ConsoleKey.D0;

    Console.Clear();
    Console.WriteLine(title);
    Console.WriteLine("\t\t\tSelect an operation:");
    Console.WriteLine("\t\t\t    [1] Addition (+)");
    Console.WriteLine("\t\t\t    [2] Subtraction (-)");
    Console.WriteLine("\t\t\t    [3] Multiplication (*)");
    Console.WriteLine("\t\t\t    [4] Division (/)");

    while (!operatorsMap.ContainsKey(key))
    {
        key = Console.ReadKey().Key;
    }

    while (rounds < 1)
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine($"\t\t\tHow many rounds? {msg}");
        _ = int.TryParse(Console.ReadLine(), out rounds);
        if (rounds < 1) msg = "Please type a number greater than 0.";
    }
    operatorsMap.TryGetValue(key, out var op);
    if (op is null) throw new Exception("Error while parsing operation, operation is null.");
    playGame(rounds, op);

}

void playGame(int rounds, string op)
{
    int curRound = 0;
    while (curRound < rounds)
    {
        var round = game.PlayRound(op);
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine();
        Console.Write($"\t\t\t{round.n1} {round.operation} {round.n2} = ");
        _ = int.TryParse(Console.ReadLine(), out var answer);
        bool correct = game.CheckAnswer(answer);
        Console.WriteLine(correct ? "\t\t\tCorrect!" : "\t\t\tIncorrect...");
        Console.WriteLine("\t\t\tPress any key to next round...");
        Console.ReadKey();
        curRound++;
    }
}

void printHistory()
{
    Console.Clear();
    Console.Write(title);

    foreach(var r in game.getHistory())
    {
        Console.WriteLine($"\t\t\t{r.n1} {r.operation} {r.n2} = {r.answer}. You answered {(r.res ? "Correctly": "Incorrectly")} ");
    }

    Console.WriteLine("\t\t\tPress any key to go back to the main meny...");
    Console.ReadKey();
}

while (true)
{
    printMenu();
    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case ConsoleKey.Q:
            Console.Clear();
            Console.WriteLine("\t\t\tThank you for playing!");
            Environment.Exit(0);
            break;
        case ConsoleKey.N:
            setupGame();
            break;
        case ConsoleKey.H:
            printHistory();
            break;
    }
}

