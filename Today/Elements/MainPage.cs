using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using Today.Helpers;
using Today.Reports;

namespace Today.Elements
{
    class MainPage
    {
        private IWebDriver driver;
        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }





        [FindsBy(How = How.CssSelector, Using = "#milk-tank > div > div > div > div > div.whiteBlock.panel-heading > div:nth-child(2) > svg")]
        private IWebElement milkproductionDrillDown { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#twentyfour_hour > div > div > div > div > div > div > div > div:nth-child(2) > svg")]
        private IWebElement milk24HrDrillDown { get; set; }



        [FindsBy(How = How.CssSelector, Using = "#milk-container > div > div > div > div > div.whiteBlock.panel-heading > div:nth-child(2) > svg")]
        private IWebElement milkingSessionDrillDown { get; set; }


        [FindsBy(How = How.ClassName, Using = "confirm")]
        private IWebElement apply { get; set; }


        #region

        //Daily milk production Data
        [FindsBy(How = How.CssSelector, Using = "#milk-tank > div > div > div > div > div.flipper > div.front-card > div.last-milk-tanks > div")]
        public IWebElement  lastMilkProduction { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#milk-tank > div > div > div > div > div.flipper > div.front-card > div.average-milk-tanks > div")]
        private IWebElement  avgMilkProduction { get; set; }

        //Milking sessions data    
        [FindsBy(How = How.CssSelector, Using = "#milk-container > div > div > div > div > div.flipper > div.front-card > div.last-milk-urns > div")]
        private IWebElement  lastMilkSession { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#milk-container > div > div > div > div > div.flipper > div.front-card > div.average-milk-urns > div")]
        private IWebElement  avgMilkSession { get; set; }
        

        //24Hr 
        [FindsBy(How = How.ClassName, Using = "#twentyfour_hour > div > div > div > div > div > div > table > tr:nth-child(2) > td:nth-child(1) > div > div.front-card > div.last-milk-tanks > div.average.last-milk-tank-text")]
        private IWebElement  last24Milk { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#twentyfour_hour > div > div > div > div > div > div > table > tr:nth-child(2) > td:nth-child(1) > div > div.front-card > div.average-milk-tanks > div.average.last-milk-tank-text")]
        private IWebElement  avg24Milk { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#twentyfour_hour > div > div > div > div > div > div > table > tr:nth-child(2) > td.widedisplay > table > tr > table:nth-child(1) > tr > td:nth-child(2) > table > tr > td:nth-child(1) > div > p.daily-number.mb-0")]
        private IWebElement  fat24Last { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#twentyfour_hour > div > div > div > div > div > div > table > tr:nth-child(2) > td.widedisplay > table > tr > table:nth-child(1) > tr > td:nth-child(2) > table > tr > td:nth-child(2) > div > p.daily-number.mb-0")]
        private IWebElement fat24avg { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#protein-details > tr > td:nth-child(2) > table > tr > td:nth-child(1) > div > p.daily-number.mb-0")]
        private IWebElement  protein24Last { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#protein-details > tr > td:nth-child(2) > table > tr > td:nth-child(2) > div > p.daily-number.mb-0")]
        private IWebElement protein24avg { get; set; }
        




        #endregion

        [FindsBy(How = How.Id, Using = "dropdownLanguage")]
        private IWebElement LangDropDown { get; set; }

        //user settings
        #region
        [FindsBy(How = How.Id, Using = "settingsBtn")]
        private IWebElement settingBtn { get; set; }

        [FindsBy(How = How.Id, Using = "dropdownUnits")]
        private IWebElement UnitsDropDown { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#settingsModal > div.dialog-body > div > div:nth-child(2) > div > div > button:nth-child(1)")]
        private IWebElement Kg { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#settingsModal > div.dialog-body > div > div:nth-child(2) > div > div > button:nth-child(2)")]
        private IWebElement Lb { get; set; }



        [FindsBy(How = How.ClassName, Using = "dropdown-item")]
        private IList<IWebElement> dropDownList { get; set; }

        #endregion

        [FindsBy(How = How.ClassName, Using = "number")]
        private IList<IWebElement> TasksFaults_Data { get; set; }

        [FindsBy(How = How.ClassName, Using = "circle-points")]
        private IList<IWebElement> circlePoints { get; set; }
        
        [FindsBy(How = How.TagName, Using = "circle")]
        private IList<IWebElement> circlePoints2 { get; set; }


                        
        [FindsBy(How = How.ClassName, Using = "svg-tooltip")]
        private IList<IWebElement> circlePointsData { get; set; }


        public void runLangs(Reporter reporter)
        {
            Help help = new Help();

            for (int i = 0; i < 26; i++)
            {
                settingBtn.Click();
                LangDropDown.Click();
                dropDownList[i].Click();
                apply.Click();
                Thread.Sleep(1000);
                reporter.logger("ss", driver);
                help.MainloadingWait2(driver);
            }


            


        }

        public IList<string> getTasksFaults_Data()
        {
            IList<string> data = new List<string>();
            foreach (var item in TasksFaults_Data)
            {
                if(item.Displayed && !item.Text.Equals(null))
                {
                    data.Add(item.Text);    
                }
            }


            return data;    
        }
        
        
        
        public IList<string> getMilkingSession10DaysData()
        {
            milkingSessionDrillDown.Click();
            Thread.Sleep(2000);
            IList<IWebElement> focuse = driver.FindElements(By.ClassName("focus"));
            IList<IWebElement> shownCircles = focuse[1].FindElements(By.TagName("circle"));

            IList<string> data = new List<string>();
            IList<string> data2 = new List<string>();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            foreach (var item in shownCircles)
            {
                Thread.Sleep(1000);

                Actions action = new Actions(driver);

                action.MoveToElement(item).Perform();
                Thread.Sleep(1000);
                action.MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                string val = jse.ExecuteScript("return   document.querySelector('body > div:nth-child(13)').innerText").ToString();
                data.Add(val);

            }
            foreach (var value in data)
            {

                if (value.Length > 2)
                {
                    string[] x = value.Split(new char[] { '\r', '\n' });
                    data2.Add(x[2]);
                }
            }
            milkingSessionDrillDown.Click();
            return data2;    
        }
        public IList<string> get24Hr10DaysData()
        {
            milk24HrDrillDown.Click();
            Thread.Sleep(2000);
            IList<IWebElement> focuse = driver.FindElements(By.ClassName("focus"));
            IList<IWebElement> shownCircles = focuse[2].FindElements(By.TagName("circle"));

            IList<string> data = new List<string>();
            IList<string> data2 = new List<string>();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            foreach (var item in shownCircles)
            {
                Thread.Sleep(1000);

                Actions action = new Actions(driver);

                action.MoveToElement(item).Perform();
                Thread.Sleep(1000);
                action.MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                string val = jse.ExecuteScript("return   document.querySelector('body > div:nth-child(14)').innerText").ToString();
                data.Add(val);

            }
            foreach (var value in data)
            {

                if (value.Length > 2)
                {
                    string[] x = value.Split(new char[] { '\r', '\n' });
                    data2.Add(x[2]);
                }
            }
            milk24HrDrillDown.Click();
            return data2;    
        }    
        public List<string> get24Hr10DaysData2()
        {
            milk24HrDrillDown.Click();
            Thread.Sleep(2000);
            IList<IWebElement> focuse = driver.FindElements(By.ClassName("focus"));
            IList<IWebElement> shownCircles = focuse[2].FindElements(By.TagName("circle"));

            List<string> data = new List<string>();
            List<string> data2 = new List<string>();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            foreach (var item in shownCircles)
            {
                Thread.Sleep(1000);

                Actions action = new Actions(driver);

                action.MoveToElement(item).Perform();
                Thread.Sleep(1000);
                action.MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                IList<IWebElement> data1 = driver.FindElements(By.ClassName("svg-tooltip"));
                string val = data1[data1.Count - 1].Text;
                data.Add(val);

            }
            foreach (var value in data)
            {

                if (value.Length > 2)
                {
                    string[] x = value.Split(new char[] { '\r', '\n' });
                    data2.Add(x[2]);
                }
            }
            milk24HrDrillDown.Click();
            return data2;    
        }
        public IList<string> getMilkProduction10DaysData()
        {
            milkproductionDrillDown.Click();
            Thread.Sleep(2000);
            IList<IWebElement> focuse = driver.FindElements(By.ClassName("focus"));
            IList<IWebElement> shownCircles = focuse[0].FindElements(By.TagName("circle"));

            IList<string> data = new List<string>();
            IList<string> data2 = new List<string>();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            foreach (var item in shownCircles)
            {
                Thread.Sleep(1000);

                Actions action = new Actions(driver);

                action.MoveToElement(item).Perform();
                Thread.Sleep(1000);
                action.MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                string val = jse.ExecuteScript("return   document.querySelector('body > div:nth-child(12)').innerText").ToString();
                data.Add(val);

            }
            foreach (var value in data)
            {

                if (value.Length > 2)
                {
                    string[] x = value.Split(new char[] { '\r', '\n' });
                    data2.Add(x[2].Split(' ')[0]);
                }
            }
            milkproductionDrillDown.Click();
            return data2;    
        }


        public IList<string> getHealthIssuesData()
        {
            IList<string> data = new List<string>();
            driver.FindElement(By.CssSelector("#todayTab > li:nth-child(2) > app-task-item > a > div > span > div > svg")).Click();
            for (int i = 1; i < 6; i++)
            {
                data.Add(driver.FindElement(By.CssSelector("#clps6 > div > app-task-sub-item:nth-child("+i+") > div > div:nth-child(2) > h6")).Text);
            }

            
            return data;

        }



        public void chngeUnitsToKg()
        {
            Help help = new Help();
            settingBtn.Click();
            UnitsDropDown.Click();
            Kg.Click();
            apply.Click();
            help.MainloadingWait2(driver);
        }
        public void chngeUnitsToLb()
        {
            Help help = new Help();
            settingBtn.Click();
            UnitsDropDown.Click();
            Lb.Click();
            apply.Click();
            help.MainloadingWait2(driver);
        }
        public void getColor()
        {


        }
        public bool DeltaMilkProduction()
        {
            string red = "rgba(239, 68, 56, 1)";
            string blue = "rgba(1, 149, 205, 1)";
            //TBD add green lable
            var last = lastMilkProduction.GetCssValue("color");

            int lastmilk = Int32.Parse(lastMilkProduction.Text.Split(' ')[0]);
            int AbgMilk = Int32.Parse(avgMilkProduction.Text.Split(' ')[0]);
            double delta = lastmilk/AbgMilk;

            if(delta <= 0.98)
            {
                return last.Equals(red);
            }
            if(delta >= 1.02)
            {
                //TBD add green lable
            }
            return last.Equals(blue);

        }      
        
        public bool DeltaMilkSession()
        {
            string red = "rgba(239, 68, 56, 1)";
            string blue = "rgba(1, 149, 205, 1)";
            //TBD add green lable
            var last = lastMilkSession.GetCssValue("color");

            int lastmilk = Int32.Parse(lastMilkSession.Text.Split(' ')[0]);
            int AbgMilk = Int32.Parse(avgMilkSession.Text.Split(' ')[0]);
            double delta = lastmilk/AbgMilk;

            if(delta <= 0.98)
            {
                return last.Equals(red);
            }
            if(delta >= 1.02)
            {
                //TBD add green lable
            }
            return last.Equals(blue);
        }
        
        public bool Delta24Hr()
        {
            string red = "rgba(239, 68, 56, 1)";
            string blue = "rgba(1, 149, 205, 1)";
            //TBD add green lable
            var last = last24Milk.GetCssValue("color");

            int lastmilk = Int32.Parse(last24Milk.Text.Split(' ')[0]);
            int AbgMilk = Int32.Parse(avg24Milk.Text.Split(' ')[0]);
            double delta = lastmilk/AbgMilk;

            if(delta <= 0.98)
            {
                return last.Equals(red);
            }
            if(delta >= 1.02)
            {
                //TBD add green lable
            }
            return last.Equals(blue);
        }
        
        public void test()
        {
            milkingSessionDrillDown.Click();
            Thread.Sleep(2000);
            IList<IWebElement> focuse = driver.FindElements(By.ClassName("focus"));
            IList<IWebElement> shownCircles = focuse[1].FindElements(By.TagName("circle"));

            IList<string> data = new List<string>();
            IList<string> data2 = new List<string>();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");

            foreach (var item in shownCircles)
            {
                Thread.Sleep(1000);

                Actions action = new Actions(driver);

                action.MoveToElement(item).Perform();
                Thread.Sleep(1000);
                action.MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                string val = jse.ExecuteScript("return   document.querySelector('body > div:nth-child(13)').innerText").ToString();
                data.Add(val);

            }
            foreach (var value in data)
            {

                if (value.Length > 2)
                {
                    string[] x = value.Split(new char[] { '\r', '\n' });
                    data2.Add(x[2]);
                }
            }
            milkingSessionDrillDown.Click();
        }

        public bool compareKgAndLb(List<float> Kg, List<float> Lb)
        {
            for (int i = 0; i < Kg.Count; i++)
            {
                float x = Lb[i] / Kg[i];
                x = (float)System.Math.Round(x, 3);
                if(x < 2.203 || x > 2.206)
                {
                    return false;
                }
            }
            return true;
        }

    }
}