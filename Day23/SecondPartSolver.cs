using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day23
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
            var nanobots = ParseInput(_input);
            var bestNanobot = nanobots.OrderByDescending(n => n.Radius).First();
            return nanobots.Where(n => Distance(bestNanobot, n) <= bestNanobot.Radius).Count();
        }

        private IEnumerable<Nanobot> ParseInput(string input)
        {
            return input
                .Replace("\r", "")
                .Split("\n")
                .Select(l =>
                    Regex.Split(l, @"[^-0-9]+").Where(n => n != "").Select(n => long.Parse(n)).ToArray()
                )
                .Select(l =>
                    new Nanobot { X = l[0], Y = l[1], Z = l[2], Radius = l[3] }
                );
        }

        private static long Distance(Nanobot n1, Nanobot n2)
        {
            return Math.Abs(n1.X - n2.X) + Math.Abs(n1.Y - n2.Y) + Math.Abs(n1.Z - n2.Z);
        }

    }
}
