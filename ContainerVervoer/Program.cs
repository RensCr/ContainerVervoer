
﻿// See https://aka.ms/new-console-template for more information
﻿using ContainerVervoer;
int lenght = 2;
int widht = 2;
Ship ship = new Ship(lenght, widht); 

List<Container> containers = new List<Container>
        {
    //new Container(40000, ContainerType.Valuable),
    //    new Container(40001, ContainerType.Valuable),
    //        new Container(40002, ContainerType.Valuable),
    //                    new Container(40003, ContainerType.Valuable),
    //                                new Container(40004, ContainerType.Valuable),
    //                                new Container(40000,ContainerType.Normal),
    //                                new Container(40001,ContainerType.Normal),
    //                                                                    new Container(40002,ContainerType.Normal),
                                    new Container(40003,ContainerType.Normal),
        new Container(40000, ContainerType.Valuable),
                new Container(40000, ContainerType.Valuable),
                        new Container(40000, ContainerType.Valuable),
                                                            new Container(40003,ContainerType.Normal),
                                                                                                new Container(40003,ContainerType.Normal),
                                                                                                new Container(40003,ContainerType.Normal),
//            new Container(10000, ContainerType.ValuableCooled),
//            new Container(10001, ContainerType.ValuableCooled),


//            new Container(10002, ContainerType.Coolable),
//            new Container(10003, ContainerType.Valuable),
//            new Container(10004, ContainerType.Normal),

//            new Container(10005, ContainerType.Normal),
//            new Container(10006, ContainerType.Normal),
//                        new Container(10000, ContainerType.ValuableCooled),
//            new Container(10001, ContainerType.ValuableCooled),


//            new Container(10002, ContainerType.Coolable),
//            new Container(10003, ContainerType.Valuable),
//            new Container(10004, ContainerType.Normal),

//            new Container(10005, ContainerType.Normal),
//            new Container(10006, ContainerType.Normal),
//            new Container(10007, ContainerType.ValuableCooled),
//new Container(10008, ContainerType.ValuableCooled),
//new Container(10009, ContainerType.Coolable),
//new Container(10010, ContainerType.Valuable),
//new Container(10011, ContainerType.Normal),

//new Container(10012, ContainerType.Normal),
//new Container(10013, ContainerType.Normal),
//new Container(10014, ContainerType.ValuableCooled),
//new Container(10015, ContainerType.ValuableCooled),
//new Container(10016, ContainerType.Coolable),

//new Container(10017, ContainerType.Valuable),
//new Container(10018, ContainerType.Normal),
//new Container(10019, ContainerType.Normal),
//new Container(10020, ContainerType.Normal),
//new Container(10021, ContainerType.ValuableCooled),

//new Container(10022, ContainerType.ValuableCooled),
//new Container(10023, ContainerType.Coolable),
//new Container(10024, ContainerType.Valuable),
//new Container(10025, ContainerType.Normal),
//new Container(10026, ContainerType.Normal),




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