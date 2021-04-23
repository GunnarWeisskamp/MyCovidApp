using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WaitHelper = SeleniumExtras.WaitHelpers;

namespace SeleniumTests.SeleniumCommonFunc
{
    public class CommonFun
    {
        protected void ClickOnElementAndSendData(IWebDriver driver, string elementIdentifier, string elementSig, string elementType,
                                              string dataToSend = "", int timeOut = 0)
        {

            IWebElement element = GetElement(driver, elementIdentifier, elementSig, timeOut);
            switch (elementType)
            {
                case "Button":
                    element.Click();
                    break;
                case "Input":
                    element.Click();
                    element.SendKeys(dataToSend);
                    break;
            }
        }

        protected string GetElementValue(IWebDriver driver, string elementIdentifier, string elementSig, string elementType, int timeOut = 0)
        {
            IWebElement element = GetElement(driver, elementIdentifier, elementSig, timeOut);
            string retValue = String.Empty;
            switch (elementType)
            {
                case "Label":
                    retValue = element.Text;
                    break;
                case "Input":
                    retValue = element.GetAttribute("value");
                    break;
            }
            return retValue;
        }

        private IWebElement GetElement(IWebDriver driver, string elementIdentifier, string elementSig, int timeOut)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            IWebElement element = null;
            switch (elementIdentifier)
            {
                case "ID":
                    wait.Until(WaitHelper.ExpectedConditions.ElementIsVisible(By.Id(elementSig)));
                    element = wait.Until(e => e.FindElement(By.Id(elementSig)));
                    break;
                case "Class":
                    wait.Until(WaitHelper.ExpectedConditions.ElementIsVisible(By.ClassName(elementSig)));
                    element = wait.Until(e => e.FindElement(By.ClassName(elementSig)));
                    break;
            }

            return element;
        }

    }
}
