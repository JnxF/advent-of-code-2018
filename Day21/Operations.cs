using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day21
{
    interface IOperation
    {
        void Execute(ulong[] p, ulong A, ulong B, ulong C);
    }

    class addr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] + p[B];
        }
    }

    class addi : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] + B;
        }
    }

    class mulr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] * p[B];
        }
    }

    class muli : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] * B;
        }
    }

    class banr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] & p[B];
        }
    }

    class bani : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] & B;
        }
    }

    class borr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] | p[B];
        }
    }

    class bori : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] | B;
        }
    }

    class setr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A];
        }
    }

    class seti : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = A;
        }
    }

    class gtir : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = A > p[B] ? 1UL : 0UL;
        }
    }

    class gtri : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] > B ? 1UL : 0UL;
        }
    }

    class gtrr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] > p[B] ? 1UL : 0UL;
        }
    }

    class eqir : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = A == p[B] ? 1UL : 0UL;
        }
    }

    class eqri : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] == B ? 1UL : 0UL;
        }
    }

    class eqrr : IOperation
    {
        public void Execute(ulong[] p, ulong A, ulong B, ulong C)
        {
            p[C] = p[A] == p[B] ? 1UL : 0UL;
        }
    }
}
