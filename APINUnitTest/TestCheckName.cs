using APINUnitTest.Setup;
using Nancy.Json;
using NUnit.Framework;
using RestSharp;

namespace APINUnitTest
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]

    public class TestCheckName : SetupGlobals
    {
        private string _baseURL = "";
        private string _IDToTest = "";
        private string _firstNameNegative = "";
        private string _firstNamePositive = "";

        [OneTimeSetUp]
        public void Setup()
        {
            _baseURL = _readConfigFileCls.GetConfigString("BaseURL");
            _IDToTest = _readConfigFileCls.GetConfigString("IDToTest");
            _firstNameNegative = _readConfigFileCls.GetConfigString("FirstNameNegative");
            _firstNamePositive = _readConfigFileCls.GetConfigString("FirstNamePositive");
        }

        [Test(Description = "Negative First Name Check")]
        [Category("First Name Check")]
        public void CheckFirstNameNegativeTest()
        {
            // call api
            RestClient client = new RestClient(_baseURL);
            RestRequest request = new RestRequest("PatientDetails/GetPatientHospitalAddressAndNextOfKinDetailsById?id=" + _IDToTest, Method.GET);

            // execute query
            IRestResponse response = client.Execute(request);
            JavaScriptSerializer javaScriptSer = new JavaScriptSerializer();
            EntityRepo.CovidAppModels.PatientDetails patientObject = javaScriptSer.Deserialize<EntityRepo.CovidAppModels.PatientDetails>(response.Content);

            // do the assert
            Assert.That(patientObject.FirstName, !Is.EqualTo(_firstNameNegative));
        }

        [Test(Description = "Positive First Name Check")]
        [Category("First Name Check")]
        public void CheckFirstNamePositiveTest()
        {
            // call api
            RestClient client = new RestClient(_baseURL);
            RestRequest request = new RestRequest("PatientDetails/GetPatientHospitalAddressAndNextOfKinDetailsById?id=" + _IDToTest, Method.GET);

            // execute query
            IRestResponse response = client.Execute(request);

            JavaScriptSerializer javaScriptSer = new JavaScriptSerializer();
            EntityRepo.CovidAppModels.PatientDetails patientObject = javaScriptSer.Deserialize<EntityRepo.CovidAppModels.PatientDetails>(response.Content);


            // do the assert
            Assert.That(patientObject.FirstName, Is.EqualTo(_firstNamePositive));
        }
    }
}