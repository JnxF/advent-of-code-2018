using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    static class Common
    {
        private const char EMPTY = '.';
        private const char TREE = '|';
        private const char LUMBERYARD = '#';

        public static int ResourceValue(char[,] matrix)
        {
            var letters = matrix.Cast<char>();
            return letters.Where(c => c == TREE).Count() * letters.Where(c => c == LUMBERYARD).Count();
        }

        public static void Evolve(char[,] matrix)
        {
            char[,] old = (char[,]) matrix.Clone();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = EvolvePosition(i, j, old);
                }
            }
        }

        public static char EvolvePosition(int i, int j, char[,] oldStatus)
        {
            char oldChar = oldStatus[i, j];
            var points = new List<char>();
            for (int I = i - 1; I <= i + 1; ++I)
            {
                for (int J = j - 1; J <= j + 1; ++J)
                {
                    // Avoid central spot
                    if (I == i && J == j) continue;

                    // Avoid outside of the matrix
                    if (I < 0 || I >= oldStatus.GetLength(0) || J < 0 || J >= oldStatus.GetLength(1)) continue;

                    points.Add(oldStatus[I, J]);
                }
            }

            switch (oldChar)
            {
                case EMPTY:
                    if (points.Where(p => p == TREE).Count() >= 3) return TREE;
                    return oldChar;

                case TREE:
                    if (points.Where(p => p == LUMBERYARD).Count() >= 3) return LUMBERYARD;
                    return oldChar;

                default:
                    if (points.Where(p => p == LUMBERYARD).Any() && points.Where(p => p == TREE).Any()) return LUMBERYARD;
                    return EMPTY;
            }
        }

        public static void Display(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static char[,] BuildMatrix(string input)
        {
            var lines = input.Replace("\r", "").Split("\n");
            int height = lines.Length;
            int width = lines[0].Length;

            char[,] res = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    res[i, j] = lines[i][j];
                }
            }

            return res;
        }
    }
}
