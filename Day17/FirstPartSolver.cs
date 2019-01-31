using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day17
{
    class FirstPartSolver : ISolver<int>
    {
        private const char Water = '|';
        private const char SittingWater = '~';
        private const char Wall = '#';
        private const char Empty = '.';

        private readonly string _input;
        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            char[,] matrix = BuildMatrix(_input, out var waterSource);
            for (int rep = 0; rep < 1000; ++rep)
            {
                Flow(matrix, 0, waterSource);
                //PrintMatrix(matrix);
                // System.Threading.Thread.Sleep(1000);
                //Console.Clear();
                Console.WriteLine("it " + rep);
            }
            // PrintMatrix(matrix);
            var k = new Dictionary<char, int>();

            foreach (var t in matrix)
            {
                if (!k.ContainsKey(t)) k[t] = 0;
                k[t]++;
            }

           return k[Water] + k[SittingWater] - 1;
        }

        private static bool Passable(char c)
        {
            return c == Water || c == Empty;
        }

        private void Flow(char[,] matrix, int i, int j)
        {
            if (i < 0 || j < 0 || i >= matrix.GetLength(0) || j >= matrix.GetLength(1))
            {
                return;
            }
            if (!Passable(matrix[i, j]))
            {
                return;
            }

            while (Passable(matrix[i, j]))
            {
                if (matrix[i, j] == Empty)
                {
                    matrix[i, j] = Water;
                }
                ++i;
                if (i >= matrix.GetLength(0))
                {
                    return;
                }
            }
            --i;

            int jLeft = j;
            while (Passable(matrix[i, jLeft]) && !Passable(matrix[i + 1, jLeft]) && jLeft >= 0)
            {
                --jLeft;
            }
            bool fallLeft = Passable(matrix[i + 1, jLeft]);

            int jRight = j;
            while (Passable(matrix[i, jRight]) && !Passable(matrix[i + 1, jRight]) && jRight < matrix.GetLength(1))
            {
                ++jRight;
            }
            bool fallRight = Passable(matrix[i + 1, jRight]);

            if (fallLeft)
            {
                for (int p = jLeft + 1; p < jRight; ++p)
                {
                    matrix[i, p] = Water;
                }
                Flow(matrix, i, jLeft);
            }
            if (fallRight)
            {
                for (int p = jLeft + 1; p <= jRight; ++p)
                {
                    matrix[i, p] = Water;
                }
                Flow(matrix, i, jRight);
            }

            if (!fallRight && !fallLeft)
            {
                for (int p = jLeft + 1; p < jRight; ++p)
                {
                    matrix[i, p] = SittingWater;
                }
            }
        }

        private void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        private char[,] BuildMatrix(string input, out int waterSource)
        {
            var lines = input
                .Replace("\r", "")
                .Split("\n")
                .Select(l => (l[0],
                    Regex.Split(l, @"\D+").Where(n => n != "").Select(n => int.Parse(n)).ToArray()
                )
            ).ToHashSet();

            var minJ = int.MaxValue;
            var maxJ = int.MinValue;
            var minI = int.MaxValue;
            var maxI = int.MinValue;

            foreach ((char c, int[] k) in lines)
            {
                if (c == 'x')
                {
                    minJ = Math.Min(minJ, k[0]);
                    maxJ = Math.Max(maxJ, k[0]);

                    minI = Math.Min(minI, k[1]);
                    maxI = Math.Max(maxI, k[2]);
                }
                else
                {
                    minI = Math.Min(minI, k[0]);
                    maxI = Math.Max(maxI, k[0]);

                    minJ = Math.Min(minJ, k[1]);
                    maxJ = Math.Max(maxJ, k[2]);
                }
            }

            var _height = maxI + 1;
            var _width = (maxJ - minJ + 1) + 2;

            waterSource = 500 - minJ + 1;

            var res = new char[_height, _width];
            for (int i = 0; i < res.GetLength(0); ++i)
            {
                for (int j = 0; j < res.GetLength(1); ++j)
                {
                    res[i, j] = Empty;
                }
            }

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
                else
                {
                    int i = values[0];
                    for (int j = values[1]; j <= values[2]; ++j)
                    {
                        points.Add((i, j));
                    }
                }

                foreach (var point in points)
                {
                    int i = point.Item1;
                    int j = point.Item2 - minJ + 1;
                    res[i, j] = Wall;
                }
            }

            return res;
        }
    }
}