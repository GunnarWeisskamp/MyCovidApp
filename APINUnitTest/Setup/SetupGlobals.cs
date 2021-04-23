using AventStack.ExtentReports;
using AventStack.ExtentReports.Tests;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace APINUnitTest.Setup
{
    public class SetupGlobals : ReadConfigFile
    {
        public ReadConfigFile _readConfigFileCls = null;
        [OneTimeSetUp]
        public void Setup()
        {
            _readConfigFileCls = new ReadConfigFile();
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            ExtentService.Instance.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            ExtentTestManager.CreateMethod(GetType().Name, TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.Properties.Get("Category").ToString()).AssignCategory(TestContext.CurrentContext.Test.Properties.Get("Category").ToString());
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }


    }
}
