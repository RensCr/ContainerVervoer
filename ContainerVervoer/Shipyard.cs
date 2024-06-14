using System.Reflection.Metadata.Ecma335;

namespace ContainerVervoer
{
    public class Shipyard
    {
        public List<Row> LoadShip(Ship ship, List<Container> containers)
        {
            List<Row> rows = CreateRowsAndStacks(ship);
            List<Container> placedContainers = new List<Container>();

            var valuableCooledContainers = containers.Where(c => c.ContainerType == ContainerType.ValuableCooled).OrderByDescending(c => c.Weight).ToList();
            var coolableContainers = containers.Where(c => c.ContainerType == ContainerType.Coolable).ToList();
            var valuableContainers = containers.Where(c => c.ContainerType == ContainerType.Valuable).OrderByDescending(c => c.Weight).ToList();
            var normalContainers = containers.Where(c => c.ContainerType == ContainerType.Normal).OrderByDescending(c => c.Weight).ToList();

            int[] order = CalculateBestOrder(ship);
            PlaceValuableCooledContainersOnRow1(rows, valuableCooledContainers, order, ship, placedContainers);
            PlaceCoolableContainersOnRow1(rows, coolableContainers, placedContainers);
            PlaceValuableContainersOnRows(rows, valuableContainers, placedContainers);
            PlaceNormalContainersOnRows(rows, normalContainers, placedContainers);

            return rows;
        }

        public int[] CalculateBestOrder(Ship ship)
        {
            int[] order = new int[ship.Width];
            for (int i = 0; i < ship.Width; i++)
            {
                order[i] = (i % 2 == 0) ? i / 2 : ship.Width - (i / 2) - 1;
            }
            return order;
        }

        public List<Row> CreateRowsAndStacks(Ship ship)
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < ship.Length; i++)
            {
                rows.Add(new Row());
            }

            foreach (var row in rows)
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    row.Stacks.Add(new Stack());
                }
            }
            return rows;
        }

        private void PlaceValuableCooledContainersOnRow1(List<Row> rows, List<Container> valuableCooledContainers, int[] order, Ship ship, List<Container> placedContainers)
        {
            foreach (var container in valuableCooledContainers)
            {
                bool placed = false;
                int valuableCooledCount = rows[0].Stacks.SelectMany(s => s.Containers).Count(c => c.ContainerType == ContainerType.ValuableCooled);
                if (valuableCooledCount < ship.Width)
                {
                    var stack = rows[0].Stacks[order[valuableCooledCount]];
                    if (stack.CanAddContainer(container))
                    {
                        stack.AddContainer(container);
                        placedContainers.Add(container);
                        placed = true;
                    }
                }
                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1. Maximaal toegestane aantal containers van dit type bereikt.");
                }
            }
        }


        private void PlaceCoolableContainersOnRow1(List<Row> rows, List<Container> coolableContainers, List<Container> placedContainers)

        {
            foreach (var container in coolableContainers)
            {
                bool placed = false;
                var minWeightStack = rows[0].Stacks.OrderBy(s => s.GetCurrentWeight()).First();
                if (minWeightStack.CanAddContainer(container))
                {
                    minWeightStack.AddContainer(container);
                    placedContainers.Add(container);
                    placed = true;
                }

                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1. Maximaal toegestane aantal containers van dit type bereikt.");
                }
            }
        }

        private void PlaceValuableContainersOnRows(List<Row> rows, List<Container> valuableContainers, List<Container> placedContainers)
        {
            foreach (var container in valuableContainers)
            {
                bool placed = false;
                for (int i = 0; i < rows.Count; i++)
                {
                    var stack = rows[i].Stacks.FirstOrDefault(s => !s.ContainsValuableCooledContainer());
                    if (stack != null && stack.CanAddContainer(container))
                    {
                        stack.AddContainer(container);
                        placedContainers.Add(container);
                        placed = true;
                        break;
                    }
                }
                if (placed) break;
            }
        }


        private void PlaceNormalContainersOnRows(List<Row> rows, List<Container> normalContainers, List<Container> placedContainers)
        {
            foreach (var container in normalContainers)
            {
                bool placed = false;
                foreach (var row in rows)
                {
                    var minWeightStack = row.Stacks.OrderBy(s => s.GetCurrentWeight()).First();
                    if (minWeightStack.CanAddContainer(container))
                    {
                        minWeightStack.AddContainer(container);
                        placedContainers.Add(container);
                        placed = true;
                        break;
                    }
                }
                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst.");
                }
            }
        }
        
    }
}
