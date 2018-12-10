using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    class Tree
    {
        public IList<Tree> Subtrees { get; set; }

        public IEnumerable<int> Metadata { get; set; }
           
        public Tree()
        {
            Subtrees = new List<Tree>();
            Metadata = new List<int>();
        }
    }
}
