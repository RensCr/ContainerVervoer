using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Ship
    {
        public int Length;
        public int Width;
        public int MaxWeight;
        public List<Row> Rows { get; private set; }

        public Ship(int length, int width)
        {
            this.Length = length;
            this.Width = width;
            this.MaxWeight = CalculateMaxWeight();
            this.Rows = CreateRowsAndStacks();
        }

        private List<Row> CreateRowsAndStacks()
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < Length; i++)
            {
                rows.Add(new Row());
            }

            foreach (var row in rows)
            {
                for (int i = 0; i < Width; i++)
                {
                    row.Stacks.Add(new Stack());
                }
            }
            return rows;
        }

        public int CalculateMaxWeight()
        {
            int maxWeight = ((Length * Width) * 150000);
            return maxWeight;
        }
        public int CalulateTotalWeight(List<Row> rows)
        {
            int totalWeight = 0;
            foreach (var row in rows)
            {
                foreach (var stack in row.Stacks)
                {
                    foreach (var container in stack.Containers)
                    {
                        totalWeight += container.Weight;
                    }
                }
            }
            return totalWeight;
        }

        public bool CanPlaceContainerInStack(int currentRowIndex, int stackIndex)
        {
            Row row = Rows[currentRowIndex];
            Stack stack = row.Stacks[stackIndex];
            bool nextStackHasValuable = false;
            bool previousStackHasValuable = false;
            int previousRowStackHeight = -1;
            int nextRowStackHeight = -1;
            if (currentRowIndex > 0)
            {
                Row previousRow = Rows[currentRowIndex - 1];
                Stack previousStack = previousRow.Stacks[stackIndex];
                previousStackHasValuable = previousStack.HasValuableContainer();
                previousRowStackHeight = previousRow.Stacks[stackIndex].Height;
            }

            if(currentRowIndex < Rows.Count-1)
            {
                Row nextRow = Rows[currentRowIndex + 1];
                Stack nextStack = nextRow.Stacks[stackIndex];
                nextStackHasValuable = nextStack.HasValuableContainer();
                nextRowStackHeight = nextRow.Stacks[stackIndex].Height;
            }

            if (previousStackHasValuable || nextStackHasValuable)
            {
                if (previousStackHasValuable && (previousRowStackHeight < stack.Containers.Count + 2))
                {
                    return false;
                }

                if (nextStackHasValuable && (nextRowStackHeight < stack.Containers.Count + 2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
