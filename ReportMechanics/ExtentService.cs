using System;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;

namespace AventStack.ExtentReports.Tests
{
    public class ExtentService
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentService()
        {

            var fullName = TestContext.CurrentContext.Test.FullName;
            fullName = fullName + ".html";
            var htmlReporter = new ExtentV3HtmlReporter(TestContext.CurrentContext.TestDirectory + "\\" + fullName);
            htmlReporter.Config.DocumentTitle = TestContext.CurrentContext.Test.FullName;
            htmlReporter.Config.ReportName = TestContext.CurrentContext.Test.FullName;
            htmlReporter.Config.Theme = Theme.Standard;
            Instance.AttachReporter(htmlReporter);
        }

        private ExtentService()
        {
        }
    }
}