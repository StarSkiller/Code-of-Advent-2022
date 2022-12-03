using System.Data;

namespace Code_of_Advent_2022;

internal static class Program
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
            for (var i = 0; i < 3; i++)
            {
                sum += _numList.Max();
                _numList.Remove(_numList.Max());
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

        private static int FirstPoints(char c)
        {
            return c switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => throw new ArgumentException("Does not contain: 'X' 'Y' or 'Z'")
            };
        }

        private static int GamePoints(char enemy, char played)
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

        private int DictSubCalculation(char c, Dictionary<char, char> dict)
        {
            return c switch
            {
                'A' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'A').Key),
                'B' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'B').Key),
                'C' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'C').Key),
                _ => throw new ArgumentException("false parameter!")
            };
        }

        public int CalcSumOfPlayedAlt()
        {
            var sum = 0;
            foreach (var l in _content)
                switch (l[2])
                {
                    case 'X':
                        sum += DictSubCalculation(l[0], Lose);
                        sum += 0;
                        break;
                    case 'Y':
                        sum += DictSubCalculation(l[0], Draw);
                        sum += 3;
                        break;
                    case 'Z':
                        sum += DictSubCalculation(l[0], Win);
                        sum += 6;
                        break;
                }

            return sum;
        }
    }
}