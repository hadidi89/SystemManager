
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
                Status= cases.Status,
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
                };

                _casess.Add(newCase);

                if (_cases.Customer != null)
                {
                    var newCustomer = new Customers
                    {
                        Id = _cases.Customer.Id,
                        FirstName = _cases.Customer.FirstName,
                        LastName = _cases.Customer.LastName,
                        Email = _cases.Customer.Email,
                        PhoneNumber = _cases.Customer.PhoneNumber

                    };

                    _customers.Add(newCustomer);
                }
            }

            return (_casess, _customers);
        }

    }
}
