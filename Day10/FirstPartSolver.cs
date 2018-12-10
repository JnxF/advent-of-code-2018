using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Day10
{
    class FirstPartSolver : ISolver<string>
    {
        private string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public string Solve()
        {
            var points = _input.Split("\n").Select(line => ParseLine(line)).ToArray();
            int SolvingIteration = 10813;

            using (StreamWriter writetext = new StreamWriter("solution.txt"))
            {
                for (int i = SolvingIteration - 10; i < SolvingIteration + 10; ++i)
                {
                    writetext.WriteLine($"ITERATION {i}");
                    var points2 = Copy(points);
                    writetext.Write(Display(points2, i));
                    writetext.WriteLine();
                }
            }

            return $"Puzzle solved after {SolvingIteration} seconds";

        }

        private Point[] Copy(Point[] points)
        {
            Point[] res = new Point[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                Point old = points[i];
                res[i] = new Point
                {
                    i = old.i,
                    j = old.j,
                    vi = old.vi,
                    vj = old.vj
                };
            }
            return res;
        }

        private string Display(Point[] points2, int iteration)
        {
            foreach (var p in points2)
            {
                p.i += p.vi * iteration;
                p.j += p.vj * iteration;
            }

            StringBuilder s = new StringBuilder();

            int minI = points2.Select(p => p.i).Min();
            int minJ = points2.Select(p => p.j).Min();
            int maxI = points2.Select(p => p.i).Max();
            int maxJ = points2.Select(p => p.j).Max();

            bool[,] matrix = new bool[maxI - minI + 1, maxJ - minJ + 1];
            foreach (var p in points2)
            {
                matrix[p.i - minI, p.j - minJ] = true;
            }

            for (int i = 0; i <= maxI - minI; ++i)
            {
                for (int j = 0; j <= maxJ - minJ; ++j)
                {
                    if (matrix[i, j]) s.Append("#");
                    else s.Append(" ");
                }
                s.AppendLine();
            }
            return s.ToString();
        }

        static Point ParseLine(string line)
        {
            Regex expression = new Regex(@"position=<\s*(-?\d+),\s*(-?\d+)> velocity=<\s*(-?\d+),\s*(-?\d+)>");
            var matches = expression.Matches(line);

            return new Point
            {
                j = int.Parse(matches[0].Groups[1].Value),
                i = int.Parse(matches[0].Groups[2].Value),
                vj = int.Parse(matches[0].Groups[3].Value),
                vi = int.Parse(matches[0].Groups[4].Value),
            };
        }

        class Point
        {
            public int i { get; set; }

            public int j { get; set; }

            public int vi { get; set; }

            public int vj { get; set; }

            public override string ToString()
            {
                return $"{i} {j} with {vi} {vj}";
            }
        }
    }
}
