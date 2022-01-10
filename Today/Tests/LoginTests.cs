using System;
using NUnit.Framework;
using Today.Base;
using Today.Elements;

namespace Today.Tests
{

    class LoginTests : BaseClass
    {
        LoginPage loginPage;
        [SetUp]
        public void test0()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        [Description("wrong username")]
        public void test1()
        {
            loginPage.setLoginDetails(userName + "1", Password);
            loginPage.acceptTerms();
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.errorDisplayed());
            Assert.IsTrue(loginPage.errorDisplayed());
        }

        [Test]
        [Description("Wrong password")]
        public void test2()
        {
            loginPage.setLoginDetails(userName, Password + "1");
            loginPage.acceptTerms();
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.errorDisplayed());
        }
        [Test]
        [Description("Terms and Conditions- Must accept terms")]
        public void test3()
        {
            loginPage.setLoginDetails(userName, Password);
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.TermsErrorDisplayed());
        }

        [Test]
        [Description("Terms and Conditions website")]
        public void test4()
        {
            string termsLink = "http://termsandconditions.afi.cloud/";
            loginPage.setLoginDetails(userName, Password);
            Assert.AreEqual(termsLink, loginPage.TermWebsite());
        }




    }
}
