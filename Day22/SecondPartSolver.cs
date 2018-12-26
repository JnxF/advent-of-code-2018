using Helpers;
using System;
using System.Collections.Generic;
using Dijkstra.NET.Extensions;
using Dijkstra.NET.Model;
using System.Linq;

namespace Day22
{
    class SecondPartSolver : ISolver<int>
    {
        private const int Padding = 10;
        private readonly int _depth;
        private readonly int _targetX;
        private readonly int _targetY;
        private readonly static Dictionary<Region, char> RegionToChars = new Dictionary<Region, char>
        {
            {Region.Rocky, '.'},
            {Region.Wet, '='},
            {Region.Narrow, '|'}
        };

        public SecondPartSolver(int depth, int targetX, int targetY)
        {
            _depth = depth;
            _targetX = targetX;
            _targetY = targetY;
        }

        public int Solve()
        {
            var matrix = BuildMatrix(_depth, _targetX, _targetY);
            uint height = (uint)matrix.GetLength(0);
            uint width = (uint)matrix.GetLength(1);
            var graph = new Graph<(int, int, int), string>();


            for (var lev = 0; lev < 3; ++lev)
            {
                for (var i = 0; i < matrix.GetLength(0); ++i)
                {
                    for (var j = 0; j < matrix.GetLength(1); ++j)
                    {
                        graph.AddNode((i, j, lev));
                    }
                }
            }

            // [0, 1, 2] == [Torch, Any, Climb]

            for (var lev = 0; lev < 3; ++lev)
            {
                for (var i = 0; i < matrix.GetLength(0); ++i)
                {
                    for (var j = 0; j < matrix.GetLength(1); ++j)
                    {
                        // Connect with 7:
                        // Rocky: climb <-> torch = (0, 2)
                        // Wet: any <-> climb = (1, 2)
                        // Narrow: any <-> torch = (0, 1)
                        switch (matrix[i, j])
                        {
                            case Region.Rocky:
                                if (lev == 2)
                                    graph.Connect(ExtractCoords(i, j, 2, height, width),
                                        ExtractCoords(i, j, 0, height, width), 7, "");
                                else if (lev == 0)
                                    graph.Connect(ExtractCoords(i, j, 0, height, width),
                                        ExtractCoords(i, j, 2, height, width), 7, "");
                                break;
                            case Region.Wet:
                                if (lev == 2)
                                    graph.Connect(ExtractCoords(i, j, 1, height, width),
                                        ExtractCoords(i, j, 2, height, width), 7, "");
                                else if (lev == 1)
                                    graph.Connect(ExtractCoords(i, j, 2, height, width),
                                        ExtractCoords(i, j, 1, height, width), 7, "");
                                break;
                            default:
                                if (lev == 1)
                                    graph.Connect(ExtractCoords(i, j, 1, height, width),
                                        ExtractCoords(i, j, 0, height, width), 7, "");
                                else if (lev == 0)
                                    graph.Connect(ExtractCoords(i, j, 0, height, width),
                                        ExtractCoords(i, j, 1, height, width), 7, "");
                                break;
                        }

                        // Connect with 1:
                        // - Torch: narrow <-> rocky   = (0, 2)
                        // - Any:   wet <-> narrow     = (1, 2)
                        // - Climb: rocky <-> wet      = (0, 1)
                        foreach ((int I, int J) in ValidAdjacents(i, j, height, width))
                        {
                            var mio = (int)matrix[i, j];
                            var tuyo = (int)matrix[I, J];
                            var ambos = new HashSet<int> { mio, tuyo };

                            if (mio == tuyo)
                            {
                                graph.Connect(ExtractCoords(i, j, lev, height, width),
                                            ExtractCoords(I, J, lev, height, width), 1, "");
                            }

                            switch (lev)
                            {
                                // Torch
                                case 0:
                                    if (ambos.SetEquals(new HashSet<int> { 0, 2 }))
                                    {
                                        graph.Connect(ExtractCoords(i, j, lev, height, width),
                                            ExtractCoords(I, J, lev, height, width), 1, "");
                                    }
                                    break;

                                // Any
                                case 1:
                                    if (ambos.SetEquals(new HashSet<int> { 1, 2 }))
                                    {
                                        graph.Connect(ExtractCoords(i, j, lev, height, width),
                                            ExtractCoords(I, J, lev, height, width), 1, "");
                                    }
                                    break;

                                // Climb
                                default:
                                    if (ambos.SetEquals(new HashSet<int> { 0, 1 }))
                                    {
                                        graph.Connect(ExtractCoords(i, j, lev, height, width),
                                            ExtractCoords(I, J, lev, height, width), 1, "");
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            ShortestPathResult result = graph.Dijkstra(
                ExtractCoords(0, 0, 0, height, width),
                ExtractCoords(10, 10, 0, height, width));

            
            foreach (var k in result.GetPath())
            {
                Console.WriteLine(Transform((int) k, (int) height, (int) width));
            }

            Console.WriteLine(result.Distance);

            return 0;
        }

        private string Transform(int k, int height,int width)
        {
            var level = k / (height * width);
            var restante = k - (level * height * width);
            var i = restante / width;
            var j = restante - width * i;
            return $"{i},{j},{level}";
        }

        private static (int, int)[] ValidAdjacents(int i, int j, uint height, uint width)
        {
            var res = new HashSet<(int, int)>();
            var adjs = new (int, int)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
            foreach (var adj in adjs)
            {
                var I = i + adj.Item1;
                var J = j + adj.Item2;
                if (I >= 0 && J >= 0 && I < height && J < width && !(I == i && J == j))
                {
                    res.Add((I, J));
                }
            }
            return res.ToArray();
        }

        private static uint ExtractCoords(int i, int j, int lev, uint height, uint width)
        {
            return (uint)(height * width * lev + i * width + j);
        }

        private void PrintMatrix(Region[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < matrix.GetLength(1); ++j)
                {
                    Console.Write(RegionToChars[matrix[i, j]]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private Region[,] BuildMatrix(int depth, int targetX, int targetY)
        {
            var numeric = new int[targetY + 1 + Padding, targetX + 1 + Padding];
            for (var i = 0; i < numeric.GetLength(0); ++i)
            {
                for (var j = 0; j < numeric.GetLength(1); ++j)
                {
                    int res;
                    if (i == 0 && j == 0)
                    {
                        res = 0;
                    }
                    else if (i == targetY && j == targetX)
                    {
                        res = 0;
                    }
                    else if (i == 0)
                    {
                        res = 16807 * j;
                    }
                    else if (j == 0)
                    {
                        res = 48271 * i;
                    }
                    else
                    {
                        res = ((numeric[i, j - 1] + _depth) % 20183) * ((numeric[i - 1, j] + _depth) % 20183);
                    }
                    numeric[i, j] = res;
                }
            }

            var matRes = new Region[targetY + 1, targetX + 1];

            for (var i = 0; i < matRes.GetLength(0); ++i)
            {
                for (var j = 0; j < matRes.GetLength(1); ++j)
                {
                    matRes[i, j] = (Region)((numeric[i, j] + _depth) % 20183 % 3);
                }
            }

            return matRes;
        }
    }
}
