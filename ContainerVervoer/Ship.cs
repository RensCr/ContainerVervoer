using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer
{
    public class Ship
    {
        public int Length;
        public int Width;
        public int MaxWeight;
        public int MaxStacks; // Maximaal aantal stapels op het schip

        public Ship(int length, int width)
        {
            this.Length = length;
            this.Width = width;
            this.MaxWeight = CalculateMaxWeight();
            this.MaxStacks = width;
        }

        public int CalculateMaxWeight()
        {
            int maxWeight = ((Length * Width) * 150000);
            return maxWeight;
        }
    }
}
