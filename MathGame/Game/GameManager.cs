namespace MathGame.Game;

public class GameManager
{
    private List<Round> history = [];
    private readonly Random rand = new();
    private Round? currentRound;
    public Difficulty difficulty = Difficulty.Normal;

    public Round PlayRound(string op)
    {
        currentRound = op switch
        {
            "+" => SetupSumRound(),
            "-" => SetupSubRound(),
            "*" => SetupMultRound(),
            "/" => SetupDivRound(),
            _ => throw new Exception("Unknown operator")
        };

        history.Add(currentRound);

        return currentRound;
    }
    private Round SetupDivRound()
    {
        var r = new Round();
        int maxN = (int) (Math.Pow(10, (int)difficulty));
        r.n2 = 1+rand.Next((int) Math.Sqrt(maxN)); //n2 is smaller to avoid n1 being too large
        r.answer = rand.Next(maxN);
        r.n1 = r.n2 * r.answer;
        r.operation = "/";
        return r;
    }
    private Round SetupMultRound()
    {
        var r = new Round();
        int maxN = (int)Math.Pow(10, (int)difficulty);
        r.n1 = rand.Next(maxN);
        r.n2 = rand.Next(maxN);
        r.operation = "*";
        r.answer = r.n1 * r.n2;
        return r;
    }
    private Round SetupSumRound()
    {        
        var r = new Round();
        // dividing by two garantee ans in bounds of our difficulty
        int maxN = (int) Math.Pow(10, (int)difficulty);
        r.n1 = rand.Next(maxN) ;
        r.n2 = rand.Next(maxN);
        r.difficulty = difficulty;
        r.answer = r.n1 + r.n2;
        r.operation = "+";
        return r;
    }
    private Round SetupSubRound()
    {
        var r = new Round();
        int maxN = (int)Math.Pow(10, (int)difficulty);
        int[] values = [rand.Next(maxN),rand.Next(maxN)];
        values.Sort();
        // n1 is always bigger so ans >= 0
        r.n1 = values[1];
        r.n2 = values[0];
        r.answer = r.n1 - r.n2;
        r.operation = "-";
        return r;
    }

    public bool CheckAnswer(int answer)
    {
        if (currentRound is null) throw new Exception("Start a game first, huh?");
        currentRound.win = answer == currentRound.answer;
        return currentRound.win;
    }

    public List<Round> getHistory()
    {
        return history;
    }

    public int CalculatePoints()
    {
        int points = 0;
        foreach(var r in history)
        {
            points += r.win ? (int)r.difficulty : 0;
        }
        return points;
    }
}

public class Round
{
    public string operation { get; internal set; } = "";
    public bool win { get; internal set; } = false;
    internal int answer;
    public int n1 { get; internal set; }
    public int n2 { get; internal set; }
    public Difficulty difficulty;
    public int points { get; internal set; }
}

public enum Difficulty
{
    Easy = 1,
    Normal=2,
    Hard=4,
    VonNeumann=8
}
