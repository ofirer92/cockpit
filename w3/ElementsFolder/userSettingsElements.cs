using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace cOCKPIT.ElementsFolder
{
    class userSettingsElements :BaseClass
    {
        private IWebDriver driver;
        public userSettingsElements(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            PageFactory.InitElements(driver, this);
        }



        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(6) > span")]
        public IWebElement usersettingsBtn { get; set; }

        
        [FindsBy(How = How.CssSelector, Using = "#settingsModal > div > div.modal-footer > button")]
        public IWebElement applyBtn { get; set; }

                
        [FindsBy(How = How.CssSelector, Using = "#settingsModal > div > div.modal-body > table:nth-child(1) > tr > td:nth-child(1) > div > app-list > div > button")]
        public IWebElement langBtn { get; set; }
                        
        [FindsBy(How = How.CssSelector, Using = "#settingsModal > div > div.modal-body > table:nth-child(1) > tr > td:nth-child(2) > div > app-list > div > button")]
        public IWebElement unitBtn { get; set; }

                        
        [FindsBy(How = How.CssSelector, Using = "#kpi-18 > div > div.linear-bar > div.score-content > div.a > span")]
        public IWebElement Daily_Average { get; set; }
              
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(1) > div:nth-child(2) > div > div > span")]
        public IWebElement Daily_Average_inner1 { get; set; }
             
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(1) > div:nth-child(3) > div > div > span")]
        public IWebElement Daily_Average_inner2 { get; set; }

                        
        [FindsBy(How = How.CssSelector, Using = "#kpi-19 > div > div.linear-bar > div.score-content > div.a > span")]
        public IWebElement Average_Herd  { get; set; }
                                
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(2) > div:nth-child(2) > div > div > span")]
        public IWebElement Average_Herd_inner1 { get; set; }
                                
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(2) > div:nth-child(3) > div > div > span")]
        public IWebElement Average_Herd_inner2 { get; set; }

                        
        [FindsBy(How = How.CssSelector, Using = "#kpi-20 > div > div.linear-bar > div.score-content > div.a > span")]
        public IWebElement Rolling_Herd  { get; set; }
                                
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(3) > div:nth-child(2) > div > div > span")]
        public IWebElement Rolling_Herd_inner1 { get; set; }
                                
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(3) > div:nth-child(3) > div > div > span")]
        public IWebElement Rolling_Herd_inner2 { get; set; }



        private List<IWebElement> shownDropdown;

        private void getDropDownList()
        {
            IList<IWebElement> c = driver.FindElements(By.ClassName("dropdown-item"));
            shownDropdown = new List<IWebElement>();
            foreach (IWebElement elements in c)
            {

                if (elements.Displayed)
                {
                    shownDropdown.Add(elements);
                }

            }
        }


        private void applyChanger()
        {
            applyBtn.Click();
            LoadingWait(10);
        }
        public void openSettings()
        {
            element_clickable(usersettingsBtn);
            usersettingsBtn.Click();
            element_clickable(applyBtn);
        }

        public void runLangs()
        {
            
            openSettings();
            langBtn.Click();
            getDropDownList();
            foreach (IWebElement item in shownDropdown)
            {
                try
                {
                    item.Click();
                }
                catch
                {
                    langBtn.Click();
                    item.Click();
                }
                
                applyChanger();
                logger();
                openSettings();
                langBtn.Click();
            }
            try
            {
                shownDropdown[6].Click();
            }
            catch (Exception)
            {
                langBtn.Click();
                shownDropdown[6].Click();
            }
            applyChanger();

        }

        public void setAsKg()
        {
            openSettings();
            try
            {
                unitBtn.Click();
                getDropDownList();
                shownDropdown[0].Click();
            }
            catch (Exception)
            {
                unitBtn.Click();
                getDropDownList();
                shownDropdown[0].Click();
            }
            applyChanger();
        }      
        public void setAsLb()
        {
            openSettings();
            try
            {
                unitBtn.Click();
                getDropDownList();
                shownDropdown[1].Click();
            }
            catch (Exception)
            {
                unitBtn.Click();
                getDropDownList();
                shownDropdown[1].Click();
            }

            applyChanger();
        }

        public  bool ouuterKpisUnits(string unit)
        {
            if(Daily_Average.Text.Equals(unit) && Average_Herd.Text.Equals(unit) && Rolling_Herd.Text.Equals(unit))
            {
                return true;
            }
            return false;
        }
        public bool kpiSettingsUnits(string unit)
        {
            kpiSettingsElements kpis = new kpiSettingsElements(driver, test);
            kpis.openRebbons();

            if (Average_Herd_inner1.Text.Equals(unit) && Average_Herd_inner2.Text.Equals(unit) && Daily_Average_inner1.Text.Equals(unit)&& Daily_Average_inner2.Text.Equals(unit)&& Rolling_Herd_inner1.Text.Equals(unit)&& Rolling_Herd_inner2.Text.Equals(unit))
            {
                return true;
            }
            return false;
        }

        public bool frontValues()
        {
            //get KG data
            setAsKg();
            List<string> dataKG = new List<string>();
            for (int i = 18; i <= 20; i++)
            {
                string c = driver.FindElement(By.CssSelector("#kpi-" + i + " > div > div.linear-bar > div.score-content > div.a")).Text;
                dataKG.Add(c.Split('\r')[0]);
            }

            //get LB data
            setAsLb();
            List<string> dataLB = new List<string>();
            for (int i = 18; i <= 20; i++)
            {
                string c = driver.FindElement(By.CssSelector("#kpi-" + i + " > div > div.linear-bar > div.score-content > div.a")).Text;
                dataLB.Add(c.Split('\r')[0]);
            }

            //check calculation
            for (int i = 0; i < dataKG.Count; i++)
            {
               double kg = Double.Parse(dataKG[i]);
               double lb = Double.Parse(dataLB[i]);
               if(!Math.Round(lb * 0.45359237 ).Equals(Math.Round(kg)))
                {
                    return false ;
                }
            }
            return true;
        }
        public bool kpiSettingsValues()
        {

            var node = test.CreateNode("Node");
            setAsKg();
            kpiSettingsElements kpis = new kpiSettingsElements(driver, test);
            kpis.openRebbons();
            kpis.setAlertBox(80);
            kpis.setTargetBox(90);
            kpis.apply();

            kpis.openRebbons();
            List<string> dataKG = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
                for (int z = 2; z <= 3; z++)
                {
                    string c = driver.FindElement(By.CssSelector("#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child("+i+") > div:nth-child("+z+") > div > div > input")).GetAttribute("value");
                    dataKG.Add(c);
                }
            }

            kpis.closeWindow(); ;

            setAsLb();
            kpis.openRebbons();
            List<string> dataLB = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
                for (int z = 2; z <= 3; z++)
                {
                    string c = driver.FindElement(By.CssSelector("#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(" + i + ") > div:nth-child(" + z + ") > div > div > input")).GetAttribute("value");
                    dataLB.Add(c);
                }
            }
           
            
            //check calculation
            for (int i = 0; i < dataKG.Count; i++)
            {
                double kg = Double.Parse(dataKG[i]);
                double lb = Double.Parse(dataLB[i]);
                if (!Math.Round(lb * 0.45359237).Equals(Math.Round(kg)))
                {
                    return false;
                }
            }
            node.Pass("Pass");
            return true;
            
        }
    }
}
