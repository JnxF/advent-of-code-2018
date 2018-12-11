using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class FirstPartSolver : ISolver<string>
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

            var res = new int[298, 298];
            for (int i = 0; i < 298; ++i)
            {
                for (int j = 0; j < 298; j++)
                {
                    res[i, j] = Convolution(matrix, i, j);
                }
            }

            int max = int.MinValue;
            int meX = -1;
            int meY = -1;
            for (int i = 0; i < 298; ++i)
            {
                for (int j = 0; j < 298; j++)
                {
                    if (res[i, j] > max)
                    {
                        max = res[i, j];
                        meX = j + 1;
                        meY = i + 1;
                    }
                }
            }

            return $"{meX},{meY}";
        }

        private int Convolution(int[,] matrix, int i, int j)
        {
            int tot = 0;
            for (int I = i; I < i + 3; ++I)
            {
                for (int J = j; J < j + 3; ++J)
                {
                    tot += matrix[I, J];
                }
            }
            return tot;
        }
    }
}
