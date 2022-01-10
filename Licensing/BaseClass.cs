using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Today.Helpers;
using Today.Reports;
using Help = Today.Helpers.Help;

namespace Today.Base
{
    class BaseClass
    {
        public static IWebDriver driver;

        public string userName = "setup5.4.2";
        public string Password = "1234";
        public string farmCode = "1076002902";

        //Links QA\Alpha\GA
        #region
        private string Alpha = "https://afimilkabcalpha.z6.web.core.windows.net/#/login";
        private string QA = "https://afimilkabcqa.z6.web.core.windows.net/#/login";
        private string GA = "https://afimilkbackupweb.z6.web.core.windows.net/#/login"; //TBD
        private string dev = "https://afimilkabcdev.z6.web.core.windows.net/#/login";
        #endregion
        public Reporter reporter = new Reporter();

        public string reportPath = @"C:\Webapps Report\Today";
        public static string version;

        //לפני כל הטסטים

        [OneTimeSetUp]
        public void Setup()
        {

            Help help = new Help();

            if (Reporter.extent == null)
            {
                driver = new ChromeDriver();
                Reporter.extent = new ExtentReports();

                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                Reporter.extent.AddSystemInfo("Browser", "Chrome");
                Reporter.extent.AddSystemInfo("Resolution", resolution.ToString());
   


                driver.Navigate().GoToUrl(QA);

                //get version
                version = help.currentVersion(driver);
                version = version.Replace(":", "");
                //set path
                reporter.setPath(reportPath, version);

                driver.Close();
            }
        }


        //אחרי כל הטסטים
        [OneTimeTearDown]
        public void thisIsTheEnd()
        {
            Reporter.extent.Flush();

        }


        //לפני כל טסט
        [SetUp]
        public void openAndRun()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(QA);
            driver.Manage().Window.Maximize();
           
            reporter.startReporting();
        }


        //אחרי כל טסט
        [TearDown]
        public void close()
        {
            reporter.testReporting(driver);
            driver.Quit();
        }



    }
}
