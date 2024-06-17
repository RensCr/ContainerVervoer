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
        public int MaxStacks; // Maximaal aantal stapels op het schip
        public List<Row> Rows { get; private set; }

        public Ship(int length, int width)
        {
            this.Length = length;
            this.Width = width;
            this.MaxWeight = CalculateMaxWeight();
            this.MaxStacks = width;
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
    }
}
