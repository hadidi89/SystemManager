using SystemManager.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
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
        Console.WriteLine("5. View the case with its comment");
        Console.WriteLine("6. Delete case");

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

            case "5":
                Console.Clear();
                await menu.ShowCommentToCaseAsync();
                break;
            case "6":
                Console.Clear();
                await menu.DeleteSpecificCaseAsync();
                break;
        }

    }
    else if (userType == 2)
    {
        Console.Clear();
        Console.WriteLine("1. Create a new case report");
        Console.WriteLine("2. View your case");
        Console.WriteLine("3. Update your info case");
        Console.WriteLine("4. Check comments on your case");
        Console.WriteLine("5. Delete your case");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                await menu.CreateNewCaseAsync();
                break;

            case "2":
                Console.Clear();
                await menu.ListSpecificCustomerCaseAsync();
                break;
            case "3":
                Console.Clear();
                await menu.UpdateMyInfoCaseAsync();
                break;

            case "4":
                Console.Clear();
                await menu.DeleteMyCaseAsync();
                break;

            case "5":
                Console.Clear();
                await menu.ShowCommentOnMyCaseAsync();
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