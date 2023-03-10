using Microsoft.EntityFrameworkCore;
using SystemManager.Contexts;
using SystemManager.Models.Entities;
using SystemManager.Models;

namespace SystemManager.Services
{
    internal class EmployeeService
    {
        private readonly static DataContext _context = new();

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

            }

            return (_casess, _customers);
        }

        public static async Task<Cases> GetAsync(int id)
        {
            var _cases = await _context.Cases.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (_cases != null)
                return new Cases
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
        public static async Task<bool> DeleteAsync(int id)
        {
            var _cases = await _context.Cases.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (_cases != null)
            {
                _context.Remove(_cases);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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
