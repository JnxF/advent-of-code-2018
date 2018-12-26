using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day24
{
    static class InputParser
    {
        public static Battle ParseInput(string input)
        {
            var lines = input
                .Replace("\r", "")
                .Split("\n\n");

            var system = lines[0].Split("\n").Skip(1).Select(l => LineToGroup(l)).ToList();
            var infect = lines[1].Split("\n").Skip(1).Select(l => LineToGroup(l)).ToList();

            return new Battle(system, infect);
        }

        private static Group LineToGroup(string line)
        {
            var res = Regex.Match(line, @"^(\d+) units each with (\d+) hit points (\(.+\) )?with an attack that does (\d+) (\w+) damage at initiative (\d+)$");

            var properties = new string(res.Groups[3].Value.Skip(1).SkipLast(2).ToArray());
            var parts = properties.Replace(",", "").Split("; ");

            var weaknesses = new HashSet<string>();
            var immunities = new HashSet<string>();

            foreach (var part in parts)
            {
                var items = part.Split(" ");
                var type = items[0];
                var goodItems = items.Skip(2);
                foreach (var it in goodItems)
                {
                    if (type == "immune")
                    {
                        immunities.Add(it);
                    }
                    else
                    {
                        weaknesses.Add(it);
                    }
                }
            }

            return new Group(int.Parse(res.Groups[1].Value),
                int.Parse(res.Groups[2].Value),
                int.Parse(res.Groups[4].Value),
                res.Groups[5].Value,
                int.Parse(res.Groups[6].Value),
                weaknesses,
                immunities);
        }
    }
}
