using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebApps.ElementsFolder;

namespace WebApps.BaseFolder
{
   
    class BaseClass
    {
        public static  IWebDriver driver;
        private string appURL = "https://afimilkcockpitqa.z6.web.core.windows.net/#/login";
        public static ExtentReports extent;
        public string reportPath = @"C:\Webapps Report\Cockpit";

        public string userName = "demo001";
        public string Password = "1234";

        public static int scrennshotNumber = 0;
        private static string screenshotpath;

        public string Fullscreenshotpath;

        public  ExtentTest test;
        private string disc;

        public static string version;


        [OneTimeSetUp]
        public void Setup()
        {
            if (extent == null) {
                Rectangle resolution = Screen.PrimaryScreen.Bounds;

                driver = new ChromeDriver();
                extent = new ExtentReports();


                string apiVersion = getApiVersion();
                string FunctionVersion = getFunctionVersion();
                extent.AddSystemInfo("Browser", "Chrome");
                extent.AddSystemInfo("Browser Version ", "dsa");
                extent.AddSystemInfo("Resolution", resolution.ToString());
                extent.AddSystemInfo("Function version", FunctionVersion.Replace("\"", ""));
                extent.AddSystemInfo("Api Version", apiVersion);
                //get version
                driver.Navigate().GoToUrl(appURL);
            MainloadingWait();
            var spark = new ExtentSparkReporter(reportPath + currentVersion()+ @"\Report.html");
            screenshotpath = reportPath + currentVersion() + @"\";
            extent.AttachReporter(spark);
            driver.Close();
            }
        }

        private string getFunctionVersion()
        {
            driver.Navigate().GoToUrl("https://afimilkcockpitfunctionappqa.azurewebsites.net/api/version?code=ejK8mAWwatCS3TaNoHAORwq652dRIJfxFnaX8g5n1Ev4gYsr6tT1gg==");
            string version = driver.FindElement(By.CssSelector("body > pre")).Text.Split(',')[0].Split(':')[1];
            return version;
        }

        private string getApiVersion()
        {
            driver.Navigate().GoToUrl("https://afimilkcockpit-qa.azurewebsites.net/version");
            string version = driver.FindElement(By.CssSelector("#folder0 > div.opened > div:nth-child(2) > span:nth-child(2)")).Text;
            return version;
        }

        [OneTimeTearDown]
        public void thisIsTheEnd()
        {
            extent.Flush();
        }
        [SetUp]
        public void openAndRun()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(appURL);
            driver.Manage().Window.Maximize();

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Properties.Get("Description").ToString());
                
                disc = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
                
            }
            catch (Exception e)
            {
                disc = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
                this.test = extent.CreateTest(TestContext.CurrentContext.Test.Properties.Get("Description").ToString());

                throw (e);
            }
        }

        [TearDown]
        public void close()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        test.Log(logstatus, "Test ended with error " + test.AddScreenCaptureFromPath(screenShot(disc)));
                        break;
                    default:
                        logstatus = Status.Pass;
                        test.Log(logstatus, "Pass" + test.AddScreenCaptureFromPath(screenShot(disc)));
                        break;
                }
               
            }
            catch (Exception e)
            {
                throw (e);
            }
            //
            driver.Quit();
        }

        public string screenShot(string testDisc)
        {
            scrennshotNumber++;
            //Fullscreenshotpath = screenshotpath  + testDisc + scrennshotNumber.ToString() + ".png";
            Fullscreenshotpath = screenshotpath  + scrennshotNumber.ToString() + ".png";

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Fullscreenshotpath, ScreenshotImageFormat.Png);
            return Fullscreenshotpath;
        }

        private string currentVersion()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(c => c.FindElement(By.ClassName("ml-5")));
            version = driver.FindElement(By.ClassName("ml-5")).Text;
            return version;
        }

        public void acceptTerms()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("document.querySelector('.acceptTerms').click()");
        }

        public void MainloadingWait()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.ElementExists((By.ClassName("ml-5"))));

        }

        public void logger()
        {
            Thread.Sleep(800);
            test.Info(MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot(disc)).Build());
        }     
        public void logger(string info)
        {
            Thread.Sleep(550);
            test.Info(info,MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot(disc)).Build());
        }      
        public void loggerElement(string info,IWebElement elem)
        {
            Thread.Sleep(550);
            test.Info(info,MediaEntityBuilder.CreateScreenCaptureFromPath(MakeElemScreenshot(elem)).Build());
        }

        public void element_clickable(IWebElement element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            var element1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            Thread.Sleep(400);
        }

        public void login()
        {
            LoginPage_Elements LoginPageE = new LoginPage_Elements(driver);
            MainloadingWait();
            LoginPageE.usernameField.SendKeys(userName);
            LoginPageE.passwordField.SendKeys(Password);
            acceptTerms();
            LoginPageE.loginBtn.Click();
            LoadingWait(10);
        }
        public void goBackToCockpit()
        {
            driver.Navigate().GoToUrl(appURL);
            LoadingWait2(20);
            login();
        }
        public void LoadingWait(int TimeToWait)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(TimeToWait)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.CssSelector("#loader > div > h6"))));
                new WebDriverWait(driver, TimeSpan.FromSeconds(TimeToWait)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.InvisibilityOfElementLocated((By.CssSelector("#loader > div > h6"))));

            }
            catch (Exception)
            {

            
            }

        }       
        public void LoadingWait2(int TimeToWait)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(TimeToWait)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.ClassName("cube-wrapper"))));
                new WebDriverWait(driver, TimeSpan.FromSeconds(TimeToWait)).Until(OpenQA.Selenium.Support.UI.ExpectedConditions.InvisibilityOfElementLocated((By.ClassName("cube-wrapper"))));

            }
            catch (Exception)
            {

            
            }

        }

        public void closeWindow()
        {
            Thread.Sleep(550);
            IList<IWebElement> c = driver.FindElements(By.ClassName("close"));
            foreach (IWebElement item in c)
            {
                if (item.Displayed) { item.Click(); }
                Thread.Sleep(550);
            }
        }
        public void loggerNoScreenshotList(List<string> info)
        {
            test.Info(MarkupHelper.CreateOrderedList(info));
        }

        public string MakeElemScreenshot( IWebElement elem)
        {
            Screenshot myScreenShot = ((ITakesScreenshot)driver).GetScreenshot();
            string path = @"c:\button.png";
            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));
            Bitmap elemScreenshot = screen.Clone(new Rectangle(elem.Location, elem.Size), screen.PixelFormat);
            elemScreenshot.Save(path, ImageFormat.Png);
            screen.Dispose();

            //test.Info("test", MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());

            return path;
        }
    }
}
