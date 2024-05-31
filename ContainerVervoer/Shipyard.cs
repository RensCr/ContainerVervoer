using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer;
public class Shipyard
{
    public static (Row, Row) LoadShip(Ship ship, List<Container> containers)
    {
        Row row1 = new Row();
        Row row2 = new Row();

        // Sorteer de containers op type: valuableCooled, cooled, valuable, normal
        containers = containers.OrderByDescending(c => c.ContainerType).ToList();

        // Maak stacks aan voor rij 1 op het schip
        for (int i = 0; i < ship.Width; i++)
        {
            row1.Stacks.Add(new Stack());
        }

        // Maak stacks aan voor rij 2 op het schip
        for (int i = 0; i < ship.Width; i++)
        {
            row2.Stacks.Add(new Stack());
        }

        // Houd bij welke containers al zijn geplaatst
        HashSet<Container> placedContainers = new HashSet<Container>();

        // Plaats containers van het type "ValuableCooled" op rij 1
        foreach (var container in containers.Where(c => c.ContainerType == ContainerType.ValuableCooled))
        {
            bool placed = false;
            int valuedCooledCount = row1.Stacks.SelectMany(s => s.Containers).Count(c => c.ContainerType == ContainerType.ValuableCooled);
            if (valuedCooledCount < ship.Width)
            {
                // Plaats de container in rij 1, op de stack met het minste gewicht
                var minWeightStack = row1.Stacks.OrderBy(s => s.GetCurrentWeight()).First();
                if (minWeightStack.GetCurrentWeight() + container.Weight <= Stack.MaxWeightPerStack)
                {
                    minWeightStack.Containers.Add(container);
                    placedContainers.Add(container);
                    placed = true;
                }
            }
            else
            {
                Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1. Maximaal toegestane aantal containers van dit type bereikt.");
            }

            if (!placed)
            {
                // Als de container niet is geplaatst, probeer dan op rij 2
                foreach (var stack in row2.Stacks)
                {
                    if (stack.GetCurrentWeight() + container.Weight <= Stack.MaxWeightPerStack)
                    {
                        stack.Containers.Add(container);
                        placedContainers.Add(container);
                        placed = true;
                        break;
                    }
                }
            }

            if (!placed)
            {
                Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst.");
            }
        }

        // Plaats containers van het type "Cooled" op rij 1
        foreach (var container in containers.Where(c => c.ContainerType == ContainerType.Coolable))
        {
            bool placed = false;
            var minWeightStack = row1.Stacks.OrderBy(s => s.GetCurrentWeight()).First();
            if (minWeightStack.GetCurrentWeight() + container.Weight <= Stack.MaxWeightPerStack)
            {
                minWeightStack.Containers.Add(container);
                placedContainers.Add(container);
                placed = true;
            }

            if (!placed)
            {
                Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst.");
            }
        }

        // Verplaats de "Valuable" containers naar rij 2
        // Verplaats de "Valuable" containers naar rij 2
        // Verplaats de "Valuable" containers naar rij 2
        var valuableContainers = containers.Where(c => c.ContainerType == ContainerType.Valuable).OrderByDescending(c => c.Weight).ToList();
        foreach (var container in valuableContainers)
        {
            bool placed = false;
            foreach (var stack in row2.Stacks.OrderBy(s => s.GetCurrentWeight()))
            {
                // Controleer of er al een "Valuable" container op deze stack staat
                if (stack.Containers.Any(c => c.ContainerType == ContainerType.Valuable))
                {
                    
                    continue;
                }

                if (stack.GetCurrentWeight() + container.Weight <= Stack.MaxWeightPerStack)
                {
                    stack.Containers.Add(container);
                    placedContainers.Add(container);
                    placed = true;
                    break;
                }
            }

            if (!placed)
            {
                Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 2.");
            }
        }
        // Tel het aantal "Valuable" containers op rij 2
        int valuableCount = row2.Stacks.Sum(s => s.Containers.Count(c => c.ContainerType == ContainerType.Valuable));

        // Verplaats de "Normal" containers naar rij 2
        // Verplaats de "Normal" containers naar rij 2
        var normalContainers = containers.Where(c => c.ContainerType == ContainerType.Normal).OrderByDescending(c => c.Weight).ToList();
        foreach (var container in normalContainers)
        {
            bool placed = false;
            // Zoek de stack met het minste gewicht
            var minWeightStack = row2.Stacks.OrderBy(s => s.GetCurrentWeight()).First();
            // Controleer of het gewicht van de stack het maximale gewicht niet overschrijdt
            if (minWeightStack.GetCurrentWeight() + container.Weight <= Stack.MaxWeightPerStack)
            {
                // Voeg de container toe aan de stack
                minWeightStack.Containers.Add(container);
                placedContainers.Add(container);
                placed = true;
            }
            // Als de container niet is geplaatst, meld een waarschuwing
            if (!placed)
            {
                Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 2.");
            }
        }






        return (row1, row2);
    }





}





