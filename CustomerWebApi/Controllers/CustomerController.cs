using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Services;
using CustomerWebApi.Models;
using CustomerWebApi.Interfaces;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository repository;

        public CustomerController(CustomerContext context, ICustomerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Customer> All()
        {
            return this.repository.GetAll().AsEnumerable();
        }

        [HttpGet("[action]/{pageIndex:int}")]
        public IPage<Customer> Pages(
            int pageIndex = 0,
            [FromQuery]int pageSize = 10)
        {
            return repository.GetPage(pageIndex, pageSize);
        }

        [HttpGet("{id}", Name = "Get")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await this.repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await this.repository.CreateAsync(customer);

            return CreatedAtAction("GetById", new { id = customer.Id }, customer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (customer.Id != id)
            {
                return BadRequest(new { id = "Invalid value." });
            }

            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await this.repository.UpdateAsync(id, customer);
            return new ObjectResult(customer);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await this.repository.DeleteAsync(id);
        }

    }
}
