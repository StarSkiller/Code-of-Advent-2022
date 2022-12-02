namespace Code_of_Advent_2022
{
    class Program
    {
        private static void Main()
        {
            var one = new TaskOne();
            Console.WriteLine("Task 1:");
            Console.WriteLine("a) " + one.GetMax());
            Console.WriteLine("b) " + one.GetTopThreeMax());
        }

        class TaskOne
        {
            private static readonly int Length = File.ReadAllLines("../../../taskone.txt").Length;
            
            private static readonly FileStream Fs = new FileStream("../../../taskone.txt", FileMode.Open);

            private readonly HashSet<int> _numList = new HashSet<int>();

            private readonly StreamReader _read = new StreamReader(Fs);

            public TaskOne()
            {
                int sum = 0;
                for (int i = 1; i <= Length; i++)
                {
                    string line = _read.ReadLine();
                    if (line == "")
                    {
                        _numList.Add(sum);
                        sum = 0;
                    }
                    else
                    {
                        sum += Int32.Parse(line);
                    }
                }
            }

            public int GetMax()
            {
                return _numList.Max();
            }

            public int GetTopThreeMax()
            {
                int sum = 0;
                HashSet<int> localNumList = _numList;
                for (int i = 0; i < 3; i++)
                {
                    sum += localNumList.Max();
                    localNumList.Remove(localNumList.Max());
                }

                return sum;
            }
        }
        class TaskTwo
        {
            private static readonly int Length = File.ReadAllLines("../../../taskone.txt").Length;
            
            private static readonly FileStream Fs = new FileStream("../../../tasktwo.txt", FileMode.Open);

            private readonly StreamReader _read = new StreamReader(Fs);

            public TaskTwo()
            {
                int sum = 0;
                for (int i = 1; i <= Length; i++)
                {
                    string line = _read.ReadLine();
                    
                }
            }
        }
    }
}