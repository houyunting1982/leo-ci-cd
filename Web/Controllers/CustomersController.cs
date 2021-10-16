using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase {
        private readonly CustomerContext _context;
        public CustomersController(CustomerContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll() {
            return await _context.Customers.ToArrayAsync();
        }

        [HttpPost]
        public async Task<Customer> Add([FromBody] Customer c) {
            _context.Customers.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
    }
}
