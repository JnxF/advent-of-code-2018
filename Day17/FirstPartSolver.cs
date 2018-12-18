using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day17
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
            bool[,] matrix = BuildMatrix(_input);
            return 0;
        }

        private bool[,] BuildMatrix(string input)
        {
            var lines = input
                .Replace("\r", "")
                .Split("\n")
                .Select(l => (l[0],
                    Regex.Split(l, @"\D+").Where(n => n != "").Select(n => int.Parse(n)).ToArray()
                )
            ).ToArray();
            var minI = lines.Where(l => l.Item1 == 'x').OrderBy(l => l.Item2[1]).First().Item2[1];
            minI = Math.Min(minI, lines.Where(l => l.Item1 == 'y').OrderBy(l => l.Item2[0]).First().Item2[0]);
            var maxI = lines.Where(l => l.Item1 == 'x').OrderByDescending(l => l.Item2[1]).First().Item2[2];
            var minJ = lines.Where(l => l.Item1 == 'y').OrderBy(l => l.Item2[1]).First().Item2[1];
            var maxJ = lines.Where(l => l.Item1 == 'y').OrderByDescending(l => l.Item2[1]).First().Item2[2];
            Console.WriteLine(minI + " " + maxI + " " + minJ + " " + maxJ);

            var _height = maxI + 1;
            var _width = maxI - minI + 3;

            var res = new bool[_height, _width];
            foreach ((var type, var values) in lines)
            {
                var points = new HashSet<(int, int)>();

                if (type == 'x')
                {
                    int j = values[0];
                    for (int i = values[1]; i <= values[2]; ++i)
                    {
                        points.Add((i, j));
                    }
                }
                if (type == 'y')
                {
                    int i= values[0];
                    for (int j = values[1]; j <= values[2]; ++j)
                    {
                        points.Add((i, j));
                    }
                }


                foreach (var point in points)
                {
                    int i = point.Item1;
                    int j = point.Item2 - minJ;
                    res[i, j] = true;
                }
            }

            for (int i = 0; i < _height; ++i)
            {
                for (int j = 0; j < _width; ++j)
                {
                    Console.Write(res[i, j] ? "#" : ".");
                }
                Console.WriteLine();
            }
            return default;
        }
    }
}
