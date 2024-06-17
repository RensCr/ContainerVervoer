using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Container
    {
        public int Weight { get; set; } // Gewicht in kg
        public ContainerType ContainerType { get; set; }

        private const int WeightEmptyContainer = 4000;
        private const int MaxWeightContainer = 150000;

        public Container(int weight, ContainerType containerType)
        {
            if (weight + WeightEmptyContainer <= MaxWeightContainer)
            {
                this.Weight = weight + WeightEmptyContainer;
            }
            else
            {
                throw new Exception($"De container weegt teveel. De container weegt {weight + WeightEmptyContainer} en maximaal {MaxWeightContainer} toegelaten");
            }

            this.ContainerType = containerType;
        }

        public bool CanPlaceInStack(Stack stack, List<Container> placedContainers)
        {
            if (stack.CanAddContainer(this))
            {
                stack.AddContainer(this);
                placedContainers.Add(this);
                
                return true;
            }
            return false;
        }


        public void PlaceValuableInStack(Stack stack, List<Container> placedContainers)
        {
            if (stack.CanAddContainer(this))
            {
                stack.AddValueableContainer(this);
                placedContainers.Add(this);
            }
        }
    }
}
