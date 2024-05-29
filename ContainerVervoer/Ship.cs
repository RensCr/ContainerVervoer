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
        private int Length;
        private int width;
        private int maxWeigth;
        public Ship(int length,int width) 
        { 
            this.Length = length;
            this.width = width;
            this.maxWeigth = CalculateMaxWeight();
        }
        private int CalculateMaxWeight()
        {
            int maxWeigth = ((Length * width) * 150000);
            return maxWeigth;
        }
    }
}
