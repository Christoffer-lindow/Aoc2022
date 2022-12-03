class AocTwo : Exercise
{
    private readonly List<(char, char)> _testData = new() { ('Y', 'A'), ('X', 'B'), ('Z', 'C') };
    private string _path = "./data/aoc-two.txt";

    private HandType TranslateOtherHand(char input) => input switch
    {
        'A' => HandType.Rock,
        'B' => HandType.Paper,
        'C' => HandType.Scissors,
        _ => throw new Exception("unreachable")
    };

    private HandType WinningStrategy(char player, HandType other)
    {
        switch (player)
        {
            case 'X':
                return other switch
                {
                    HandType.Rock => HandType.Scissors,
                    HandType.Paper => HandType.Rock,
                    HandType.Scissors => HandType.Paper,
                    _ => throw new Exception("unreadable")
                };
            case 'Z':
                return other switch
                {
                    HandType.Rock => HandType.Paper,
                    HandType.Paper => HandType.Scissors,
                    HandType.Scissors => HandType.Rock,
                    _ => throw new Exception("unreachable")
                };
            default:
                return other;
        }
    }


    bool PlayerIsWinner(HandType playerHand, HandType otherHand) => (playerHand, otherHand) switch
    {
        (HandType.Rock, HandType.Scissors) => true,
        (HandType.Paper, HandType.Rock) => true,
        (HandType.Scissors, HandType.Paper) => true,
        _ => false
    };


    int GetPlayerScoreByRound(char player, char other)
    {
        HandType otherHand = TranslateOtherHand(other);
        HandType playerHand = WinningStrategy(player, otherHand);
        int handValue = (int)playerHand;

        if (playerHand == otherHand) return 3 + handValue;

        return PlayerIsWinner(playerHand, otherHand) ? 6 + handValue : handValue;
    }

    private enum HandType
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public void Solve()
    {
        int playerScore = 0;
        foreach ((char player, char other) in LoadData())
        {
            playerScore += GetPlayerScoreByRound(player, other);
        }
        Console.WriteLine(playerScore);
    }

    List<(char, char)> LoadData()
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            char? player = null;
            char? other = null;

            List<(char, char)> data = new();

            while (sr.Peek() >= 0)
            {
                var c = (char)sr.Read();

                if (c == '\n')
                {
                    if (player is null || other is null)
                    {
                        throw new Exception("Invalid input, player or other should not be null");
                    }

                    data.Add(((char, char))(player, other));
                    player = null;
                    other = null;
                    continue;
                }

                if (c == 'X' || c == 'Y' || c == 'Z')
                {
                    player = c;
                }
                if (c == 'A' || c == 'B' || c == 'C')
                {
                    other = c;
                }
            }
            return data;
        }
    }
}