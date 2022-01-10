using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Today.Helpers;

namespace Today.Elements
{
    internal class LicensingPOM
    {
        private IWebDriver driver;
        public LicensingPOM(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div.form-group.user-icon > input")]
        private IWebElement usernameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div.form-group.lock-icon.mb-0 > input")]
        private IWebElement passwordField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#inline-popups > button")]
        private IWebElement loginBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "btn-ok")]
        private IWebElement okBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "errortag")]
        private IWebElement errorText { get; set; }


        public void acceptTerms()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("document.querySelector('.acceptTerms').click()");
        }

        internal bool assertNearExpired(string userName, string password)
        {
            Help h = new Help();
            usernameField.SendKeys(userName);
            passwordField.SendKeys(password);
            acceptTerms();
            loginBtn.Click();
            Thread.Sleep(1000);
            if(!errorText.Displayed || !errorText.Text.Equals("Your license is about to expire. Please contact support to renew"))
            {
                return false;
            }
            okBtn.Click();
            try
            {
                h.MainloadingWait(driver);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
                internal bool assertExpired(string userName, string password)
        {
            Help h = new Help();
            usernameField.SendKeys(userName);
            passwordField.SendKeys(password);
            acceptTerms();
            loginBtn.Click();
            Thread.Sleep(1000);
            if(!errorText.Displayed || !errorText.Text.Equals("You do not have a valid license for this product. Please contact support."))
            {
                return false;
            }
            okBtn.Click();
            try
            {
                h.MainloadingWait(driver);
            }
            catch (Exception)
            {

                return true;
            }
            return false;
        }

        internal bool assertLogin(string userName,string password)
        {
            Help h = new Help();
            usernameField.SendKeys(userName);
            passwordField.SendKeys(password);
            acceptTerms();
            loginBtn.Click();
            try
            {
                h.MainloadingWait(driver);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


    }
}
