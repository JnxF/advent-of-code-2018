using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Day16
{
    static class InputParsers
    {
        public static HashSet<Description> ParseDescriptions(string input)
        {
            var lines = input.Split("\n");
            var res = new HashSet<Description>();
            for (int i = 0; i < lines.Length; i += 4)
            {
                var before = Regex.Split(lines[i], @"\D+").Where(n => n != "").Select(n => int.Parse(n)).ToArray();
                var instruction = Regex.Split(lines[i + 1], @"\D+").Where(n => n != "").Select(n => int.Parse(n)).ToArray();
                var after = Regex.Split(lines[i + 2], @"\D+").Where(n => n != "").Select(n => int.Parse(n)).ToArray();
                res.Add(new Description
                {
                    Before = before,
                    Instruction = instruction,
                    After = after,
                });
            }
            return res;
        }

        public static IEnumerable<int[]> ParseInstructions(string input2)
        {
            int[] LineToArrayOfInts(string l) => Regex
                .Split(l, @"\D+")
                .Where(n => n != "")
                .Select(i => int.Parse(i))
                .ToArray();

            return input2
             .Split("\n")
             .Select(l => LineToArrayOfInts(l));
        }
    }
}
