using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3
{
    class FirstPartSolver : ISolver<int>
    {
        private const int SIDE = 1000;

        private readonly string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var matrix = new int[SIDE, SIDE];
            IEnumerable<Claim> claims = _input.Split("\n").Select(line => ParseLine(line));

            foreach (var claim in claims)
            {
                PutClaimInMatrix(claim, ref matrix);
            }

            var total = 0;
            foreach (var box in matrix)
            {
                if (box > 1)
                {
                    ++total;
                }
            }
            return total;

        }

        private static void PutClaimInMatrix(Claim claim, ref int[,] matrix)
        {
            for (int i = claim.StartY; i < claim.StartY + claim.Height; ++i)
            {
                for (int j = claim.StartX; j < claim.StartX + claim.Width; ++j)
                {
                    ++matrix[i, j];
                }
            }
        }

        private static Claim ParseLine(string line)
        {
            Regex expression = new Regex(@"#\d+ @ (\d+),(\d+): (\d+)x(\d+)");
            var matches = expression.Matches(line);

            var Claim = new Claim
            {
                StartX = int.Parse(matches[0].Groups[1].Value),
                StartY = int.Parse(matches[0].Groups[2].Value),
                Width = int.Parse(matches[0].Groups[3].Value),
                Height = int.Parse(matches[0].Groups[4].Value),
            };

            return Claim;
        }
    }
}
