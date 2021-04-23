using OpenQA.Selenium;
using SeleniumTests.SeleniumCommonFunc;

namespace SeleniumTests.PageModel
{
    public class LoginPageModel : CommonFun
    {
        private readonly IWebDriver _driver;
        public LoginPageModel(IWebDriver driver)
        {
            _driver = driver;
        }

        public string LoginAndConfirm(string UserName, string Password)
        {
            string retValue = string.Empty;
            ClickOnElementAndSendData(_driver, "ID", "loginLink", "Button", "", 10);
            ClickOnElementAndSendData(_driver, "ID", "userName", "Input", UserName, 10);
            ClickOnElementAndSendData(_driver, "ID", "password", "Input", Password, 10);
            ClickOnElementAndSendData(_driver, "ID", "loginSubmit", "Button", "", 10);
            return GetElementValue(_driver, "ID", "currentLoggedOnUser", "Label", 10);
        }

    }
}
