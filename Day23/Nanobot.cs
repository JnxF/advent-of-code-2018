using System;
using System.Collections.Generic;
using System.Text;

namespace Day23
{
    class Nanobot
    {
        public long Radius { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z} @ {Radius}";
        }
    }
}
