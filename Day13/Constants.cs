using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day13
{
    static class Constants
    {
        public static readonly ISet<char> carChars = new HashSet<char> { '^', 'v', '<', '>' };
        public static readonly IDictionary<char, char> carsToPipes = new Dictionary<char, char>() {
            {'^', '|' },
            {'v', '|'},
            {'<', '-'},
            {'>', '-'}
        };
        public static readonly IDictionary<char, Direction> directions = new Dictionary<char, Direction>() {
            {'^', Direction.UP },
            {'v', Direction.DOWN},
            {'<', Direction.LEFT },
            {'>', Direction.RIGHT }
        };
        public static readonly IDictionary<Direction, char> directionsToCarChars = new Dictionary<Direction, char>() {
            {Direction.UP, '^' },
            {Direction.DOWN, 'v'},
            {Direction.LEFT , '<'},
            {Direction.RIGHT, '>' }
        };
        public static readonly IDictionary<Direction, (int, int)> directionsToPoints = new Dictionary<Direction, (int, int)>() {
            {Direction.UP, (-1, 0) },
            {Direction.DOWN, (1, 0)},
            {Direction.LEFT , (0, -1)},
            {Direction.RIGHT, (0, 1) }
        };
        public static readonly ISet<char> nonTurningPipes = new HashSet<char> { '|', '-' };
        public static readonly ISet<char> turningPipes = new HashSet<char> { '/', '\\', '+' };
        public static readonly ISet<char> pipes = new HashSet<char>(nonTurningPipes.Union(turningPipes));

    }
}
