using SystemManager.Models;
using SystemManager.Models.Entities;

namespace SystemManager.Services;

internal class MenuService
{
    public async Task CreateNewCaseAsync()
    {
        var customer = new Customers();
        var cases = new Cases();

        Console.WriteLine("Error Report: ");
        cases.Description = Console.ReadLine()?? "";

        Console.WriteLine(DateTime.Now);
        cases.CreatedTime = DateTime.Now;

        Console.Write("Enter new status (0=NotStarted, 1=InProgress, 2=Completed):");
        CaseStatus Status = (CaseStatus)int.Parse(Console.ReadLine() ?? "");
        //CaseStatus Status = (CaseStatus)Enum.Parse(typeof(CaseStatus), Console.ReadLine() ?? "");

        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        customer.Email = Console.ReadLine() ?? "";

        Console.Write("phone number: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";

        await CaseService.SaveAsync(cases, customer);
    }

    public async Task ListAllCasesAsync()
    {
        var (casess, customers) = await CaseService.GetAllAsync();

        if (casess.Any())
        {
            foreach (Cases cases in casess)
            {
                Console.WriteLine($"Case number: {cases.Id}");
                Console.WriteLine($"Description: {cases.Description}");
                Console.WriteLine($"CreatedTime: {cases.CreatedTime}");
                Console.WriteLine($"Status: {cases.Status}");

                if (cases.Customer != null)
                {
                    Console.WriteLine($"Customer : {cases.Customer.Id}");
                    Console.WriteLine($"Namn: {cases.Customer.FirstName} {cases.Customer.LastName}");
                    Console.WriteLine($"E-postadress: {cases.Customer.Email}");
                    Console.WriteLine($"Telefonnummer: {cases.Customer.PhoneNumber}");
                }

                Console.WriteLine("");
            }
        }
    }

    public async Task ListSpecificCaseAsync()
    {

    }

    public async Task UpdateSpecificCaseStatusAsync()
    {

    }

    public async Task DeleteSpecificCaseAsync()
    {

    }

    public async Task AddCommentToCaseAsync()
    {

    }
}
