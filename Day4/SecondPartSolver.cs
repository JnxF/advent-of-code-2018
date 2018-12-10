using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class SecondPartSolver : ISolver<int>
    {
        private const int MinutesInADay = 24 * 60;
        private readonly string _input;

        public SecondPartSolver(string input)
        {
            _input = input;
        }

        public int Solve()
        {
            var log = _input.Split("\n").Select(line => InfoFromLine(line)).OrderBy(inf => inf.Date);

            var fromIdToOccurencesPerMinute = new Dictionary<int, int[]>();

            int? currentId = null;
            Information last = null;

            foreach (var logEntry in log)
            {
                if (currentId == null)
                {
                    currentId = logEntry.GuardId;
                }

                if (logEntry.Type == InformationType.WAKE_UP)
                {
                    if (!fromIdToOccurencesPerMinute.ContainsKey(currentId.Value))
                    {
                        fromIdToOccurencesPerMinute.Add(currentId.Value, new int[MinutesInADay]);
                    }
                    int beginSleep = last.Hour * 24 + last.Minute;
                    int endSleep = logEntry.Hour * 24 + logEntry.Minute;

                    for (int i = beginSleep; i != endSleep; i = (i + 1) % MinutesInADay)
                    {
                        fromIdToOccurencesPerMinute[currentId.Value][i]++;
                    }
                }

                else if (logEntry.Type != InformationType.SLEEP)
                {
                    // We have finished with the previous guard
                    currentId = logEntry.GuardId;
                }

                last = logEntry;
            }

            var mostSleepyOfEveryone = fromIdToOccurencesPerMinute.OrderByDescending(item => item.Value.Max()).Select(i => i.Key).FirstOrDefault();

            // Argmax over fromIdToOccurencesPerMinute[mostSleepyofEveryone]
            int mostSleepyMinute = 0;
            int maxSlept = 0;
            for (int i = 0; i < fromIdToOccurencesPerMinute[mostSleepyOfEveryone].Length; ++i)
            {
                if (fromIdToOccurencesPerMinute[mostSleepyOfEveryone][i] > maxSlept)
                {
                    mostSleepyMinute = i;
                    maxSlept = fromIdToOccurencesPerMinute[mostSleepyOfEveryone][i];
                }
            }

            return mostSleepyOfEveryone * mostSleepyMinute;
        }

        private Information InfoFromLine(string line)
        {
            Regex expression = new Regex(@"\[(\d+)-(\d+)-(\d+) (\d\d):(\d\d)\] (.+)$");
            var matches = expression.Matches(line)[0];

            var year = int.Parse(matches.Groups[1].Value);
            var month = int.Parse(matches.Groups[2].Value);
            var day = int.Parse(matches.Groups[3].Value);
            var hour = int.Parse(matches.Groups[4].Value);
            var minute = int.Parse(matches.Groups[5].Value);

            string rest = matches.Groups[6].Value;
            InformationType informationType;
            int? guardId = null;

            if (line.Contains("wakes"))
            {
                informationType = InformationType.WAKE_UP;
            }
            else if (line.Contains("falls"))
            {
                informationType = InformationType.SLEEP;
            }
            else
            {
                informationType = InformationType.BEGIN;
                Regex expression2 = new Regex(@"Guard #(\d+) begins shift");
                var matches2 = expression2.Matches(line)[0];
                guardId = int.Parse(matches2.Groups[1].Value);
            }

            return new Information
            {
                Date = new DateTime(year, month, day, hour, minute, 0),
                Type = informationType,
                GuardId = guardId,
                Month = month,
                Day = day,
                Hour = hour,
                Minute = minute
            };
        }
    }
}
