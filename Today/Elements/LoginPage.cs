using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Today.Elements
{
    class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
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


        [FindsBy(How = How.ClassName, Using = "alert")]
        private IWebElement TermserrorText { get; set; }


        [FindsBy(How = How.ClassName, Using = "terms-link")]
        private IWebElement TermsLink { get; set; }


        public void setLoginDetails(string username, string password)
        {
            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
        }

        public void acceptTerms()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("document.querySelector('.acceptTerms').click()");
        }

        public void clickLogin()
        {
            loginBtn.Click();
        }

        public bool errorDisplayed()
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists((By.ClassName("errortag"))));
                if (errorText.Text.Equals("Login unsuccessful. Invalid username or password.") && okBtn.Enabled)
                {
                    return true;
                }
                return false;

            }
            catch
            {

                return false;
            }
        }
        public bool TermsErrorDisplayed()
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists((By.ClassName("alert"))));
                if (TermserrorText.Text.Equals("You must agree to the following terms and conditions to login"))
                {
                    return true;
                }
                return false;

            }
            catch
            {

                return false;
            }
        }
        public string TermWebsite()
        {
            TermsLink.Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(500);
            string currentURL = driver.Url;
            return currentURL;



        }


    }
}
