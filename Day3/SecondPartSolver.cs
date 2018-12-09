using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3
{
    class SecondPartSolver : ISolver<int>
    {
        private const int SIDE = 1000;

        private readonly string _input;

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var claims = _input.Split("\n").Select(line => ParseLine(line)).ToArray();

            int n = claims.Count();

            for (int i = 0; i < n; ++i)
            {
                if (TryWithClaim(i, claims))
                {
                    return i + 1;
                }
            }

            return int.MaxValue;
        }

        private bool TryWithClaim(int i, IList<Claim> claims)
        {
            var claim = claims[i];
            var matrix = new bool[SIDE, SIDE];
            var n = claims.Count();

            PutInitialClaim(claims[i], ref matrix);

            for (int j = 0; j < n; ++j)
            {
                if (i == j)
                {
                    continue;
                }
                if (!CanPlaceClaim(claims[j], ref matrix))
                {
                    return false;
                }
            }

            return true;
        }

        private static void PutInitialClaim(Claim claim, ref bool[,] matrix)
        {
            for (int i = claim.StartY; i < claim.StartY + claim.Height; ++i)
            {
                for (int j = claim.StartX; j < claim.StartX + claim.Width; ++j)
                {
                    matrix[i, j] = true;
                }
            }
        }

        private bool CanPlaceClaim(Claim claim, ref bool[,] matrix)
        {
            for (int i = claim.StartY; i < claim.StartY + claim.Height; ++i)
            {
                for (int j = claim.StartX; j < claim.StartX + claim.Width; ++j)
                {
                    if (matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
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
