
﻿// See https://aka.ms/new-console-template for more information
﻿using ContainerVervoer;
int lenght = 3;
int widht = 3;
Ship ship = new Ship(lenght, widht);

List<Container> containers = new List<Container>
        {
            new Container(26, ContainerType.Valuable),
            new Container(15, ContainerType.Normal),
            new Container(16, ContainerType.Normal),
            new Container(17, ContainerType.Normal),
            new Container(18, ContainerType.Normal),
            new Container(19, ContainerType.Normal),
            new Container(10, ContainerType.ValuableCooled), 
            new Container(11, ContainerType.ValuableCooled), 
            new Container(12, ContainerType.Coolable),       
            new Container(10, ContainerType.ValuableCooled), 
            new Container(11, ContainerType.ValuableCooled), 
            new Container(12, ContainerType.Coolable),       
            new Container(13, ContainerType.Valuable),       
            new Container(14, ContainerType.ValuableCooled), 
            new Container(15, ContainerType.ValuableCooled),
            new Container(16, ContainerType.Coolable),       
            new Container(17, ContainerType.Valuable),       
            new Container(18, ContainerType.ValuableCooled),
            new Container(19, ContainerType.ValuableCooled), 
            new Container(15, ContainerType.Normal),
            new Container(16, ContainerType.Normal),
            new Container(17, ContainerType.Normal),
            new Container(18, ContainerType.Normal),
            new Container(19, ContainerType.Normal),
            new Container(15, ContainerType.Normal),
            new Container(16, ContainerType.Normal),
            new Container(17, ContainerType.Normal),
            new Container(18, ContainerType.Normal),
            new Container(19, ContainerType.Normal),
            new Container(10, ContainerType.Coolable),
            new Container(10, ContainerType.Valuable),
            new Container(10, ContainerType.Normal)
        };




Console.WriteLine($"Informatie \n Je schip is {lenght}meter lang en {widht} meter breedt.");
Shipyard shipyard = new Shipyard();
List<Row> rows = shipyard.LoadShip(ship, containers);
string generatedURL = Containervisualiser.GenerateURL(ship,rows);
Console.WriteLine("Generated URL: " + generatedURL);

int x = 1;
foreach (var row in rows)
{
    Console.WriteLine($"\n Containers in rij {x}:");

    PrintRow(row);
    x += 1;
}

    

 void PrintRow(Row row)
{
    for (int i = 0; i < row.Stacks.Count; i++)
    {
        Console.WriteLine($"kolom {i + 1}:");
        foreach (var container in row.Stacks[i].Containers)
        {
            Console.WriteLine($"- Container Type: {container.ContainerType}, Gewicht: {container.Weight} kg");
        }
    }
}