using CustomerWebApi.Interfaces;
using CustomerWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Services
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext) : base(dbContext)
        {
        }

        public IPage<Customer> GetPage(int pageIndex, int pageSize)
        {
            var allCount = this.GetAll().Count();
            var items = this.GetAll().AsNoTracking().OrderBy(x => x.Surname)
                .Skip(pageSize * pageIndex).Take(pageSize).ToList();
            return new Page<Customer>
            {
                AllCount = allCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items
            };
        }
    }
}
