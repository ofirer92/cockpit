using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;

namespace WebApps.ElementsFolder
{
    class cockpitAdmin : BaseClass
    {
        private string link2= "https://afimilkcockpitadminqa.z6.web.core.windows.net/";

        private IWebDriver driver;
        public cockpitAdmin(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void setdata()
        {
            driver.Navigate().GoToUrl(link2);
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector("#inline-popups > div.form-group.user-icon > input")).SendKeys(userName);
            driver.FindElement(By.CssSelector("#inline-popups > div.form-group.lock-icon.mb-0 > input")).SendKeys(Password);
            driver.FindElement(By.Id("acceptTerms")).Click();
            Thread.Sleep(400);
            driver.FindElement(By.CssSelector("#inline-popups > div.form-group.lock-icon.mb-0 > input")).SendKeys(Keys.Enter);
            Thread.Sleep(5000);


            IList<IWebElement> data = driver.FindElements(By.ClassName("kpi-result"));

            for (int i = 0; i < data.Count; i++)
            {
                for (int z = 0; z < 12; z++)
                {
                    try
                    {
                        data[i].SendKeys(Keys.Control + "a");
                        data[i].SendKeys((z * 10).ToString());
                    }
                    catch
                    {

                    }

                    i++;
                }
                i--;
            }
        }
        public void setInventory(int heifers, int Lact1,int Lact2, int Lact3)
        {
            driver.FindElement(By.CssSelector("body > app-root > app-dashboard > div.bg-dark > app-tool-bar > div > div:nth-child(4)")).Click();
            element_clickable(driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success")));
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success")).Click();

            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(heifers.ToString());
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(3) > input")).SendKeys(Keys.Control +"a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(3) > input")).SendKeys(Lact1.ToString());
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(4) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(4) > input")).SendKeys(Lact2.ToString());
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(5) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(5) > input")).SendKeys(Lact3.ToString());

            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(Keys.Control + "a");
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(Keys.Delete);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(heifers.ToString());


            driver.FindElement(By.CssSelector("body > app-root > app-dashboard > div.bg-dark > app-tool-bar > div > div:nth-child(3)")).Click();
            Thread.Sleep(6000);
            


        }
        public void setInventory2(int heifers, int Lact1, int Lact2, int Lact3)
        {
            
            driver.FindElement(By.CssSelector("body > app-root > app-dashboard > div.bg-dark > app-tool-bar > div > div:nth-child(4)")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > summary")).Click();
            // heifers
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(Keys.Delete);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(2) > input")).SendKeys(Lact1.ToString());

            //1st lactation
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(3) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(3) > input")).SendKeys(Keys.Delete);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(3) > input")).SendKeys(Lact1.ToString());


            //2nd lactation
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(4) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(4) > input")).SendKeys(Keys.Delete);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(4) > input")).SendKeys(Lact1.ToString());

            //3rd lactation
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(5) > input")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(5) > input")).SendKeys(Keys.Delete);
            driver.FindElement(By.CssSelector("#mySidenav > app-menu > div > details.success > div > div > div:nth-child(5) > input")).SendKeys(Lact1.ToString());


            driver.FindElement(By.CssSelector("body > app-root > app-dashboard > div.bg-dark > app-tool-bar > div > div:nth-child(3)")).Click();
            Thread.Sleep(10000);
        }

    }
}
