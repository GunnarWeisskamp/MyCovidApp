using System;
using System.Linq;
using System.Threading.Tasks;
using EntityRepo.ContextActions;
using EntityRepo.CovidAppModels;
using Microsoft.Extensions.Configuration;
using MiddleWearUnitTest.Report;
using Moq;
using NUnit.Framework;

namespace MiddleWearUnitTest
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    class PatientAddressTest : BaseFixture
    {
        private IConfiguration _configuration;
        private int _IDToTest;
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
           .AddJsonFile("MiddleWearUnitTestConfig.json")
           .Build();
            _IDToTest = Convert.ToInt32(_configuration["IDToTest"]);
        }

        [Test(Description = "Get Patient Address By Id")]
        [Category("PatientAddress")]
        public async Task Test_GetPatientAddressById()
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<PatientAddress>>();
            mockSet.As<IQueryable<PatientAddress>>().Setup(m => m.Provider);
            mockSet.As<IQueryable<PatientAddress>>().Setup(m => m.Expression);
            mockSet.As<IQueryable<PatientAddress>>().Setup(m => m.ElementType);
            mockSet.As<IQueryable<PatientAddress>>().Setup(m => m.GetEnumerator());

            var mockContext = new Mock<CovidAppContext>();
            mockContext.Setup(c => c.PatientAddress).Returns(mockSet.Object);

            var patientAddressAndNextOfKin = new PatientDetailsActions();
            var result = await patientAddressAndNextOfKin.GetPatientHospitalAddressAndNextOfKinDetailsById(_IDToTest);
            var items = result.PatientAddresses.ToList();
            //check that we only got one back
            Assert.AreEqual(2, items.Count);

            //check if state equal to qld
            Assert.AreEqual("QLD", items[0].State);
        }
    }
}
