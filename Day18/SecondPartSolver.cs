using Helpers;
using System.Collections.Generic;

namespace Day18
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
            char[,] matrix = Common.BuildMatrix(_input);

            // Iterate 999 times
            for (int iteration = 1; iteration < 1000; ++iteration)
            {
                Common.Evolve(matrix);
            }

            // Keep track of the values since the iteration 1000
            List<int> values = new List<int>();
            for (int i = 1000; i < 1500; ++i)
            {
                Common.Evolve(matrix);
                values.Add(Common.ResourceValue(matrix));
            }

            // Search for the iteration in which 
            // the value of the 1000-ith iteration is repeated
            int toSearch = values[0];
            int loopLength = -1;
            for (int i = 1; loopLength == -1; ++i)
            {
                if (values[i] == toSearch)
                {
                    loopLength = i;
                }
            }

            int multiple = (1000 / loopLength) + 1;
            return values[multiple + (1000000000 % loopLength)];
        }
    }
}
