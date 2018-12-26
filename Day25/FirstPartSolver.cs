using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day25
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
            var points = ParseInput(_input);
            int n = points.Count;
            var uf = new UnionFind(n);

            for (int i = 0; i < n; ++i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    var p1 = points[i];
                    var p2 = points[j];
                    if (Distance(p1, p2) <= 3)
                    {
                        uf.Union(i, j);
                    }
                }
            }

            return uf.Count;
        }

        private static int Distance(int[] p1, int[] p2)
        {
            return p1.Zip(p2, (a, b) => Math.Abs(a - b)).Sum();
        }

        private IList<int[]> ParseInput(string input)
        {
            return input.Replace("\r", "")
                .Split("\n")
                .Select(l =>
                    Regex.Split(l, @"[^-0-9]+").Where(n => n != "").Select(n => int.Parse(n)).ToArray()
                ).ToList();
        }

    }
}
