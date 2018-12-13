using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day13
{
    class FirstPartSolver : ISolver<string>
    {
        private readonly string _input;

        public FirstPartSolver(string input)
        {
            _input = input;
        }

        public string Solve()
        {
            var lines = _input.Replace("\r", "").Split("\n");
            int width = lines[0].Length;
            int height = lines.Length;

            var matrix = new char[height, width];
            var cars = new List<Car>();
            BuildMatrix(lines, cars, height,width, ref matrix);

            for (int iteration = 0; ; ++iteration)
            {
                var sortedCars = cars.OrderBy(c => c.i * width + c.j);
                foreach (var car in sortedCars)
                {
                    car.move();

                    var coinciders = Coinciders(sortedCars.ToList());
                    if (coinciders != null)
                    {
                        var car1 = coinciders.Value.Item1;
                        return car1.ToString();
                    }
                }
            }
        }

        private (Car, Car)? Coinciders(List<Car> cars)
        {
            for (int i = 0; i < cars.Count(); ++i)
            {
                for (int j = i + 1; j < cars.Count(); ++j)
                {
                    if (cars[i].i == cars[j].i && cars[i].j == cars[j].j)
                    {
                        return (cars[i], cars[j]);
                    }
                }
            }
            return null;
        }

        private void BuildMatrix(string[] lines, List<Car> cars, int height, int width, ref char[,] matrix)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char val = lines[i][j];
                    if (Constants.carChars.Contains(val))
                    {
                        cars.Add(new Car {
                            i = i,
                            j = j,
                            Direction = Constants.directions[val],
                            matrix = matrix,
                            height = height,
                            width = width
                        });
                        matrix[i, j] = Constants.carsToPipes[val];
                    }
                    else
                    {
                        matrix[i, j] = lines[i][j];
                    }
                }
            }
        }

        private void Display(char[,] matrix, List<Car> cars)
        {
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var car = cars.Where(c => c.i == i && c.j == j).FirstOrDefault();
                    if (car != null)
                    {
                        Console.Write(Constants.directionsToCarChars[car.Direction]);
                    }
                    else
                    {
                        Console.Write(matrix[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
