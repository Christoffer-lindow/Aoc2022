using System.Text;

class AoCThree : Exercise
{
    private string _dataUrl = "./data/aoc-three.txt";
    private Dictionary<char,int> _itemValues = new ()
    {
        {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6}, {'g', 7},
        {'h', 8}, {'i', 9}, {'j', 10}, {'k', 11}, {'l', 12}, {'m', 13}, {'n', 14},
        {'o', 15}, {'p', 16}, {'q', 17}, {'r', 18}, {'s', 19}, {'t', 20}, {'u', 21},
        {'v', 22}, {'w', 23}, {'x', 24}, {'y', 25}, {'z', 26}
    };

    private int GetCharacterValue(char c)
    {

        var baseVal = _itemValues.GetValueOrDefault(char.ToLower(c));
        return char.IsAsciiLetterUpper(c) ? 
            baseVal + _itemValues.Count : baseVal;
    }

    public List<string> LoadData()
    {
        List<string> lines = new();
        using(StreamReader file= new StreamReader(_dataUrl))
        {
            string? ln = "";
            while ((ln=file.ReadLine())  is not null) lines.Add(ln);
        }
        return lines;
    }

    private HashSet<char> GetUniqueItemsInRuckSack(string rucksack)
    {
        var uniqueCharacters = new HashSet<char>();
        rucksack.ToList().ForEach(c => uniqueCharacters.Add(c));
        return uniqueCharacters;
    }

    private List<HashSet<char>> UniqueCharacterInRuckSacs(List<string> rucksacks)
    {
        var collections = new List<HashSet<char>>();
        rucksacks.ForEach(rucksack => collections.Add(GetUniqueItemsInRuckSack(rucksack)));

        return collections;
    }

    private string ConstructUniqueItemsInRuckSacks(List<HashSet<char>> collections)
    {
        var builder = new StringBuilder();
        
        collections.ForEach(collection => 
        {
            foreach (var c in collection) builder.Append(c);
        });

        return builder.ToString();
    }

    private int CheckRuckSack(string rucksack, int num, bool splitRuckSack)
    {
        if (!splitRuckSack)
        {
            var occurances =new Dictionary<char,int>();

            foreach(char c in rucksack)
            {
                if (occurances.ContainsKey(c)) occurances[c] = occurances[c] + 1;
                else occurances[c] = 1;

                if (occurances[c] == num) return GetCharacterValue(c);
            }
            throw new Exception("unreachable");
        }

        var uniqueCharacters = new HashSet<char>();

        foreach (char c in SplitStringInMiddle(rucksack, true)) uniqueCharacters.Add(c);

        foreach (char c in SplitStringInMiddle(rucksack, false))
        {
            if (uniqueCharacters.Contains(c)) return GetCharacterValue(c);
        }

        throw new Exception("unreachable");
    }

    private string SplitStringInMiddle(string text, bool firstPart)
    {
        if (firstPart)
        {
            return text[0..(text.Length/2)];
        }
        return text[(text.Length / 2)..text.Length];
    }

    private List<string> Rucksacks(List<string> rucksacks, int start, int end)
    {
        var output = new List<string>();

        if (end == 1) output.Add(rucksacks[start]);
        else foreach (int i in Enumerable.Range(start, end)) output.Add(rucksacks[i]);
        
        return output;
    }
    public void Solve(int skip)
    {
        var score = 0;
        var data = LoadData();

        for (int i = 0; i <= data.Count -1; i= i+skip)
        {
            var input = Rucksacks(data, i, skip);
            if (skip == 1)
            {
                score += CheckRuckSack(input[0], 1,true);
            }
            else
            {
                var rucksacks = ConstructUniqueItemsInRuckSacks(UniqueCharacterInRuckSacs(input));
                score += CheckRuckSack(rucksacks, input.Count, false);
            }
            
        }
        Console.WriteLine(score);
    }

    public void Solve()
    {
        throw new NotImplementedException();
    }
}
