using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;

namespace WebApps.ElementsFolder
{
    class kpiSettingsElements : BaseClass
    {
        #region must have
        private IWebDriver driver;

        

        public kpiSettingsElements(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            PageFactory.InitElements(driver, this);
        }       
        public kpiSettingsElements(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        private string redBorder = "1px solid rgb(255, 0, 0)";

        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(5) > span")]
        public IWebElement kpiSettingsBtn { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-footer > button")]
        public IWebElement applyBtn { get; set; }

                
        [FindsBy(How = How.CssSelector, Using = "#personalizeLoosDataModal > div > div.modal-footer > button:nth-child(1)")]
        public IWebElement applyBtnAfterCancel { get; set; }

        
        [FindsBy(How = How.ClassName, Using = "option")]
        public IList<IWebElement> rebbons { get; set; }
                
        [FindsBy(How = How.ClassName, Using = "targetbox")]
        public IList<IWebElement> targetBox { get; set; }
                        
        [FindsBy(How = How.ClassName, Using = "alertbox")]
        public IList<IWebElement> alertBox { get; set; }
                                        
        [FindsBy(How = How.ClassName, Using = "kpi-hidden")]
        public IList<IWebElement> hiddenKPIS { get; set; }
                                                                         
        [FindsBy(How = How.ClassName, Using = "kpi-visible")]
        public IList<IWebElement> visableKPIs { get; set; }
                                                            
        [FindsBy(How = How.ClassName, Using = "kpi-content")]
        public IList<IWebElement> showKpisAtFront { get; set; }
                                                                          
        [FindsBy(How = How.ClassName, Using = "img-info")]
        public IList<IWebElement> infoBtns { get; set; }
                                                                                                          
        [FindsBy(How = How.ClassName, Using = "col-12")]
        public IList<IWebElement> hideBtns { get; set; }      
        
        
        [FindsBy(How = How.ClassName, Using = "kpi-title")]
        public IList<IWebElement> kpiTitles { get; set; }
        
        [FindsBy(How = How.ClassName, Using = "titleOne")]
        public IList<IWebElement> InforKpiTitles { get; set; }
               
        [FindsBy(How = How.ClassName, Using = "btn-reset")]
        public IList<IWebElement> defaultBtn { get; set; }
        
        [FindsBy(How = How.ClassName, Using = "table-content")]
        private IList<IWebElement> section { get; set; }
                
        [FindsBy(How = How.ClassName, Using = "topic-title")]
        public IList<IWebElement> titles { get; set; }

                                
        [FindsBy(How = How.CssSelector, Using = "#personalizeModal > div > div.modal-body > div > div > div:nth-child(3) > div > div > div > div.table-content > div:nth-child(2) > div:nth-child(3) > div > div > input")]
        public IWebElement testedKPI { get; set; }
                                        
        [FindsBy(How = How.CssSelector, Using = "#personalizeLoosDataModal > div > div.modal-footer > button:nth-child(2)")]
        public IWebElement cancel { get; set; }

                                                
        [FindsBy(How = How.Id, Using = "circulargauge_control_1_Axis_Group_0")]
        public IWebElement FERTILITY { get; set; }



        public void openSettings()
        {
            Thread.Sleep(1000);
            kpiSettingsBtn.Click();
        }
        public void openRebbons()
        {
            openSettings();
            element_clickable(rebbons[0]);
            for (int i = 2; i >= 0; i--)
            {
                rebbons[i].Click();
            }
            
        }
        public void setTargetBox(int targetValue)
        {
            foreach (IWebElement target in targetBox)
            {
                target.SendKeys(Keys.Control + "a");
                target.SendKeys(Keys.Delete);
                target.SendKeys(targetValue.ToString()) ;
            }
        }
        public void setAlertBox(int alertValues)
        {
            foreach (IWebElement alert in alertBox)
            {
                try
                {
                    alert.SendKeys(Keys.Control + "a");
                    alert.SendKeys(Keys.Delete);
                    alert.SendKeys(alertValues.ToString());
                }
                catch (Exception)
                {

                }

            }
        }

        public void apply()
        {
            applyBtn.Click();
            LoadingWait(10);
        }

        public bool valid_Data_targetBox(int expected_value)
        {
            int wrongVals=0;
            foreach (IWebElement item in targetBox)
            {
                string c = item.GetAttribute("value");

                if (!item.GetAttribute("value").Equals(expected_value.ToString()))
                {
                    wrongVals++;
                }
            }
            if (wrongVals > 2) { return false; };
            return true;
        }
        public bool valid_Data_Alertbox(int expected_value)
        {
            int wrongVals = 0;
            foreach (IWebElement item in alertBox)
            {
                string c = item.GetAttribute("value");
                if (item.Displayed)
                {
                    if (!item.GetAttribute("value").Equals(expected_value.ToString()))
                    {
                        wrongVals++;
                    }
                }
            }
            if (wrongVals > 2) { return false; };
            return true;
        }

        public void showAllKpis()
        {
            foreach (IWebElement item in hiddenKPIS)
            {
                element_clickable(item);
                Thread.Sleep(1000);
                item.Click();
            }
            apply();
        }     
        public void hideAllKpis()
        {
            
            foreach (IWebElement item in visableKPIs)
            {
                element_clickable(item);
                item.Click();
            }
            apply();
        }

        public void hideSection(int sectionNumber)
        {
            openRebbons();
            Thread.Sleep(1000);
            IList<IWebElement> enabledKpis = section[sectionNumber].FindElements(By.ClassName("kpi-visible"));
            foreach (var kpi in enabledKpis)
            {
                kpi.Click();
            }
            apply();
        }

        public int shownKPIS()
        {
            return showKpisAtFront.Count;
        }
        
        public void infosClick()
        {
            Thread.Sleep(4000);
            foreach (IWebElement item in infoBtns)
            {
                item.Click();
            }
        }        
        public int hidesClick()
        {
            
            int z = 0;
            Thread.Sleep(1000);
            for (int i = 0; i <= hideBtns.Count; i++)
            {
                try
                {
                    
                    if (i % 2 != 0 && hideBtns[i].Displayed)
                    {
                        scrollToElement(hideBtns[i]);
                        hideBtns[i].Click();
                        
                    }
                }
                catch (Exception)
                {

                    
                }

            }
            foreach (IWebElement item in hideBtns)
            {
                if (item.Displayed) { z++; };
            }
            
            return z;
        }
        private void scrollToElement(IWebElement element)
        {
            
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
        public int shownElements(IList<IWebElement> Elist)
        {
            return Elist.Count;
        }

        public List<string> sameTitles()
        {
            Thread.Sleep(1500);
            IList<IWebElement> c = driver.FindElements(By.ClassName("kpi-title"));
            IList<IWebElement> c2 = driver.FindElements(By.ClassName("titleOne"));
            List<string> falses = new List<string>();
            for (int i = 0; i < c.Count; i++)
            {
                if (!c[i].Text.Equals(c2[i].Text)){
                    falses.Add(c[i].Text + "  ||  " + c2[i].Text);
                }
            }
            return falses;
        }

        public void restoreToDefaults()
        {
            foreach (IWebElement item in defaultBtn)
            {
                element_clickable(item);
                Actions actions = new Actions(driver);
                actions.MoveToElement(item);
                actions.Perform();
                item.Click();
            }
        }
        public List<string> getValues(IList<IWebElement> Elist)
        {
            List<string> data = new List<string>();
            foreach (IWebElement item in Elist)
            {
                string c = item.GetAttribute("value");
                data.Add(c);
            }
            return data;
        }

        public bool checkDefaultValues(string[] defaultValues)
        {
            for (int i = 0; i < getValues(targetBox).Count; i++)
            {
                if (!getValues(targetBox)[i].Equals(defaultValues[i]))
                {
                    logger(getValues(targetBox)[i] + "  ||  "+ defaultValues[i]);
                    return false;
                }
            }
            return true;
        }
        public bool checkDefaultValues2(string[] defaultValues)
        {
            for (int i = 0; i < getValues(alertBox).Count; i++)
            {
                if (!getValues(alertBox)[i].Equals(defaultValues[i]))
                {
                    logger(getValues(alertBox)[i] + "  ||  " + defaultValues[i]);
                    return false;
                }
            }
            return true;
        }


        public bool hideAndCheck()
        {
            showAllKpis();
            IList<IWebElement> Elist1 = driver.FindElements(By.ClassName("topic"));
            openRebbons();
            hideAllKpis();
            IList<IWebElement> Elist = driver.FindElements(By.ClassName("topic"));
            if (Elist1.Count == 3 & Elist.Count == 0)
            {
                openRebbons();
                showAllKpis();
                return true;
            }
            openRebbons();
            showAllKpis();
            return false;
        }

        public void setKpiVal(IWebDriver driver,int kpiNumber, string kpiValues)
        {
            openRebbons();
            for (int i = kpiNumber; i < 20; i++)
            {
                driver.FindElements(By.ClassName("hrline"))[i].FindElement(By.ClassName("ng-valid")).SendKeys(Keys.Control + "a");
                driver.FindElements(By.ClassName("hrline"))[i].FindElement(By.ClassName("ng-valid")).SendKeys(Keys.Delete);
                driver.FindElements(By.ClassName("hrline"))[i].FindElement(By.ClassName("ng-valid")).SendKeys(kpiValues);
            }

            apply();
        }
    }
}
