using Helpers;
using System;
using System.Collections.Generic;

namespace Day22
{
    class FirstPartSolver : ISolver<int>
    {
        private readonly int _depth;
        private readonly int _targetX;
        private readonly int _targetY;
        private readonly static Dictionary<Region, char> RegionToChars = new Dictionary<Region, char>
        {
            {Region.Rocky, '.'},
            {Region.Wet, '='},
            {Region.Narrow, '|'}
        };

        public FirstPartSolver(int depth, int targetX, int targetY)
        {
            _depth = depth;
            _targetX = targetX;
            _targetY = targetY;
        }

        public int Solve()
        {
            var matrix = BuildMatrix(_depth, _targetX, _targetY);
            // PrintMatrix(matrix);
            // Sum of matrix values
            int tot = 0;
            foreach (var it in matrix)
            {
                tot += (int)it;
            }
            return tot;
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
            var numeric = new int[targetY + 1, targetX + 1];
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
