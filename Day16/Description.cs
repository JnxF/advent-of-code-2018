using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    class Description
    {
        public int[] Before { get; set; }
        public int[] Instruction { get; set; }
        public int[] After { get; set; }
        public int Opcode => Instruction[0];
        public int Input1 => Instruction[1];
        public int Input2 => Instruction[2];
        public int Output => Instruction[3];
    }
}
