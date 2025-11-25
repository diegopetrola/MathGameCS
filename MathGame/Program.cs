
using MathGame.Game;
using static MathGame.Print.PrintFunctions;

GameManager game = new();

while (true)
{
    PrintMenu(game);
    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case (ConsoleKey.Q):
            ExitGame();
            break;
        case (ConsoleKey.N):
            SetupGame();
            break;
        case (ConsoleKey.H):
            PrintHistory(game);
            break;
        case (ConsoleKey.C):
            ChangeDifficulty();
            break;
    };
}

void ExitGame()
{
    Console.Clear();
    Console.WriteLine("\t\t\tThank you for playing!");
    Environment.Exit(0);
}

void SetupGame()
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
    Console.Write("\t\t\t    ");

    while (!operatorsMap.ContainsKey(key))
    {
        key = Console.ReadKey().Key;
    }

    while (rounds < 1)
    {
        Console.Clear();
        Console.WriteLine(title);
        Console.Write($"\t\t\tHow many rounds? {msg}");
        _ = int.TryParse(Console.ReadLine(), out rounds);
        if (rounds < 1) msg = "Please type a number greater than 0.";
    }
    operatorsMap.TryGetValue(key, out var op);
    if (op is null) throw new Exception("Error while parsing operation, operation is null.");
    PlayGame(rounds, op);
}

void PlayGame(int rounds, string op)
{
    int curRound = 0;
    game.StartSession();
    while (curRound < rounds)
    {
        var round = game.PlayRound(op);
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine();
        Console.WriteLine($"\t\t\tDifficulty: {game.difficulty}");
        Console.WriteLine($"\t\t\tRound: {curRound}/{rounds}\tPoints:{game.SessionPoints}");
        Console.WriteLine();
        Console.Write($"\t\t\t{round.N1.ToString("N0")} {round.Operation} {round.N2.ToString("N0")} = ");
        _ = int.TryParse(Console.ReadLine(), out var answer);
        int points = game.CheckAnswer(answer);
        Console.WriteLine(points>0? $"\t\t\tCorrect! You got scored {points}!" : "\t\t\tIncorrect...");
        Console.Write("\t\t\tPress any key to next round...");
        Console.ReadKey();
        curRound++;
    }
    game.EndSession();
}

void ChangeDifficulty()
{
    Dictionary<string, Difficulty> difMap = new();
    int count = 1;
    foreach (var dif in Enum.GetNames<Difficulty>())
    {
        difMap.Add(count.ToString(), Enum.Parse<Difficulty>(dif));
        count++;
    }

    PrintChangeDiffMenu();

    string read = "undef";
    Difficulty gameDif;
    while (!difMap.TryGetValue(read, out gameDif))
    {
        read = Console.ReadLine() ?? "undef";
        Console.WriteLine(difMap.TryGetValue(read, out gameDif));
    }

    game.difficulty = gameDif;
}