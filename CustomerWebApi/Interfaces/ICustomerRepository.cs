using CustomerWebApi.Models;
using CustomerWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Interfaces
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        IPage<Customer> GetPage(int pageIndex, int pageSize);
    }
}
