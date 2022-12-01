class AocOne : IExercise
{
    private string _data = "./data/aoc-one.txt";
    public void Solve()
    {
        int val = GetHighestOfLastN(1);
        Console.WriteLine(val);
    }

    private int GetHighestOfLastN(int n)
    {
        var numbers = new List<int>();
        
        using(StreamReader file= new StreamReader(_data)) 
        {
            string? ln;
            var currentCalories = 0;

            while ((ln=file.ReadLine())  is not null)
            {
                if (String.IsNullOrWhiteSpace(ln))
                {
                    numbers.Add(currentCalories);
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += int.Parse(ln);
                }
            }
        }

        numbers.Sort();
        return numbers.TakeLast(n).Sum();
    }
}