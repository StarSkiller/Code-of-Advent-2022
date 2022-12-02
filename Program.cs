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
    }

    private class TaskOne
    {
        private static readonly int Length = File.ReadAllLines("../../../taskone.txt").Length;

        private static readonly FileStream Fs = new("../../../taskone.txt", FileMode.Open);

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
        private static readonly int Length = File.ReadAllLines("../../../tasktwo.txt").Length;

        private static readonly FileStream Fs = new("../../../tasktwo.txt", FileMode.Open);

        private readonly StreamReader _read = new(Fs);

        private readonly Dictionary<char, char> losing = new();

        private readonly Dictionary<char, char> winning = new();

        public TaskTwo()
        {
            winning['X'] = 'C';
            winning['Y'] = 'A';
            winning['Z'] = 'B';

            losing['X'] = 'B';
            losing['Y'] = 'C';
            losing['Z'] = 'A';
        }

        private int GamePoints(char enemy, char played)
        {
            if (winning[played] == enemy) return 6;

            if (losing[played] == enemy) return 0;

            return 3;
        }

        public int CalcSumOfPlayed()
        {
            var sum = 0;
            for (var i = 1; i <= Length; i++)
            {
                var content = _read.ReadLine();
                switch (content[2])
                {
                    case 'X':
                        sum += 1;
                        break;
                    case 'Y':
                        sum += 2;
                        break;
                    case 'Z':
                        sum += 3;
                        break;
                }

                sum += GamePoints(content[0], content[2]);
            }

            return sum;
        }
    }
}