using SystemManager.Services;

var menu = new MenuService();

while (true)
{
    Console.WriteLine("Welcome to the Case Reports App!");

    Console.WriteLine("1. Are you an Employee");
    Console.WriteLine("2. Are you a Customer");

    int userType = int.Parse(Console.ReadLine()?? "");

    if (userType == 1)
    {
        Console.Clear();
        Console.WriteLine("1. View all cases");
        Console.WriteLine("2. View a specific case");
        Console.WriteLine("3. Update the status of a case");
        Console.WriteLine("4. Add comment to a case");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                await menu.ListAllCasesAsync();
                break;

            case "2":
                Console.Clear();
                await menu.ListSpecificCaseAsync();
                break;

            case "3":
                Console.Clear();
                await menu.UpdateSpecificCaseStatusAsync();
                break;

            case "4":
                Console.Clear();
                await menu.AddCommentToCaseAsync();
                break;
        }

    }
    else if (userType == 2)
    {
        Console.Clear();
        Console.WriteLine("1. Create a new case report");
        Console.WriteLine("2. Update the status of a case");
        Console.WriteLine("3. Delete a case");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                await menu.CreateNewCaseAsync();
                break;

            case "2":
                Console.Clear();
                await menu.UpdateSpecificCaseStatusAsync();
                break;

            case "3":
                Console.Clear();
                await menu.DeleteSpecificCaseAsync();
                break;

        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
    
    Console.WriteLine("Press any button to continue...!");
    Console.ReadLine();

}