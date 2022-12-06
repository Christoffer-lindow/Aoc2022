class AoCSix : Exercise
{
    private readonly string _dataPath = "./data/aoc-six.txt";
    private int GetIndex(string input, int count)
    {
        var charMap = new Dictionary<char,int>();
        for (int i = 0; i < input.Length; i++)
        {
            if (charMap.Count == count) return i;
            if (charMap.ContainsKey(input[i]))
            {
                var jumpBackTo = charMap.GetValueOrDefault(input[i]);
                charMap = new Dictionary<char, int>();
                i = jumpBackTo;
                continue;
            }
            charMap.Add(input[i], i);
        }
        throw new Exception("unreachable");
    }

    public void Solve()
    {
        var data = File.ReadAllText(_dataPath);
        Console.WriteLine(GetIndex(data, 14));
    }
}