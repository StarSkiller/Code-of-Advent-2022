using System.Data;

namespace Code_of_Advent_2022;

internal class Program
{
    private static void Main()
    {
        var one = new TaskOne();
        var two = new TaskTwo();
        Console.WriteLine("Task 1:");
        Console.WriteLine("a) " + one.GetMax());
        Console.WriteLine("b) " + one.GetTopThreeMax());
        Console.WriteLine("");
        Console.WriteLine("Task 2:");
        Console.WriteLine("a) " + two.CalcSumOfPlayed());
        Console.WriteLine("b) " + two.CalcSumOfPlayedAlt());
    }

    private class TaskOne
    {
        private static readonly int Length = File.ReadAllLines("../../../taskOne.txt").Length;

        private static readonly FileStream Fs = new("../../../taskOne.txt", FileMode.Open);

        private readonly HashSet<int> _numList = new();

        private readonly StreamReader _read = new(Fs);

        public TaskOne()
        {
            var sum = 0;
            for (var i = 1; i <= Length; i++)
            {
                var line = _read.ReadLine();
                if (line == "")
                {
                    _numList.Add(sum);
                    sum = 0;
                }
                else
                {
                    if (line == null) throw new NoNullAllowedException();
                    sum += int.Parse(line);
                }
            }
        }

        public int GetMax()
        {
            return _numList.Max();
        }

        public int GetTopThreeMax()
        {
            var sum = 0;
            var localNumList = _numList;
            for (var i = 0; i < 3; i++)
            {
                sum += localNumList.Max();
                localNumList.Remove(localNumList.Max());
            }

            return sum;
        }
    }

    private class TaskTwo
    {
        private static readonly Dictionary<char, char> Lose = new();

        private static readonly Dictionary<char, char> Win = new();

        private static readonly Dictionary<char, char> Draw = new();
        private readonly string[] _content = File.ReadAllLines("../../../taskTwo.txt");

        public TaskTwo()
        {
            Win['X'] = 'C';
            Win['Y'] = 'A';
            Win['Z'] = 'B';

            Lose['X'] = 'B';
            Lose['Y'] = 'C';
            Lose['Z'] = 'A';

            Draw['X'] = 'A';
            Draw['Y'] = 'B';
            Draw['Z'] = 'C';
        }

        private int FirstPoints(char c)
        {
            switch (c)
            {
                case 'X':
                    return 1;
                case 'Y':
                    return 2;
                case 'Z':
                    return 3;
            }

            throw new ArgumentException("Does not contain: 'X' 'Y' or 'Z'");
        }

        private int GamePoints(char enemy, char played)
        {
            if (Win[played] == enemy) return 6;

            return Lose[played] == enemy ? 0 : 3;
        }

        public int CalcSumOfPlayed()
        {
            var sum = 0;
            foreach (var l in _content)
            {
                sum += FirstPoints(l[2]);
                sum += GamePoints(l[0], l[2]);
            }

            return sum;
        }

        public int CalcSumOfPlayedAlt()
        {
            var sum = 0;
            foreach (var l in _content)
                switch (l[2])
                {
                    case 'X':
                        switch (l[0])
                        {
                            case 'A':
                                sum += FirstPoints(Lose.FirstOrDefault(x => x.Value == 'A').Key);
                                break;
                            case 'B':
                                sum += FirstPoints(Lose.FirstOrDefault(x => x.Value == 'B').Key);
                                break;
                            case 'C':
                                sum += FirstPoints(Lose.FirstOrDefault(x => x.Value == 'C').Key);
                                break;
                        }

                        sum += 0;
                        break;
                    case 'Y':
                        switch (l[0])
                        {
                            case 'A':
                                sum += FirstPoints(Draw.FirstOrDefault(x => x.Value == 'A').Key);
                                break;
                            case 'B':
                                sum += FirstPoints(Draw.FirstOrDefault(x => x.Value == 'B').Key);
                                break;
                            case 'C':
                                sum += FirstPoints(Draw.FirstOrDefault(x => x.Value == 'C').Key);
                                break;
                        }

                        sum += 3;
                        break;
                    case 'Z':
                        switch (l[0])
                        {
                            case 'A':
                                sum += FirstPoints(Win.FirstOrDefault(x => x.Value == 'A').Key);
                                break;
                            case 'B':
                                sum += FirstPoints(Win.FirstOrDefault(x => x.Value == 'B').Key);
                                break;
                            case 'C':
                                sum += FirstPoints(Win.FirstOrDefault(x => x.Value == 'C').Key);
                                break;
                        }

                        sum += 6;
                        break;
                }

            return sum;
        }
    }
}