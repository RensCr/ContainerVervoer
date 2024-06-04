// See https://aka.ms/new-console-template for more information
using ContainerVervoer;

Ship ship = new Ship(3, 3); // lengte 2 meter,  breedte 2 meter
List<Container> containers = new List<Container>
        {
            new Container(30000, ContainerType.Normal),
            new Container(20000, ContainerType.Valuable),
            new Container(20000, ContainerType.Valuable),
            new Container(20000, ContainerType.Valuable),
            new Container(1, ContainerType.Coolable),
            new Container(1, ContainerType.Coolable),
            new Container(50, ContainerType.Normal),
            new Container(60000, ContainerType.Valuable),
            new Container(241, ContainerType.ValuableCooled),
            new Container(240, ContainerType.ValuableCooled),
            new Container(25000, ContainerType.ValuableCooled),
        };

 List<Row> rows = Shipyard.LoadShip(ship, containers);
int x = 1;
foreach (var row in rows)
{
    Console.WriteLine($"Containers in deel {x}:");

    PrintRow(row);
    x += 1;
}

    

 void PrintRow(Row row)
{
    for (int i = 0; i < row.Stacks.Count; i++)
    {
        Console.WriteLine($"Stack {i + 1}:");
        foreach (var container in row.Stacks[i].Containers)
        {
            Console.WriteLine($"- Container Type: {container.ContainerType}, Gewicht: {container.Weight} kg");
        }
    }
}