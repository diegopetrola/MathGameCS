
// Font Credits: https://patorjk.com 
const string title = "       __  __   _ _____ _  _    ___   _   __  __ ___         \r\n      |  \\/  | /_\\_   _| || |  / __| /_\\ |  \\/  | __|        \r\n      | |\\/| |/ _ \\| | | __ | | (_ |/ _ \\| |\\/| | _|         \r\n      |_|  |_/_/ \\_\\_| |_||_|  \\___/_/ \\_\\_|  |_|___|        \r\n                                                     \r\n";

Round[] history = [];

void printMenu()
{
    Console.WriteLine(title);
    Console.WriteLine("\t\t\t[N]ew Game");
    Console.WriteLine("\t\t\t[H]istory");
    Console.WriteLine("\t\t\t[Q]uit");
}

void setupGame()
{
    string msg = "";
    bool readNumber = false;
    int rounds = 0;
    while (rounds < 1)
    {
        Console.WriteLine(title);
        Console.WriteLine($"\t\t\tHow many rounds? {msg}");
        readNumber = int.TryParse(Console.ReadLine(), out rounds);
        if (!readNumber || rounds < 1) msg = "Please type a number greater than 0.";
    }
}

void playGame(int rounds)
{
    int curRound = 0;
    
    while (curRound < rounds)
    {

    }
}

void playRound()
{
    Round round = new Round();
    history.Append(round);

    Console.WriteLine(title);
    Console.WriteLine("\t\t\t");
}

while (true)
{
    printMenu();
    var key = Console.ReadKey(true).Key;

    switch (key)
    {
        case ConsoleKey.Q:
            Console.WriteLine("\t\t\tThank you for playing!");
            Environment.Exit(0);
            break;
        case ConsoleKey.N:
            setupGame();
            break;
    }


}

public class Round
{
    public string op = "";
    public int result { get; set; }
    public int n1 { get; set; }
    public int n2 { get; set; }
}

