namespace MathGame
{
    internal class GameManager
    {
        private List<Round> history = [];
        private readonly Random rand = new();
        private Round? currentRound;

        public Round PlayRound(string op)
        {
            currentRound = new Round();
            history.Append(currentRound);
            currentRound.n1 = 5;
            currentRound.n2 = 3;
            currentRound.operation = op;
            switch (op)
            {
                case ("+"):
                    currentRound.answer = currentRound.n1 + currentRound.n2;
                    break;
                case ("-"):
                    currentRound.answer = currentRound.n1 - currentRound.n2;
                    break;
                case ("*"):
                    currentRound.answer = currentRound.n1 * currentRound.n2;
                    break;
                case ("/"):
                    currentRound.answer = currentRound.n1 / currentRound.n2;
                    break;
                default:
                    throw new Exception("Unknown operator");

            }
            return currentRound;
        }

        public bool CheckAnswer(int answer)
        {
            if (currentRound is null) throw new Exception("Start a game first, huh?");
            currentRound.res = answer == currentRound.answer;
            return currentRound.res;
        }

        public List<Round> getHistory()
        {
            return history;
        }
    }
    
    public class Round
    {
        public string operation { get; internal set; } = "";
        public bool res { get; internal set; } = false;
        internal int answer;
        public int n1 { get; internal set; }
        public int n2 { get; internal set; }
    }
}
