using System.Data;

namespace Code_of_Advent_2022;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Task 1:");
        Console.WriteLine("a) " + TaskOne.GetMax());
        Console.WriteLine("b) " + TaskOne.GetTopThreeMax());
        Console.WriteLine("");
        Console.WriteLine("Task 2:");
        Console.WriteLine("a) " + TaskTwo.CalcSumOfPlayed());
        Console.WriteLine("b) " + TaskTwo.CalcSumOfPlayedAlt());
        Console.WriteLine("");
        Console.WriteLine("Task 3:");
        Console.WriteLine("a) " + TaskThree.CalculateFinalOne());
        Console.WriteLine("b) " + TaskThree.CalculateFinalTwo());
    }

    private static class TaskOne
    {
        private static readonly int Length = File.ReadAllLines("../../../taskOne.txt").Length;

        private static readonly FileStream Fs = new("../../../taskOne.txt", FileMode.Open);

        private static readonly HashSet<int> NumList = new();

        private static readonly StreamReader Read = new(Fs);

        static TaskOne()
        {
            var sum = 0;
            for (var i = 1; i <= Length; i++)
            {
                var line = Read.ReadLine();
                if (line == "")
                {
                    NumList.Add(sum);
                    sum = 0;
                }
                else
                {
                    if (line == null) throw new NoNullAllowedException();
                    sum += int.Parse(line);
                }
            }
        }

        public static int GetMax()
        {
            return NumList.Max();
        }

        public static int GetTopThreeMax()
        {
            var sum = 0;
            for (var i = 0; i < 3; i++)
            {
                sum += NumList.Max();
                NumList.Remove(NumList.Max());
            }

            return sum;
        }
    }

    private static class TaskTwo
    {
        private static readonly Dictionary<char, char> Lose = new();

        private static readonly Dictionary<char, char> Win = new();

        private static readonly Dictionary<char, char> Draw = new();
        private static readonly string[] Content = File.ReadAllLines("../../../taskTwo.txt");

        static TaskTwo()
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

        public static int CalcSumOfPlayed()
        {
            var sum = 0;
            foreach (var l in Content)
            {
                sum += FirstPoints(l[2]);
                sum += GamePoints(l[0], l[2]);
            }

            return sum;
        }

        private static int DictSubCalculation(char c, Dictionary<char, char> dict)
        {
            return c switch
            {
                'A' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'A').Key),
                'B' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'B').Key),
                'C' => FirstPoints(dict.FirstOrDefault(x => x.Value == 'C').Key),
                _ => throw new ArgumentException("false parameter!")
            };
        }

        public static int CalcSumOfPlayedAlt()
        {
            var sum = 0;
            foreach (var l in Content)
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

    private static class TaskThree
    {
        private static readonly string[] File = System.IO.File.ReadAllLines("../../../taskThree.txt");

        private static string ExtractCompartment(int choice, string str)
        {
            return choice == 0 ? str[..(str.Length / 2)] : str.Substring(str.Length / 2, str.Length / 2);
        }

        private static char FindOutCommonLetter(IReadOnlyList<string> t)
        {
            foreach (var l in t[0].Where(l => t[1].Contains(l) && t[2].Contains(l))) return l;

            throw new Exception("no common in:\n" + t[0] + "\n" + t[1] + "\n" + t[2]);
        }

        private static int CheckPriority(char p)
        {
            if (p > 96 && p < 123) return p - 96;
            return p - 38;
        }

        public static int CalculateFinalOne()
        {
            var sum = 0;
            foreach (var line in File)
            foreach (var l in ExtractCompartment(0, line)
                         .Where(l => ExtractCompartment(1, line)
                             .Contains(l)))
            {
                sum += CheckPriority(l);
                break;
            }

            return sum;
        }

        public static int CalculateFinalTwo()
        {
            var t = new List<string>();
            var sum = 0;
            foreach (var line in File)
            {
                if (t.Count == 3)
                {
                    sum += CheckPriority(FindOutCommonLetter(t));
                    t.Clear();
                }

                t.Add(line);
            }

            if (t.Count <= 0) return sum;
            sum += CheckPriority(FindOutCommonLetter(t));
            t.Clear();

            return sum;
        }
    }
}