using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using ConcurrentPriorityQueue;

namespace Day7
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
            var graphLines = _input.Split("\n").Select(line => GraphCoordinatesFromLine(line)).ToArray();
            var graph = new Dictionary<char, ISet<char>>();
            var keys = new HashSet<char>();
            var entryDegree = new Dictionary<char, int>();
            foreach (var (x, y) in graphLines)
            {
                if (!graph.ContainsKey(x))
                {
                    graph.Add(x, new HashSet<char>());
                }
                graph[x].Add(y);

                if (!entryDegree.ContainsKey(y))
                {
                    entryDegree.Add(y, 0);
                }
                ++entryDegree[y];
                keys.Add(x);
                keys.Add(y);
            }

            var priorityQueue = new ConcurrentPriorityQueue<char, int>();

            foreach (char v in keys)
            {
                if (!entryDegree.ContainsKey(v))
                {
                    priorityQueue.Enqueue(v, -(v - 'A'));
                }
            }

            StringBuilder answer = new StringBuilder();

            while (priorityQueue.Count != 0)
            {
                char u = priorityQueue.Dequeue();
                answer.Append(u);
                if (graph.ContainsKey(u))
                    foreach (char v in graph[u])
                    {
                        --entryDegree[v];
                        if (entryDegree[v] == 0)
                        {
                            priorityQueue.Enqueue(v, -(v - 'A'));
                        }
                    }
            }
            return answer.ToString();

        }

        private (char, char) GraphCoordinatesFromLine(string line)
        {
            Regex expression = new Regex(@"Step (\w) must be finished before step (\w) can begin.");
            var matches = expression.Matches(line);

            return (
                char.Parse(matches[0].Groups[1].Value),
                char.Parse(matches[0].Groups[2].Value)
            );
        }
    }
}
