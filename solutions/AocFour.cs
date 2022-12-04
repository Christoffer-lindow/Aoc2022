class AocFour : Exercise
{
    private readonly string _testData =
        "2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8";

    private readonly string _dataPath = "./data/aoc-four.txt";
    private string[] TestDataIntoArr()
    {
        return _testData.Split('\n');
    }

    private List<(int, int)> LineIntoPairs(String inputRow)
    {
        var pairs = new List<(int,int)>();
        foreach(var range in inputRow.Split(','))
        {
            var pair = range.Split('-');
            pairs.Add((int.Parse(pair[0]), int.Parse(pair[1])));
        }
        return pairs;
    }

    private IEnumerable<int> GetRangeOfPair((int, int) pair)
    {
        return Enumerable.Range(Math.Min(pair.Item1, pair.Item2), Math.Max(pair.Item1, pair.Item2) - Math.Min(pair.Item1, pair.Item2) + 1);
    }

    private bool PairContainsOtherPair((int, int) pairOne, (int,int) pairTwo)
    {
        return pairOne.Item1 >= pairTwo.Item1 && pairOne.Item2 <= pairTwo.Item2;
    }

    private IEnumerable<string> LoadData()
    {
        return File.ReadLines(_dataPath);
    }

    private bool Intersects(List<(int, int)> pairs)
    {
        var firstPair = pairs[0];
        var secondPair = pairs[1];

        if (PairContainsOtherPair(firstPair, secondPair)) return true;
        if (PairContainsOtherPair(secondPair, firstPair)) return true;

        return false;
    }

    private bool AnyPartIntersects(List<(int, int)> pairs)
    {
        (int, int) firstPair;
        (int, int) secondPair;

        if (pairs[0].Item1 < pairs[1].Item1)
        {
            firstPair = pairs[0];
            secondPair = pairs[1];
        }
        else
        {
            firstPair = pairs[1];
            secondPair= pairs[0];
        }

        foreach(int i in GetRangeOfPair(firstPair))
        {
            foreach(int j in GetRangeOfPair(secondPair))
            {
                if (i == j) return true;
            }
        }
        return false;
    }
    public void Solve()
    {
        int intersections = 0;
        int anyKindOfIntersection = 0;
        foreach (var line in LoadData())
        {
            var pairs = LineIntoPairs(line);
            if (Intersects(pairs))
            {
                intersections++;
            }

            if (AnyPartIntersects(pairs))
            {
                anyKindOfIntersection++;   
            }
        }
        Console.WriteLine(intersections);
        Console.WriteLine(anyKindOfIntersection);
    }


}