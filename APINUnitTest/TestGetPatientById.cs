using System.Collections.Generic;
using APINUnitTest.Setup;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace APINUnitTest
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class TestGetPatientById : SetupGlobals
    {
        private string _baseURL = "";
        private string _IDToTest = "";
        [OneTimeSetUp]
        public void Setup()
        {
            _baseURL = _readConfigFileCls.GetConfigString("BaseURL");
            _IDToTest = _readConfigFileCls.GetConfigString("IDToTest");
        }

        [Test(Description = "Get Patient By Id Check")]
        [Category("GetPatientById")]
        public void GetPatientById()
        {
            // call api
            RestClient client = new RestClient(_baseURL);
            RestRequest request = new RestRequest("PatientDetails/GetPatientHospitalAddressAndNextOfKinDetailsById?id=" + _IDToTest, Method.GET);

            // execute query
            IRestResponse response = client.Execute(request);

            var patientObject = JsonConvert.DeserializeObject<List<EntityRepo.CovidAppModels.PatientDetails>>(response.Content);

            // do the assert
            Assert.That(patientObject.Count, Is.EqualTo(1));
        }

    }
}
