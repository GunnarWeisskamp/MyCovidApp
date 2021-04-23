using NUnit.Framework;
using SeleniumTests.PageModel;
using SeleniumTests.Setup;

namespace SeleniumTests
{
    public class LoginPage : SetupGlobals
    {
        LoginPageModel _loginPageModelCls = null;
        string _BaseURL = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;


        [SetUp]
        public void Setup()
        {
            _BaseURL = _readConfigFileCls.GetConfigString("BaseURL");
            _UserName = _readConfigFileCls.GetConfigString("UserName");
            _Password = _readConfigFileCls.GetConfigString("Password");
            driver.Url = _BaseURL;
            _loginPageModelCls = new LoginPageModel(driver);

        }

        [Test(Description = "Test login area")]
        [Category("Login")]
        public void LoginAndCheckUserNameAndPassword()
        {
            string result = string.Empty;
            result = _loginPageModelCls.LoginAndConfirm(_UserName, _Password);
            Assert.IsTrue(result == "Current logged on user: Fred");
        }
    }
}