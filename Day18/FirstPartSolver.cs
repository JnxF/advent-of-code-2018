using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            char[,] matrix = Common.BuildMatrix(_input);
            // Display(matrix);

            for (int iteration = 1; iteration <= 10; ++iteration)
            {
                // Console.WriteLine($"Iteration {iteration}");
                Common.Evolve(matrix);
                // Display(matrix);
                // Console.WriteLine($"At {iteration}, " + ResourceValue(matrix));
            }

            return Common.ResourceValue(matrix);
        }
    }
}
