using SystemManager.Models;
using SystemManager.Models.Entities;

namespace SystemManager.Services;

internal class MenuCustomerService
{
    
    //----------------------------------------------------------------------------------------------------------------
    // Customer method
    public async Task CreateNewCaseAsync()
    {
        var customer = new Customers();
        var cases = new Cases();

        Console.WriteLine("Error Report: ");
        cases.Description = Console.ReadLine() ?? "";

        Console.WriteLine(DateTime.Now);
        cases.CreatedTime = DateTime.Now;

        Console.Write("Enter new status (0=NotStarted, 1=InProgress, 2=Completed):");
        var opt = Console.ReadLine();
        if (opt == "0")
            cases.Status = "NotStarted";
        else if (opt == "1")
            cases.Status = "InProgress";
        else if (opt == "2")
            cases.Status = "Completed";

        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        customer.Email = Console.ReadLine() ?? "";

        Console.Write("phone number: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";

        await CustomerService.SaveAsync(cases, customer);
    }

    public async Task ListSpecificCustomerCaseAsync()
    {
        Console.Write("Enter your Email: ");
        var email = Console.ReadLine();
        if (email != null)
        {
            var cases = await CustomerService.GetCustomerAsync(email);
            if (cases != null)
            {
                Console.WriteLine($"Case number: {cases.Id}");
                Console.WriteLine($"Description: {cases.Description}");
                Console.WriteLine($"CreatedTime: {cases.CreatedTime}");
                Console.WriteLine($"Status: {cases.Status}");
                Console.WriteLine($"Name: {cases.FirstName} {cases.LastName}");
                Console.WriteLine($"Email: {cases.Email}");
                Console.WriteLine($"Phone number: {cases.PhoneNumber}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No case with the given email address {email} was found.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($"No Id provided.");
            Console.WriteLine("");
        }

    }
    public async Task UpdateMyInfoCaseAsync()
    {
        Console.Write("Enter your Email: ");
        var email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email))
        {
            var customer = new Customers();
            var cases = await CustomerService.GetCustomerAsync(email);
            if (cases != null)
            {
                Console.WriteLine("Error Report: ");
                cases.Description = Console.ReadLine() ?? "";

                Console.WriteLine(DateTime.Now);
                cases.CreatedTime = DateTime.Now;

                Console.Write("Enter new status (0=NotStarted, 1=InProgress, 2=Completed):");
                var opt = Console.ReadLine();
                if (opt == "0")
                    cases.Status = "NotStarted";
                else if (opt == "1")
                    cases.Status = "InProgress";
                else if (opt == "2")
                    cases.Status = "Completed";

                Console.Write("First Name: ");
                customer.FirstName = Console.ReadLine() ?? "";

                Console.Write("Last Name: ");
                customer.LastName = Console.ReadLine() ?? "";

                Console.Write("Email: ");
                customer.Email = Console.ReadLine()?? "";

                Console.Write("phone number: ");
                customer.PhoneNumber = Console.ReadLine() ?? "";

                await CustomerService.UpdateCustomerAsync(cases, customer);

                Console.WriteLine("Your case updated");
            }
            else
            {
                Console.WriteLine($"Could not find any case with the given Id address.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine($"No Id provided.");
            Console.WriteLine("");
        }
    }
    public async Task DeleteMyCaseAsync()
    {
        Console.Write("Enter your Email: ");
        var email = Console.ReadLine();
        if (!String.IsNullOrEmpty(email))
        {
            bool deleted = await CustomerService.DeleteCustomerAsync(email);

            if(deleted) 
            {
                
                Console.WriteLine("Case deleted");
            }
            else
            {
                Console.WriteLine("Could not find any case with the given Email address.");
            }
        }
        else
        {
            Console.WriteLine($"No Id provided.");
            Console.WriteLine("");
        }
    }

    public async Task ShowCommentOnMyCaseAsync()
    {
        Console.Write("Enter case Email: ");
        string email = Console.ReadLine() ?? "";
        var existingCase = await CustomerService.GetCustomerAsync(email);

        if (existingCase != null)
        {
            Console.WriteLine($"Description: {existingCase.Description}");
            Console.WriteLine($"CreatedTime: {existingCase.CreatedTime}");
            Console.WriteLine($"Status: {existingCase.Status}");
            Console.WriteLine($"Customer name: {existingCase.FirstName} {existingCase.LastName}");
            Console.WriteLine($"Customer email: {existingCase.Email}");
            Console.WriteLine($"Customer phone number: {existingCase.PhoneNumber}");
            Console.WriteLine();
        }
        if (existingCase == null)
        {
            Console.WriteLine($"No case found with ID {email}");
            return;
        }
        var caseWithComments = await CustomerService.CheckUpMyCaseAsync(email);

        if (caseWithComments.Any())
        {
            foreach (var comment in caseWithComments)
            {
                Console.WriteLine($"Empolyee comments: {comment.Text} (posted on {comment.CreatedAt})");
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("No comment in this case");
        }
    }
}

