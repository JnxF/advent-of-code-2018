using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Day6
{
    class FirstPartSolver : ISolver<int>
    {
        private const int MatrixSide = 4000;
        private string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var coordinates = _input.Split("\n").Select(line => PointFromLine(line)).ToArray();

            var dict = new Dictionary<int, int>();
            var matrix = new int?[MatrixSide, MatrixSide];
            for (int i = 0; i < MatrixSide; ++i)
            {
                for (int j = 0; j < MatrixSide; ++j)
                {
                    var res = Nearest(i, j, ref coordinates);
                    matrix[i, j] = res;

                    if (res == null) continue;

                    if (dict.ContainsKey(res.Value))
                    {
                        dict[res.Value]++;
                    }
                    else
                    {
                        dict[res.Value] = 1;
                    }
                }
            }

            foreach (var x in dict.OrderBy(i => i.Value))
            {
                Console.WriteLine(x.Value);
            }

            return 2;
        }

        private static int Distance(Point p1, Point p2)
        {
            return (int)(Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y));
        }

        private static int? Nearest(int i, int j, ref Point[] coordinates)
        {
            int minDist = int.MaxValue;
            int minDistId = 0;

            int? tie = null;
            Point p = new Point { Y = i, X = j };
            for (int w = 0; w < coordinates.Length; ++w)
            {
                int distance = Distance(p, coordinates[w]);


                if (distance < minDist)
                {
                    minDist = distance;
                    minDistId = w;
                }
                else if (distance == minDist)
                {
                    tie = distance;
                }

            }


            if (tie != null && tie == minDist)
            {
                return null;
            }
            else
            {
                return minDistId;
            }
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

        class Point
        {
            public int X { get; set; }

            public int Y { get; set; }
        }


    }
}
