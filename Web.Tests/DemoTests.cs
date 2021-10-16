using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Web.Controllers;

using Xunit;

namespace Web.Tests {
    public class DemoTests {
        [Fact]
        public async Task CustomerIntegrationTest() {
            // Create a DB context
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            var context = new CustomerContext(optionsBuilder.Options);

            // Always start from the fresh database
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Create a controller
            var controller = new CustomersController(context);

            // Add a customer
            await controller.Add(new Customer {
                CustomerName = "Pippi"
            });

            // Check: Does GetAll return the added customer?
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("Pippi", result.First().CustomerName);
        }
    }
}
