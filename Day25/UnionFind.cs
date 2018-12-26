using System;
using System.Collections.Generic;
using System.Text;

namespace Day25
{
    /// <summary>
    /// Adapted from https://algs4.cs.princeton.edu/15uf/UF.java.html
    /// </summary>
    class UnionFind
    {
        private readonly int[] _parent;  // parent[i] = parent of i
        private readonly byte[] _rank;   // rank[i] = rank of subtree rooted at i (never more than 31)
        public int Count { get; private set; }

        /**
         * Initializes an empty union–find data structure with {@code n} sites
         * {@code 0} through {@code n-1}. Each site is initially in its own 
         * component.
         *
         * @param  n the number of sites
         * @throws IllegalArgumentException if {@code n < 0}
         */
        public UnionFind(int n)
        {
            if (n < 0) throw new ArgumentException();
            Count = n;
            _parent = new int[n];
            _rank = new byte[n];
            for (int i = 0; i < n; i++)
            {
                _parent[i] = i;
                _rank[i] = 0;
            }
        }

        /**
         * Returns the component identifier for the component containing site {@code p}.
         *
         * @param  p the integer representing one site
         * @return the component identifier for the component containing site {@code p}
         * @throws IllegalArgumentException unless {@code 0 <= p < n}
         */
        public int Find(int p)
        {
            Validate(p);
            while (p != _parent[p])
            {
                _parent[p] = _parent[_parent[p]];    // path compression by halving
                p = _parent[p];
            }
            return p;
        }

        /**
         * Returns true if the the two sites are in the same component.
         *
         * @param  p the integer representing one site
         * @param  q the integer representing the other site
         * @return {@code true} if the two sites {@code p} and {@code q} are in the same component;
         *         {@code false} otherwise
         * @throws IllegalArgumentException unless
         *         both {@code 0 <= p < n} and {@code 0 <= q < n}
         */
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /**
         * Merges the component containing site {@code p} with the 
         * the component containing site {@code q}.
         *
         * @param  p the integer representing one site
         * @param  q the integer representing the other site
         * @throws IllegalArgumentException unless
         *         both {@code 0 <= p < n} and {@code 0 <= q < n}
         */
        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            // make root of smaller rank point to root of larger rank
            if (_rank[rootP] < _rank[rootQ]) _parent[rootP] = rootQ;
            else if (_rank[rootP] > _rank[rootQ]) _parent[rootQ] = rootP;
            else
            {
                _parent[rootQ] = rootP;
                _rank[rootP]++;
            }
            Count--;
        }

        // validate that p is a valid index
        private void Validate(int p)
        {
            int n = _parent.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentException("index " + p + " is not between 0 and " + (n - 1));
            }
        }
    }
}
