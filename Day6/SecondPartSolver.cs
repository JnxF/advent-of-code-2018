using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Day6
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
            var coordinates = _input.Split("\n").Select(line => PointFromLine(line)).ToArray();

            int maxI = coordinates.Select(i => i.Y).Max();
            int maxJ = coordinates.Select(i => i.X).Max();
            var chesses = new Chess[maxI + 1, maxJ + 1];
            for (int i = 0; i < chesses.GetLength(0); ++i)
            {
                for (int j = 0; j < chesses.GetLength(1); ++j)
                {
                    chesses[i, j] = new Chess
                    {
                        Total = 0
                    };
                }
            }


            int safeSpots = 0;
            for (int i = 0; i < chesses.GetLength(0); ++i)
            {
                for (int j = 0; j < chesses.GetLength(1); ++j)
                {
                    foreach (var c in coordinates)
                    {
                        chesses[i, j].Total += Manhattan(c, i, j);
                    }
                    if (chesses[i, j].Total < 10000) ++safeSpots;
                }

            }

            return safeSpots;
        }

        private static int Manhattan(Point p, int i, int j)
        {
            int abs(int k) => k > 0 ? k : -k;
            return abs(p.X - j) + abs(p.Y - i);
        }

        private static Point PointFromLine(string line)
        {
            Regex expression = new Regex(@"(\d+), (\d+)");
            var matches = expression.Matches(line);

            return new Point
            {
                X = int.Parse(matches[0].Groups[1].Value),
                Y = int.Parse(matches[0].Groups[2].Value),
            };
        }

        class Chess
        {
            public int Total { get; set; }
        }

        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public bool IsInfinite { get; set; }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }


    }
}
