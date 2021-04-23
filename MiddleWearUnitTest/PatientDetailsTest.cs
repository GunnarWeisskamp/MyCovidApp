using System.Collections.Generic;
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
    public class PatientDetailsTest : BaseFixture
    {
        private IConfiguration _configuration;
        private string _firstName = "";
        private string _lastName = "";

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
           .AddJsonFile("MiddleWearUnitTestConfig.json")
           .Build();
            _firstName = _configuration["firstName"];
            _lastName = _configuration["lastName"];
        }

        [Test(Description = "Get Patient First Name and Last Name")]
        [Category("PatientDetails")]
        public async Task Test_GetPatientDetailsByFirstNameAndLastName()
        {
            var patientObject = new List<PatientDetails>
            {
                new PatientDetails { FirstName = "Fred", LastName = "Williams" }

            }.AsQueryable();

            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<PatientDetails>>();
            mockSet.As<IQueryable<PatientDetails>>().Setup(m => m.Provider).Returns(patientObject.Provider);
            mockSet.As<IQueryable<PatientDetails>>().Setup(m => m.Expression).Returns(patientObject.Expression);
            mockSet.As<IQueryable<PatientDetails>>().Setup(m => m.ElementType).Returns(patientObject.ElementType);
            mockSet.As<IQueryable<PatientDetails>>().Setup(m => m.GetEnumerator()).Returns(patientObject.GetEnumerator());

            var mockContext = new Mock<CovidAppContext>();
            mockContext.Setup(c => c.PatientDetails).Returns(mockSet.Object);

            var patDetails = new PatientDetailsActions();
            var patient = await patDetails.GetPatientDetailsByFirstNameAndLastName(_firstName, _lastName);

            Assert.IsTrue(patient.FirstName == _firstName && patient.LastName == _lastName);
        }
    }
}