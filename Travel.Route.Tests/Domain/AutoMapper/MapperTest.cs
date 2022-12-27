using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Route.Tests.Support;

namespace Travel.Route.Tests.Domain.AutoMapper
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void ValidateMapper()
        {
            var mapper = AutoMapperHelper.GetMapperConfiguration();
            mapper.AssertConfigurationIsValid();
        }
    }
}
