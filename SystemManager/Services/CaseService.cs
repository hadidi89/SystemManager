
using Microsoft.EntityFrameworkCore;
using SystemManager.Contexts;
using SystemManager.Models;
using SystemManager.Models.Entities;

namespace SystemManager.Services
{
    internal class CaseService
    {
        private readonly static DataContext _context = new();
        public static async Task SaveAsync(Cases cases, Customers customer)
        {
            var _caseEntity = new CaseEntity
            { 
                Description= cases.Description,
                CreatedTime= DateTime.Now,
                Status = cases.Status,
            };

            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == customer.FirstName && x.LastName == customer.LastName && x.Email == customer.Email && x.PhoneNumber == customer.PhoneNumber);
            if (_customerEntity != null)
                _caseEntity.CustomerId = _customerEntity.Id;
            else
                _caseEntity.Customer = new CustomerEntity
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                        
                };

            _context.Add(_caseEntity);
            await _context.SaveChangesAsync();
        }

        public static async Task<(IEnumerable<Cases>, IEnumerable<Customers>)> GetAllAsync()
        {
            var _casess = new List<Cases>();
            var _customers = new List<Customers>();

            foreach (var _cases in await _context.Cases.Include(x => x.Customer).ToListAsync())
            {
                var newCase = new Cases
                {
                    Id = _cases.Id,
                    Description = _cases.Description,
                    CreatedTime = DateTime.Now,
                    Status = _cases.Status,
                    FirstName = _cases.Customer.FirstName,
                    LastName = _cases.Customer.LastName,
                    Email = _cases.Customer.Email,
                    PhoneNumber = _cases.Customer.PhoneNumber
                };

                _casess.Add(newCase);

                //if (_cases.Customer != null)
                //{
                //    var newCustomer = new Customers
                //    {
                //        Id = _cases.Customer.Id,
                //        FirstName = _cases.Customer.FirstName,
                //        LastName = _cases.Customer.LastName,
                //        Email = _cases.Customer.Email,
                //        PhoneNumber = _cases.Customer.PhoneNumber

                //    };

                //    _customers.Add(newCustomer);
                //}
            }

            return (_casess, _customers);
        }

        public static async Task<Cases> GetAsync(int id)
        {
            var _cases = await _context.Cases.Include(x=>x.Customer).FirstOrDefaultAsync(x =>x.Id == id);
            if (_cases != null)
                return new Cases
                {
                    Id = _cases.Id,
                    Description = _cases.Description,
                    CreatedTime = DateTime.Now,
                    Status = _cases.Status,
                    FirstName= _cases.Customer.FirstName,
                    LastName= _cases.Customer.LastName,
                    Email = _cases.Customer.Email,
                    PhoneNumber = _cases.Customer.PhoneNumber
                };
            else
                return null!;
        }
        public static async Task UpdateAsync(Cases cases)
        {
            var _caseEntity = await _context.Cases.FirstOrDefaultAsync(x => x.Id == cases.Id);
            if (_caseEntity != null)
            {
                if (!string.IsNullOrEmpty(cases.Status)) 
                    _caseEntity.Status = cases.Status;
                _context.Update(_caseEntity);
                await _context.SaveChangesAsync();
            }
            
        }

        public static async Task DeleteAsync(int id)
        {
            var _cases = await _context.Cases.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (_cases != null)
            {
                _context.Remove(_cases);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task AddCommentAsync(int caseId, Comments comment, Employees employee)
        {
            var caseEntity = await _context.Cases.FindAsync(caseId);
            if (caseEntity != null)
            {
                var employeeEntity = await _context.Employees.FirstOrDefaultAsync(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName && x.Email == employee.Email && x.PhoneNumber == employee.PhoneNumber);
                if (employeeEntity == null)
                {
                    employeeEntity = new EmployeeEntity
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber
                    };
                    _context.Employees.Add(employeeEntity);
                    await _context.SaveChangesAsync();
                }

                var commentEntity = new CommentEntity
                {
                    Text = comment.Text,
                    CreatedAt = DateTime.Now,
                    CaseId = caseId,
                    EmployeeId = employeeEntity.Id
                };
                _context.Comments.Add(commentEntity);
                await _context.SaveChangesAsync();
            }
        }

        //public static async Task<IEnumerable<Comments>> GetCaseDetailsAsync(int caseId)
        //{
        //    var caseEntity = await _context.Cases.FindAsync(caseId);

        //    if (caseEntity == null)
        //    {
        //        return null;
        //    }

        //    var employeeEntity = await _context.Employees.FindAsync(caseEntity.EmployeeId);
        //    var commentsEntities = await _context.Comments
        //        .Where(c => c.CaseId == caseId)
        //        .OrderByDescending(c => c.CreatedAt)
        //        .ToListAsync();

        //    var comments = commentsEntities.Select(c => new Comments
        //    {
        //        Text = c.Text,
        //        CreatedAt = c.CreatedAt,
        //        Employee = new Employees
        //        {
        //            FirstName = c.Employee.FirstName,
        //            LastName = c.Employee.LastName,
        //            Email = c.Employee.Email,
        //            PhoneNumber = c.Employee.PhoneNumber
        //        }
        //    }).ToList();

        //    var caseDetails = new CaseDetails
        //    {
        //        Id = caseEntity.Id,
        //        te = caseEntity.Title,
        //        Description = caseEntity.Description,
        //        CreatedAt = caseEntity.CreatedAt,
        //        Status = caseEntity.Status,
        //        Employee = new Employees
        //        {
        //            FirstName = employeeEntity.FirstName,
        //            LastName = employeeEntity.LastName,
        //            Email = employeeEntity.Email,
        //            PhoneNumber = employeeEntity.PhoneNumber
        //        },
        //        Comments = comments
        //    };

        //    return Enumerable.Empty<Comments>();
        //}


        public static async Task<IEnumerable<Comments>> GetCommentsAsync(int caseId)
        {
            var _caseEntity = await _context.Cases.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == caseId);

            if (_caseEntity != null)
            {
                return _caseEntity.Comments.Select(c => new Comments
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    CaseId = c.CaseId
                });
            }

            return Enumerable.Empty<Comments>();
        }

    }

}

//public static async Task SaveCommentAsync(Cases cases, Comments comment, Employees employee)
//{
//    var _caseEntity = new CaseEntity
//    {
//        Description = cases.Description,
//        CreatedTime = DateTime.Now,
//        Status = cases.Status,
//    };

//    var _commentEntity = new CommentEntity
//    {
//        Text = comment.Text,
//        CreatedAt = DateTime.Now,
//    };

//    var _employeeEntity = await _context.Employees.FirstOrDefaultAsync(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName && x.Email == employee.Email && x.PhoneNumber == employee.PhoneNumber);
//    if (_employeeEntity != null)
//        _commentEntity.EmployeeId = _employeeEntity.Id;
//    else
//        _commentEntity.Employee = new EmployeeEntity
//        {
//            FirstName = employee.FirstName,
//            LastName = employee.LastName,
//            Email = employee.Email,
//            PhoneNumber = employee.PhoneNumber

//        };

//    _context.Add(_commentEntity);
//    await _context.SaveChangesAsync();
//}



//public static async Task AddCommentAsync(int caseId, string comment, Employees employee)
//{
//    var _caseEntity = await _context.Cases.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == caseId);
//    if (_caseEntity != null)
//    {
//        var newComment = new CommentEntity
//        {
//            Text = comment,
//            CreatedAt = DateTime.Now,
//            CaseId = caseId
//        };
//        _caseEntity.Comments.Add(newComment);
//        _context.Update(_caseEntity);
//        await _context.SaveChangesAsync();
//    }
//}