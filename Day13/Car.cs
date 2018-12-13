using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    class Car
    {
        public int i { get; set; }
        public int j { get; set; }
        public Direction Direction { get; set; }
        public char[,] matrix { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        private Turn _nextTurn = Turn.LEFT;
        private char Under => matrix[i, j];

        public override string ToString()
        {
            return $"{j},{i}";
        }

        public void move()
        {
            if (!ValidPosition(i, j))
            {
                Console.WriteLine("Error state");
            }
            if (Under == '+')
            {
                PerformIntersection();

            }
            else if (Constants.turningPipes.Contains(Under))
            {
                PerformTurn();
            }

            (var di, var dj) = Constants.directionsToPoints[Direction];
            int I = i + di;
            int J = j + dj;
            i = I;
            j = J;
        }

        private void PerformIntersection()
        {
            switch (Direction)
            {
                case Direction.UP:
                    switch (_nextTurn)
                    {
                        case Turn.LEFT:
                            Direction = Direction.LEFT;
                            break;
                        case Turn.RIGHT:
                            Direction = Direction.RIGHT;
                            break;
                    }
                    break;
                case Direction.RIGHT:
                    switch (_nextTurn)
                    {
                        case Turn.LEFT:
                            Direction = Direction.UP;
                            break;
                        case Turn.RIGHT:
                            Direction = Direction.DOWN;
                            break;
                    }
                    break;
                case Direction.LEFT:
                    switch (_nextTurn)
                    {
                        case Turn.LEFT:
                            Direction = Direction.DOWN;
                            break;
                        case Turn.RIGHT:
                            Direction = Direction.UP;
                            break;
                    }
                    break;
                case Direction.DOWN:
                    switch (_nextTurn)
                    {
                        case Turn.LEFT:
                            Direction = Direction.RIGHT;
                            break;
                        case Turn.RIGHT:
                            Direction = Direction.LEFT;
                            break;
                    }
                    break;
            }
            NextTurnDirection();
        }

        private void NextTurnDirection()
        {
            switch (_nextTurn)
            {
                case Turn.LEFT:
                    _nextTurn = Turn.STRAIGHT;
                    break;
                case Turn.STRAIGHT:
                    _nextTurn = Turn.RIGHT;
                    break;
                default:
                    _nextTurn = Turn.LEFT;
                    break;
            }
        }

        private void PerformTurn()
        {
            switch (Under)
            {
                case '/':
                    if (Direction == Direction.LEFT)
                    {
                        Direction = Direction.DOWN;
                    }
                    else if (Direction == Direction.UP)
                    {
                        Direction = Direction.RIGHT;
                    }
                    else if (Direction == Direction.RIGHT)
                    {
                        Direction = Direction.UP;
                    }
                    else if (Direction == Direction.DOWN)
                    {
                        Direction = Direction.LEFT;
                    }
                    break;
                case '\\':
                    if (Direction == Direction.LEFT)
                    {
                        Direction = Direction.UP;
                    }
                    else if (Direction == Direction.DOWN)
                    {
                        Direction = Direction.RIGHT;
                    }
                    else if (Direction == Direction.UP)
                    {
                        Direction = Direction.LEFT;
                    }
                    else if (Direction == Direction.RIGHT)
                    {
                        Direction = Direction.DOWN;
                    }
                    break;
            }
        }

        private bool ValidPosition(int i, int j)
        {
            return 0 <= i && i < height && 0 <= j && j < width && Constants.pipes.Contains(matrix[i, j]);
        }

        private enum Turn
        {
            LEFT, STRAIGHT, RIGHT
        }
    }
}
