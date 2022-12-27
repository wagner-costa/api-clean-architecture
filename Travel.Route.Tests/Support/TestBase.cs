using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Travel.Route.Data.Context;

namespace Travel.Route.Tests.Support
{
    public class TestBase
    {
        public AppDbContext ContextInMemory { get; set; }

        public Mock<IConfiguration> Configuration { get; set; }

        [TestInitialize]
        public virtual void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            ContextInMemory = new AppDbContext(options);

            Configuration = new Mock<IConfiguration>();
        }
    }
}
