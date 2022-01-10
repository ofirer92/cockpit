using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace WebApps.TestFolder
{
    class LoaginPage: BaseClass
    {
        //public ExtentTest test;


        LoginPage_Elements LoginPageE;
        [SetUp]
        public void setTag()
        {
            test.AssignCategory("loading page");
        }
        [Test]
        [Description("wrong username")]
        public void test1()
        {
            
            LoginPageE = new LoginPage_Elements(driver);
            Assert.IsTrue(LoginPageE.wrongUserName());

        }
        [Test]
        [Description("wrong password")]
        public void test2()
        {
            LoginPageE = new LoginPage_Elements(driver);
            Assert.IsTrue(LoginPageE.wrongPassword());
        }
        [Test]
        [Description("accept terms and conditions")]
        public void test3()
        {
            LoginPageE = new LoginPage_Elements(driver);

            Assert.IsTrue(LoginPageE.noTerms());

        }
        [Test]
        [Description(" terms and conditions link")]
        public void test4()
        {
            LoginPageE = new LoginPage_Elements(driver);
            Assert.IsTrue(LoginPageE.TermsAndConditionsLink());

        }      
        
        
        [Test]
        [Description("Forgot password")]
        public void test5()
        {
            LoginPageE = new LoginPage_Elements(driver);
            Assert.IsTrue(LoginPageE.forgotPassword1());
            logger("no user name case");
            Assert.IsTrue(LoginPageE.forgotPassword2());
        }
    }
}
