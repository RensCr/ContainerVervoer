using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer
{
    public class Row
    {
        public List<Stack> Stacks { get; set; }

        public Row()
        {
            Stacks = new List<Stack>();
        }
        public int GetTotalWeight()
        {
            return Stacks.Sum(stack => stack.GetTotalWeight());
        }
    }
}
