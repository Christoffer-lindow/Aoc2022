class AocFive : Exercise
{
    private readonly string _dataPath = "./data/aoc-five.txt";

    private (List<Instruction>, List<Stack<char>>) ParseData()
    {
        var parseStacks = true;

        var instructions =  new List<Instruction>();
        var stacks = new List<Stack<char>>();

        var chars = new List<List<char>>();
        foreach(var line in File.ReadAllLines(_dataPath))
        {
            if (line.Length >= 3 && line[1] == '1') continue;
            if (string.IsNullOrEmpty(line)) 
            {
                parseStacks = false;
                continue;
            }

            if (!parseStacks) {
                instructions.Add(ParseToInstruction(line));
                continue;
            }

            chars.Add(ParseToCharArr(line.ToString()));
        }

        chars.Reverse();

        for (int i = 0; i < chars[0].Count; i++)
        {
            stacks.Add(new Stack<char>());
        }

        chars.ForEach(stackAsChars => {
            for (int i = 0; i < stackAsChars.Count; i++)
            {
                if(!char.IsWhiteSpace(stackAsChars[i])) stacks[i].Push(stackAsChars[i]);
            }
        });

        return (instructions, stacks);
    }
    
    private List<char> ParseToCharArr(string line)
    {
        var chars = new List<char>();
        for (int i = 0; i < line.Length-1; i++)
        {
            if (i == 0 || i % 4 == 0)
            {
                i++;
                chars.Add(line[i]);
                i++;
            }
        }
        return chars;
    }

    private Instruction ParseToInstruction(string instruction)
    {
        int? move = null;
        int? from = null;
        int? to = null;
        for (int i = 0; i < instruction.Length; i++)
        {
            if (char.IsAsciiLetter(instruction[i]) || char.IsWhiteSpace(instruction[i])) continue;

            if (move is null) 
            {
                if (char.IsNumber(instruction[i+1]))
                {
                    move = int.Parse(new String(new char[] {instruction[i], instruction[i+1]}));
                    i = i+2;
                    continue;
                }
                move = int.Parse(instruction[i].ToString());
                continue;
            }

            if (from is null) 
            {
                from = int.Parse(instruction[i].ToString());
                continue;
            }

            to = int.Parse(instruction[i].ToString());
        }
        

        return new Instruction(move, from, to);
    }

    private void PerformInstruction(Instruction instruction, List<Stack<char>> stacks)
    {
        var chars = new List<char>();
        for (int i = 0; i < instruction.Move; i++)
        {
            if (instruction.From is not null && instruction.To is not null)
            {
                chars.Add(stacks[(int) instruction.From-1].Pop());
            }
        }
        chars.Reverse();
        foreach(char c in chars)
        {
            if (instruction.To is not null) stacks[(int) instruction.To-1].Push(c);
        }
    }

    private record Instruction(int? Move, int? From, int? To);

    public void Solve()
    {
        (var instructions, var stacks) = ParseData();


        instructions.ForEach(instruction => PerformInstruction(instruction, stacks));
        stacks.ForEach(stack => {
            Console.Write(stack.Peek());
        });
    }
}