namespace MathGame.Game;

public class GameManager
{
    private readonly List<Round> History = [];
    private readonly Random rand = new();
    public Difficulty difficulty = Difficulty.Normal;
    private List<Round> currentSession = [];
    public int SessionPoints { get; private set; }

    public void StartSession()
    {
        SessionPoints = 0;
        currentSession = [];
    }

    public int EndSession()
    {
        int points = CalculatePoints(currentSession);
        currentSession = [];
        return points;
    }

    public Round PlayRound(string op)
    {
        var currentRound = op switch
        {
            "+" => SetupSumRound(),
            "-" => SetupSubRound(),
            "*" => SetupMultRound(),
            "/" => SetupDivRound(),
            _ => throw new Exception("Unknown operator")
        };
        currentRound.difficulty = difficulty;
        currentSession.Add(currentRound);
        History.Add(currentRound);

        return currentRound;
    }
    private Round SetupDivRound()
    {
        var r = new Round();
        int maxN = (int)(Math.Pow(10, (int)difficulty));
        r.N2 = 1 + rand.Next((int)Math.Sqrt(maxN)); //N2 is smaller to avoid n1 being too large
        r.answer = rand.Next(maxN);
        r.N1 = r.N2 * r.answer;
        r.Operation = "/";
        return r;
    }
    private Round SetupMultRound()
    {
        var r = new Round();
        int maxN = (int)Math.Pow(10, (int)difficulty);
        r.N1 = rand.Next(maxN);
        r.N2 = rand.Next(maxN);
        r.Operation = "*";
        r.answer = r.N1 * r.N2;
        return r;
    }
    private Round SetupSumRound()
    {
        var r = new Round();
        int maxN = (int)Math.Pow(10, (int)difficulty);
        r.N1 = rand.Next(maxN);
        r.N2 = rand.Next(maxN);
        r.answer = r.N1 + r.N2;
        r.Operation = "+";
        return r;
    }
    private Round SetupSubRound()
    {
        var r = new Round();
        int maxN = (int)Math.Pow(10, (int)difficulty);
        int[] values = [rand.Next(maxN), rand.Next(maxN)];
        values.Sort();
        // n1 is always bigger so ans >= 0
        r.N1 = values[1];
        r.N2 = values[0];
        r.answer = r.N1 - r.N2;
        r.Operation = "-";
        return r;
    }

    public int CheckAnswer(int answer)
    {
        var round = currentSession.LastOrDefault() ?? throw new Exception("Start a round first huh?");
        round.Won = answer == round.answer;
        var roundPoints = round.Won ? (int)round.difficulty : 0;
        SessionPoints += roundPoints;
        return roundPoints;
    }

    public List<Round> GetHistory()
    {
        return History;
    }

    public int CalculatePoints()
    {
        return CalculatePoints(History);
    }

    private int CalculatePoints(List<Round> ar)
    {
        int points = 0;
        foreach (var r in ar)
        {
            points += r.Won ? (int)r.difficulty : 0;
        }
        return points;
    }
}

public class Round
{
    public string Operation { get; internal set; } = "";
    public bool Won { get; internal set; }
    internal int answer;
    public int N1 { get; internal set; }
    public int N2 { get; internal set; }
    public Difficulty difficulty;
    public int Points { get; internal set; }
}

public enum Difficulty
{
    Easy = 1,
    Normal = 2,
    Hard = 4,
    VonNeumann = 8
}
