using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;


namespace WebApps.ElementsFolder
{
    class LoginPage_Elements: BaseClass
    {
        public string loginError = "Your login failed, please try again";
        public string TermsError = "You must agree to the following terms and conditions to login";
        public string TermsURL = "http://termsandconditions.afi.cloud/";
        public string forgotPasswordErrorMessage = "Farm Name is required";
        public string forgotPasswordCorrectMessage = "Afifarm will send password reset instructions to the email address associated with your farm";
       
        
        private IWebDriver driver;
        public  LoginPage_Elements(IWebDriver driver)
        {
            this.driver = driver;
            
            PageFactory.InitElements(driver, this);
        }

        #region elements

        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div.form-group.user-icon > input")]
        public IWebElement usernameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div.form-group.lock-icon.mb-0 > input")]
        public IWebElement passwordField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#inline-popups > button")]
        public IWebElement loginBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#afiLoginFailureModal > p")]
        public IWebElement loginErrorText { get; set; }   
        
        [FindsBy(How = How.CssSelector, Using = "body > ngb-modal-window > div > div > div.modal-footer > button")]
        public IWebElement loginErrorBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#loginPasswordErrorMessage")]
        public IWebElement PasswordError { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div:nth-child(4) > div")]
        public IWebElement TermsErrorText { get; set; }

                
        [FindsBy(How = How.ClassName, Using = "terms-link")]
        public IWebElement TermsLink { get; set; }

              
        [FindsBy(How = How.CssSelector, Using = "#inline-popups > div:nth-child(5) > div")]
        public IWebElement forgotPasswordBtn { get; set; }
              
        [FindsBy(How = How.CssSelector, Using = "#loginFarmAccountErrorMessage2")]
        public IWebElement forgotPasswordError { get; set; }
              
        [FindsBy(How = How.CssSelector, Using = "#forgotPasswordCardBack > div > div > div > div > p")]
        public IWebElement forgotPasswordMessage1 { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#forgotPasswordCardBack > div > div > div > div > button")]
        public IWebElement ok2Btn { get; set; }

        #endregion


        public bool wrongUserName()
        {

            MainloadingWait();
            usernameField.SendKeys(userName+"s1235481");
            passwordField.SendKeys(Password);
            acceptTerms();
            loginBtn.Click();
            element_clickable(loginErrorBtn);
            return loginErrorText.Text.Equals(loginError);
        }
        public bool wrongPassword()
        {
            MainloadingWait();
            usernameField.SendKeys(userName );
            passwordField.SendKeys(Password + "s1235481");
            acceptTerms();
            loginBtn.Click();
            element_clickable(loginErrorBtn);
            return loginErrorText.Text.Equals(loginError);


        }
        public bool noTerms()
        {
            usernameField.SendKeys(userName);
            passwordField.SendKeys(Password + "s1235481");
            loginBtn.Click();
            return TermsErrorText.Text.Equals(TermsError);
        }
        public bool TermsAndConditionsLink()
        {
            TermsLink.Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            string currentURL = driver.Url;

            return TermsURL.Equals(currentURL);
        }

        public bool forgotPassword1()
        {
            forgotPasswordBtn.Click();
            return forgotPasswordError.Text.Equals(forgotPasswordErrorMessage);
        }      
        public bool forgotPassword2()
        {
            usernameField.SendKeys(userName);
            forgotPasswordBtn.Click();
            element_clickable(ok2Btn);
            Thread.Sleep(300);
            return forgotPasswordMessage1.Text.Equals(forgotPasswordCorrectMessage);
        }
    }
}
