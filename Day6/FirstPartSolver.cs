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
        private readonly string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var coordinates = _input.Split("\n").Select(line => PointFromLine(line)).ToArray();

           // foreach (var c in coordinates)
            // {
               //  c.IsInfinite = false;
            // }

            int maxI = coordinates.Select(i => i.Y).Max();
            int maxJ = coordinates.Select(i => i.X).Max();
            var chesses = new Chess[maxI + 1, maxJ + 1];
            for (int i = 0; i < chesses.GetLength(0); ++i)
            {
                for (int j = 0; j < chesses.GetLength(1); ++j)
                {
                    chesses[i, j] = new Chess
                    {
                        Distance = int.MaxValue,
                        BestFound = null,
                        Tie = false
                    };
                }
            }


            for (int i = 0; i < chesses.GetLength(0); ++i)
            {
                for (int j = 0; j < chesses.GetLength(1); ++j)
                {
                    foreach (var c in coordinates)
                    {
                        var current = chesses[i, j];
                        var man = Manhattan(c, i, j);
                        if (man < current.Distance)
                        {
                            current.Distance = man;
                            current.Tie = false;
                            current.BestFound = c;
                        }
                        else if (man == current.Distance)
                        {
                            current.Tie = true;
                        }
                    }

                    bool border = i == 0 || j == 0 || i == chesses.GetLength(0) - 1 || j == chesses.GetLength(1) - 1;
                    if (border)
                    {
                        chesses[i, j].BestFound.IsInfinite = true;
                    }
                }
            }

            // It's funny seeing how the matrix is :D
            /*
            using (var r = new StreamWriter("outputMatrix.txt"))
            {
                for (int i = 0; i < chesses.GetLength(0); ++i)
                {
                    for (int j = 0; j < chesses.GetLength(1); ++j)
                    {

                        if (coordinates.Where(c => c.X == j && c.Y == i).Count() != 0)
                        {
                            r.Write("XX ");
                        }
                        else
                        {
                            if (chesses[i, j].Tie)
                                r.Write("   ");
                            else
                            {
                                if ((chesses[i, j].BestFound.GetHashCode() % 100) < 10)
                                    r.Write(" ");
                                r.Write(chesses[i, j].BestFound.GetHashCode() % 100 + " ");

                            }
                        }

                    }
                    r.WriteLine();
                }
            }
            */

            var dict = new Dictionary<Point, int>();
            foreach (var c in chesses)
            {
                if (c.Tie) continue;
                if (!dict.ContainsKey(c.BestFound))
                {
                    dict.Add(c.BestFound, 0);
                }
                ++dict[c.BestFound];

            }

            var greatestArea = dict.Where(x => !x.Key.IsInfinite).OrderByDescending(x => x.Value).Select(x => x.Value).FirstOrDefault();
            return greatestArea;
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
            public int Distance { get; set; }

            public Point BestFound { get; set; }

            public bool Tie { get; set; }
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
