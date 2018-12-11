using Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day11
{
    class SecondPartSolver : ISolver<string>
    {
        private const int _input = 1308;

        public string Solve()
        {
            var matrix = new int[300, 300];
            for (int i = 0; i < 300; ++i)
            {
                for (int j = 0; j < 300; ++j)
                {
                    int X = j + 1;
                    int Y = i + 1;

                    int rackId = X + 10;
                    int powerLevel = rackId * Y;
                    powerLevel += _input;
                    powerLevel *= rackId;

                    int hundreds = (powerLevel / 100) % 10;
                    powerLevel = hundreds - 5;
                    matrix[i, j] = powerLevel;
                }
            }

            // X, Y, width, sum
            var results = new HashSet<(int, int, int, int)>();
            for (int width = 1; width <= 300; ++width)
            {
                (Point p, int sum) = FindMaxSumSubMatrix(matrix, width);
                results.Add((p.X - width + 2, p.Y - width + 2, width, sum));
            }

            return results.OrderByDescending(k => k.Item4).Select(k => $"{k.Item1},{k.Item2},{k.Item3}").FirstOrDefault();

        }

        // Extracted from https://www.techiedelight.com/find-maximum-sum-submatrix-in-given-matrix/
        // function to find maximum sum k x k sub-matrix
        private static (Point, int) FindMaxSumSubMatrix(int[,] mat, int k)
        {
            // pre-process the input matrix such that sum[i, j] stores
            // sum of elements in matrix from (0, 0) to (i, j)
            int[,] sum = new int[300, 300];

            sum[0, 0] = mat[0, 0];

            // pre-process first row
            for (int j = 1; j < 300; j++)
                sum[0, j] = mat[0, j] + sum[0, j - 1];

            // pre-process first column
            for (int i = 1; i < 300; i++)
                sum[i, 0] = mat[i, 0] + sum[i - 1, 0];

            // pre-process rest of the matrix
            for (int i = 1; i < 300; i++)
                for (int j = 1; j < 300; j++)
                    sum[i, j] = mat[i, j] + sum[i - 1, j] + sum[i, j - 1]
                        - sum[i - 1, j - 1];

            int total, max = int.MinValue;
            Point p = new Point { X = -1, Y = -1 };

            // find maximum sum sub-matrix

            // start from cell (k - 1, k - 1) and consider each
            // sub-matrix of size k x k
            for (int i = k - 1; i < 300; i++)
            {
                for (int j = k - 1; j < 300; j++)
                {
                    // Note (i, j) is bottom right corner coordinates of
                    // square sub-matrix of size k

                    total = sum[i, j];
                    if (i - k >= 0)
                        total = total - sum[i - k, j];
                    if (j - k >= 0)
                        total = total - sum[i, j - k];
                    if (i - k >= 0 && j - k >= 0)
                        total = total + sum[i - k, j - k];

                    if (total > max)
                    {
                        max = total;
                        p = new Point { Y = i, X = j };
                    }
                }
            }

            // returns coordinates of bottom right corner of sub-matrix
            return (p, max);
        }

    }
}
