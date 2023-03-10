using SystemManager.Services;

var menuCustomer = new MenuCustomerService();
var menuEmployee = new MenuEmployeeService();
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
                await menuEmployee.ListAllCasesAsync();
                break;

            case "2":
                Console.Clear();
                await menuEmployee.ListSpecificCaseAsync();
                break;

            case "3":
                Console.Clear();
                await menuEmployee.UpdateSpecificCaseStatusAsync();
                break;

            case "4":
                Console.Clear();
                await menuEmployee.AddCommentToCaseAsync();
                break;

            case "5":
                Console.Clear();
                await menuEmployee.ShowCommentToCaseAsync();
                break;
            case "6":
                Console.Clear();
                await menuEmployee.DeleteSpecificCaseAsync();
                break;
        }

    }
    else if (userType == 2)
    {
        Console.Clear();
        Console.WriteLine("1. Create a new case report");
        Console.WriteLine("2. View your case");
        Console.WriteLine("3. Update your case info");
        Console.WriteLine("4. Check comments on your case");
        Console.WriteLine("5. Delete your case");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                await menuCustomer.CreateNewCaseAsync();
                break;

            case "2":
                Console.Clear();
                await menuCustomer.ListSpecificCustomerCaseAsync();
                break;
            case "3":
                Console.Clear();
                await menuCustomer.UpdateMyInfoCaseAsync();
                break;

            case "4":
                Console.Clear();
                await menuCustomer.DeleteMyCaseAsync();
                break;

            case "5":
                Console.Clear();
                await menuCustomer.ShowCommentOnMyCaseAsync();
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