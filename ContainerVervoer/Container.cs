namespace ContainerVervoer
{
    public class Container
    {
        public int Weight { get; set; } // Gewicht in kg
        public ContainerType ContainerType { get; set; }

        public Container(int weight, ContainerType containerType)
        {
            this.Weight = weight;
            this.ContainerType = containerType;
        }
    }
}
