using SystemManager.Models;

namespace SystemManager.Services
{
    internal class MenuEmployeeService
    {
        // Empolyee method
        public async Task ListAllCasesAsync()
        {
            var (casess, customers) = await EmployeeService.GetAllAsync();

            if (casess.Any())
            {
                foreach (Cases cases in casess)
                {
                    Console.WriteLine($"Case number: {cases.Id}");
                    Console.WriteLine($"Description: {cases.Description}");
                    Console.WriteLine($"CreatedTime: {cases.CreatedTime}");
                    Console.WriteLine($"Status: {cases.Status}");
                    Console.WriteLine($"Name: {cases.FirstName} {cases.LastName}");
                    Console.WriteLine($"Email: {cases.Email}");
                    Console.WriteLine($"Phone number: {cases.PhoneNumber}");
                    Console.WriteLine("");
                }
            }
        }

        public async Task ListSpecificCaseAsync()
        {
            Console.Write("Enter the Id case: ");
            var id = Console.ReadLine();
            if (!String.IsNullOrEmpty(id))
            {
                var cases = await EmployeeService.GetAsync(int.Parse(id));
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
                    Console.WriteLine($"No case with the given email address {id} was found.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"No Id provided.");
                Console.WriteLine("");
            }

        }

        public async Task UpdateSpecificCaseStatusAsync()
        {
            Console.WriteLine("Enter the Id case you want to update ");
            var id = Console.ReadLine();
            if (!String.IsNullOrEmpty(id))
            {

                var cases = await EmployeeService.GetAsync(int.Parse(id));
                if (cases != null)
                {
                    Console.Write("Enter new status (0=NotStarted, 1=InProgress, 2=Completed):");
                    var opt = Console.ReadLine();
                    if (opt == "0")
                        cases.Status = "NotStarted";
                    else if (opt == "1")
                        cases.Status = "InProgress";
                    else if (opt == "2")
                        cases.Status = "Completed";

                    await EmployeeService.UpdateAsync(cases);

                    Console.WriteLine("Status updated");
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

        public async Task DeleteSpecificCaseAsync()
        {
            Console.Write("Enter the Id case you want to delete: ");
            var id = Console.ReadLine();
            if (!String.IsNullOrEmpty(id))
            {
                bool deleted = await EmployeeService.DeleteAsync(int.Parse(id));

                if (deleted)
                {
                    Console.WriteLine("Case deleted");
                }
                else
                {
                    Console.WriteLine("Could not find any case with the given Id address.");
                }
            }
            else
            {
                Console.WriteLine($"No Id provided.");
                Console.WriteLine("");
            }
        }

        public async Task AddCommentToCaseAsync()
        {
            Console.Write("Enter the Id case you want to add comment on: ");
            int caseId = int.Parse(Console.ReadLine() ?? "");

            Console.WriteLine("We need your information to know who is following this case");
            Console.WriteLine("employee information:");
            Console.Write("First name: ");
            string firstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            string lastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine() ?? "";

            Employees employee = new Employees
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            Console.Write("Enter the comment text: ");
            string commentText = Console.ReadLine() ?? "";

            Comments comment = new Comments
            {
                Text = commentText,
                CreatedAt = DateTime.Now
            };

            await EmployeeService.AddCommentAsync(caseId, comment, employee);

            Console.WriteLine("Comment added successfully.");
        }

        public async Task ShowCommentToCaseAsync()
        {
            Console.Write("Enter case ID: ");
            int caseId = int.Parse(Console.ReadLine() ?? "");
            var existingCase = await EmployeeService.GetAsync(caseId);

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
                Console.WriteLine($"No case found with ID {caseId}");
                return;
            }
            var caseWithComments = await EmployeeService.GetCommentsAsync(caseId);

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
                Console.WriteLine("Are you want to add new comment, YES or NO");
                var option = Console.ReadLine() ?? "";
                if (option == "yes".ToLower())
                {
                    Console.WriteLine("We need your information to know who is following this case");
                    Console.WriteLine("employee information:");
                    Console.Write("First name: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Last name: ");
                    string lastName = Console.ReadLine() ?? "";

                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";

                    Console.Write("Phone number: ");
                    string phoneNumber = Console.ReadLine() ?? "";
                    Employees employee = new Employees
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PhoneNumber = phoneNumber
                    };

                    Console.Write("Enter the comment text: ");
                    string commentText = Console.ReadLine() ?? "";

                    Comments _comment = new()
                    {
                        Text = commentText,
                        CreatedAt = DateTime.Now
                    };

                    await EmployeeService.AddCommentAsync(caseId, _comment, employee);

                    Console.WriteLine("Comment added successfully.");
                }
                else
                {
                    Console.WriteLine("Welcome when yow want to add any comment");
                }
            }
        }
    }
}
