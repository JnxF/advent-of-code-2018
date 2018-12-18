using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day15
{
    class FirstPartSolver : ISolver<string>
    {
        private readonly string _input;
        private const char WALL = '#';
        private const char EMPTY = '.';
        private const char GOBLIN = 'G';
        private const char ELF = 'E';

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public string Solve()
        {
            var board = new Board(_input);
            board.Display();
            for (int i = 0; i < 49; ++i)
            board.Iteration();
            return "";
        }

        private class Creature
        {
            public char Type { get; set;  }
            public int i { get; set; }
            public int j { get; set; }
            public (int, int) pos => (i, j);
            public int sorting => i * 500 + j;
            public int LifePoints { get; internal set; } = 200;
            public void Damage() => LifePoints = Math.Max(0, LifePoints - 3);
        }

        private class Board
        {
            private readonly int _width;
            private readonly int _height;
            private int _iteration;
            private ISet<Creature> _goblins;
            private ISet<Creature> _elfs;
            public readonly bool[,] _wall;
            private IEnumerable<Creature> _creatures => _goblins.Union(_elfs);

            public Board(string input)
            {
                _iteration = 0;
                _goblins = new HashSet<Creature>();
                _elfs = new HashSet<Creature>();

                var lines = input.Replace("\r", "").Split("\n");
                _width = lines[0].Length;
                _height = lines.Length;
                _wall = new bool[_height, _width];
                for (int i = 0; i < _height; ++i)
                {
                    for (int j = 0; j < _width; ++j)
                    {
                        var originalInput = lines[i][j];

                        switch (originalInput)
                        {
                            case GOBLIN:
                                _goblins.Add(new Creature
                                {
                                    Type = GOBLIN,
                                    i = i,
                                    j = j
                                });
                                break;

                            case ELF:
                                _elfs.Add(new Creature
                                {
                                    Type = ELF,
                                    i = i,
                                    j = j
                                });
                                break;

                            case WALL:
                                _wall[i, j] = originalInput == WALL;
                                break;
                        }
                    }
                }
            }

            public void Display()
            {
                Console.WriteLine($"Iteration {_iteration}");
                for (int i = 0; i < _height; ++i)
                {
                    for (int j = 0; j < _width; ++j)
                    {
                        char res = _wall[i, j] ? WALL : EMPTY;
                        if (_goblins.Any(g => g.pos == (i, j)))
                        {
                            res = GOBLIN;
                        }
                        if (_elfs.Any(e => e.pos == (i, j)))
                        {
                            res = ELF;
                        }
                        Console.Write(res);
                    }
                    Console.Write("   ");
                    var elfsHere = _elfs.Where(e => e.i == i);
                    var goblinsHere = _goblins.Where(g => g.i == i);
                    var everybody = elfsHere
                        .Union(goblinsHere)
                        .OrderBy(t => t.sorting)
                        .Select(t => t.Type + "(" + t.LifePoints + ")");

                    if (everybody.Count() != 0)
                    {
                        Console.WriteLine(everybody.Aggregate((a, x) => a + ", " + x));
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }

            public void Iteration()
            {
                foreach (var creature in _creatures.OrderBy(c => c.sorting))
                {
                    Turn(creature);
                }
                ++_iteration;
                Display();
            }

            private ISet<Creature> Enemies(char type)
            {
                return type == ELF ? _goblins : _elfs;
            }

            public void Turn(Creature c)
            {
                var enemies = Enemies(c.Type);

                // Try first to attack
                var adjsToAttack = enemies
                    .Where(e => Adjs2(c.pos).Contains(e.pos))
                    .OrderBy(e => e.LifePoints)
                    .ThenBy(e => e.sorting)
                    .ToArray();

                if (adjsToAttack.Any())
                {
                    var attacked = adjsToAttack.First();
                    attacked.Damage();
                    if (attacked.LifePoints == 0)
                    {
                        enemies.Remove(attacked);
                    }
                    return;
                }

                // Then to move
                
                var distance = BFS(c.pos);
                var closestGoblin = enemies
                    .Select(g => Adjs(g.pos))
                    .SelectMany(x => x)
                    .OrderBy(x => distance[x.Item1, x.Item2])
                    .ThenBy(x => x.Item1 * 500 + x.Item2);

                if (closestGoblin.Count() == 0)
                {
                    Console.WriteLine("returning");
                    return;
                }

                var distance2 = BFS(closestGoblin.First());
                var bds = Adjs(c.pos)
                    .Where(p => distance2[p.Item1, p.Item2] != -1)
                    .OrderBy(p => distance2[p.Item1, p.Item2])
                    .ThenBy(p => p.Item1 * 500 + p.Item2);

                if (!bds.Any())
                {
                    return;
                }

                var bestDirection = bds.First();
                
                if (false)
                for (int i = 0; i < _height; i++)
                {
                    for (int j = 0; j < _width; j++)
                    {
                        if (closestGoblin.First().Item1 == i && closestGoblin.First().Item2 == j)
                        {
                            Console.Write("X");
                        } else  if (bestDirection.Item1 == i && bestDirection.Item2 == j)
                        {
                            Console.Write("+");
                        }
                        else if (distance2[i, j] == -1)
                        {
                            Console.Write("N");
                        }
                        else if (distance2[i, j] >= 10)
                        {
                            Console.Write("^");
                        }
                        else
                            Console.Write(distance2[i, j]);
                    }
                    Console.WriteLine();

                }
                Console.WriteLine();


                c.i = bestDirection.Item1;
                c.j = bestDirection.Item2;
            }
            private (int, int)[] Adjs((int, int) pos)
            {
                bool Valid((int, int) p) =>
                    p.Item1 >= 0 &&
                    p.Item1 < _height &&
                    p.Item2 >= 0 &&
                    p.Item2 < _width &&
                    !_wall[p.Item1, p.Item2] &&
                    !_goblins.Any(g => g.pos == p) &&
                    !_elfs.Any(g => g.pos == p);
                    
                int i = pos.Item1;
                int j = pos.Item2;

                return new (int, int)[] {
                    (i + 1, j),
                    (i - 1, j),
                    (i, j + 1),
                    (i, j - 1),
                }.Where(x => Valid(x))
                .ToArray();
            }

            private (int, int)[] Adjs2((int, int) pos)
            {
                bool Valid((int, int) p) =>
                    p.Item1 >= 0 &&
                    p.Item1 < _height &&
                    p.Item2 >= 0 &&
                    p.Item2 < _width &&
                    !_wall[p.Item1, p.Item2];

                int i = pos.Item1;
                int j = pos.Item2;

                return new (int, int)[] {
                    (i + 1, j),
                    (i - 1, j),
                    (i, j + 1),
                    (i, j - 1),
                }.Where(x => Valid(x))
                .ToArray();
            }

            public int[,] BFS((int, int) pos)
            {
                var queue = new Queue<((int, int), int)>();
                var distance = new int[_height, _width];
                for (int i = 0; i < _height; i++)
                {
                    for (int j = 0; j < _width; j++)
                    {
                        distance[i, j] = -1;
                    }

                }
                var visited = new bool[_height, _width];
                queue.Enqueue((pos, 0));
                visited[pos.Item1, pos.Item2] = true;
                while (queue.Count != 0)
                {
                    var peek = queue.Dequeue();
                    (int i, int j) = peek.Item1;
                    visited[i, j] = true;
                    var myDistance = peek.Item2;
                    distance[i, j] = myDistance;
                    
                    foreach (var next in Adjs(peek.Item1))
                    {
                        if (!visited[next.Item1, next.Item2])
                        queue.Enqueue((next, myDistance + 1));
                    }
                }

                if (false)
                for (int i = 0; i < _height; i++)
                {
                    for (int j = 0; j < _width; j++)
                    {
                        Console.Write(distance[i, j]);
                    }
                    Console.WriteLine("  ");

                }
                return distance;
            }

        }
    }
}
