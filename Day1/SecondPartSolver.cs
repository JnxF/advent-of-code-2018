using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class SecondPartSolver : ISolver<int>
    {
        private readonly string _input;

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var numbers = _input.Split("\n").Select(x => Cleaner.CleanNumberString(x));
            var seenFrequencies = new HashSet<int>();
            var n = numbers.Count();
            var frequency = 0;

            for (int i = 0; ; i = (i + 1) % n)
            {
                var number = numbers.ElementAt(i);
                frequency += number;

                if (seenFrequencies.Contains(frequency))
                {
                    return frequency;
                }
                else
                {
                    seenFrequencies.Add(frequency);
                }
            }
        }
    }
}
