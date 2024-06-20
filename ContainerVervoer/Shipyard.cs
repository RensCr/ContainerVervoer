using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Shipyard
    {
        public List<Row> LoadShip(Ship ship, List<Container> containers)
        {
            List<Row> rows = ship.Rows;
            List<Container> placedContainers = new List<Container>();

            var valuableCooledContainers = containers.Where(c => c.ContainerType == ContainerType.ValuableCooled).OrderByDescending(c => c.Weight).ToList();
            var coolableContainers = containers.Where(c => c.ContainerType == ContainerType.Coolable).ToList();
            var valuableContainers = containers.Where(c => c.ContainerType == ContainerType.Valuable).OrderByDescending(c => c.Weight).ToList();
            var normalContainers = containers.Where(c => c.ContainerType == ContainerType.Normal).OrderBy(c => c.Weight).ToList();

            int[] order = CalculateBestOrder(ship.Width);
            PlaceValuableCooledContainersOnRow1(valuableCooledContainers, order, ship, placedContainers);
            PlaceCoolableContainersOnRow1(rows, coolableContainers, placedContainers);
            PlaceValuableContainers(rows, valuableContainers, placedContainers);
            PlaceNormalContainers(rows, normalContainers, placedContainers, ship);
            int totalShipWeight = ship.CalulateTotalWeight(rows);
            if (totalShipWeight > ship.MaxWeight / 2)
            {
                return rows;
            }
            else
            {
                Console.WriteLine($"Containers voldoen niet aan de gewichteisen. Totaalgewicht van de geplaatste containers: {totalShipWeight}. \nMaximaal schipgewicht {ship.MaxWeight}kg, minimaal schipgewicht {ship.MaxWeight / 2}kg");
                return rows;
            }
        }

        private int[] CalculateBestOrder(int width)
        {
            int[] order = new int[width];

            for (int i = 0; i < width; i++)
            {
                order[i] = (i % 2 == 0) ? i / 2 : width - (i / 2) - 1;
            }

            return order;
        }

        private void PlaceValuableCooledContainersOnRow1(List<Container> valuableCooledContainers, int[] order, Ship ship, List<Container> placedContainers)
        {
            List<Row> rows = ship.Rows;

            foreach (var container in valuableCooledContainers)
            {
                bool placed = false;
                int valuableCooledCount = rows[0].Stacks.SelectMany(s => s.Containers).Count(c => c.ContainerType == ContainerType.ValuableCooled);
                if (valuableCooledCount < ship.Width)
                {
                    var stack = rows[0].Stacks[order[valuableCooledCount]];
                    if (stack.CanAddContainer(container) && !stack.ContainsValuableCooledContainer())
                    {
                        container.CanPlaceInStack(stack, placedContainers);
                        placed = true;
                    }
                }
                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1. Maximaal toegestane aantal containers van dit type bereikt of al een waardevolle gekoelde container in de stapel.");
                }
            }
        }

        private void PlaceCoolableContainersOnRow1(List<Row> rows, List<Container> coolableContainers, List<Container> placedContainers)
        {
            foreach (var container in coolableContainers)
            {
                bool placed = false;
                var minWeightStack = rows[0].Stacks.OrderBy(s => s.GetCurrentStackWeight()).First();
                if (minWeightStack.CanAddContainer(container))
                {
                    container.CanPlaceInStack(minWeightStack, placedContainers);
                    placed = true;
                }

                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1.");
                }
            }
        }

        private void PlaceValuableContainers(List<Row> rows, List<Container> valuableContainers, List<Container> placedContainers)
        {
            foreach (var container in valuableContainers)
            {
                bool placed = false;

                var filteredRows = rows.Where((row, index) => (index + 1) % 3 != 0);

                foreach (var stack in filteredRows.SelectMany(r => r.Stacks).OrderBy(s => s.GetCurrentStackWeight()))
                {
                    if (!stack.ContainsValuableCooledContainer() && !stack.ContainsValuableContainer() && stack.CanAddContainer(container))
                    {
                        container.PlaceValuableInStack(stack, placedContainers);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Console.WriteLine($"Container {container.ContainerType} met gewicht {container.Weight} kon niet geplaatst worden.");
                }
            }
        }


        private void PlaceNormalContainers(List<Row> rows, List<Container> normalContainers, List<Container> placedContainers,Ship ship)
        {
            foreach (var container in normalContainers)
            {
                if (!TryPlaceContainer(container, rows, placedContainers,ship))
                {
                    Console.WriteLine($"Container : {container.ContainerType} met gewicht {container.Weight}. Kon niet geplaatst worden");
                }
            }
        }

        private bool TryPlaceContainer(Container container, List<Row> rows, List<Container> placedContainers,Ship ship)
        {
            var orderedRows = rows.OrderBy(row => row.GetCurrentTotalWeight()).ToList();

            foreach (var row in orderedRows)
            {
                foreach (var stack in row.Stacks.OrderBy(s => s.GetCurrentStackWeight()))
                {
                    int currentRowIndex = rows.IndexOf(row);
                    int stackIndex = row.Stacks.IndexOf(stack);

                    if (stack.CanAddContainer(container) && ship.CanPlaceContainerInStack(currentRowIndex, stackIndex))
                    {
                        container.CanPlaceInStack(stack, placedContainers);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

