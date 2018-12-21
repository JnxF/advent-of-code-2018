using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day19
{
    interface Operation
    {
        void Execute(int[] p, int A, int B, int C);
    }

    class addr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] + p[B];
        }
    }

    class addi : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] + B;
        }
    }

    class mulr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] * p[B];
        }
    }

    class muli : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] * B;
        }
    }

    class banr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] & p[B];
        }
    }

    class bani : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] & B;
        }
    }

    class borr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] | p[B];
        }
    }

    class bori : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] | B;
        }
    }

    class setr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A];
        }
    }

    class seti : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = A;
        }
    }

    class gtir : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = A > p[B] ? 1 : 0;
        }
    }

    class gtri : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] > B ? 1 : 0;
        }
    }

    class gtrr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] > p[B] ? 1 : 0;
        }
    }

    class eqir : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = A == p[B] ? 1 : 0;
        }
    }

    class eqri : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] == B ? 1 : 0;
        }
    }

    class eqrr : Operation
    {
        public void Execute(int[] p, int A, int B, int C)
        {
            p[C] = p[A] == p[B] ? 1 : 0;
        }
    }
}
