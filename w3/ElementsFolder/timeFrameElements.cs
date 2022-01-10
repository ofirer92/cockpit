using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Threading;
using WebApps.BaseFolder;

namespace WebApps.ElementsFolder
{
    class timeFrameElements: BaseClass
    {

        #region must have
        private IWebDriver driver;

        public timeFrameElements(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(3) > span")]
        public IWebElement TimeframeBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "radio-out")]
        public IList<IWebElement> radios { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-footer > button")]
        public IWebElement timeFrameApply { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#by-months-fromMonth")]
        public IWebElement month_1 { get; set; }
                
        [FindsBy(How = How.CssSelector, Using = "#by-months-fromYear")]
        public IWebElement year_1 { get; set; }
                        
        [FindsBy(How = How.ClassName, Using = "filter-dates")]
        public IWebElement currentDate { get; set; }
                                
        [FindsBy(How = How.ClassName, Using = "btn-dirL")]
        public IWebElement prevBtn { get; set; }
                                        
        [FindsBy(How = How.ClassName, Using = "btn-dirR")]
        public IWebElement nextBtn { get; set; }
                                                
        [FindsBy(How = How.Id, Using = "by-quarter")]
        public IWebElement byQ { get; set; }
                                                 
        //need new css\ID!
        [FindsBy(How = How.CssSelector, Using = "#by-months-fromYear")]
        public IWebElement byQYear { get; set; }

        [FindsBy(How = How.Id, Using = "by-months-period")]
        public IWebElement byMonthFrom{ get; set; }
        
        [FindsBy(How = How.Id, Using = "by-months-toMonth")]
        public IWebElement byMonthTo{ get; set; }
     
        [FindsBy(How = How.Id, Using = "by-months-toYear")]
        public IWebElement byMonthToYear{ get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-body > p")]
        public IWebElement errorMessage{ get; set; }
                
        [FindsBy(How = How.CssSelector, Using = "#datesLoosDataModal > div > div.modal-footer > button:nth-child(1)")]
        public IWebElement applyAfterCancel{ get; set; }
               
        [FindsBy(How = How.CssSelector, Using = "#datesLoosDataModal > div > div.modal-footer > button:nth-child(2)")]
        public IWebElement cancel{ get; set; }



        List<IWebElement> shownDropdown;


        public void openTimeFrame()
        {
            element_clickable(TimeframeBtn);
            TimeframeBtn.Click();
            element_clickable(month_1);
        }

        public void setTimeFrameType(int x)
        {

            element_clickable(radios[x]);
            radios[x].Click();
            timeFrameApply.Click();
            LoadingWait(10);
        }

        public void apply()
        {
            timeFrameApply.Click();
            LoadingWait(10);
        }

        public void SetByMonthJan20()
        {
            openTimeFrame();
            setTimeFrameType(0);
            openTimeFrame();
            month_1.Click();
            getDropDownList();
            shownDropdown[0].Click();
            year_1.Click();
            getDropDownList();
            shownDropdown[4].Click();
            apply();
        }
        public void setByQ()
        {
            openTimeFrame();
            setTimeFrameType(1);

        }

        private void setByPeriod()
        {
            openTimeFrame();
            setTimeFrameType(2);
        }

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

        public void nextMonth()
        {
            openTimeFrame();
            element_clickable(nextBtn);
            nextBtn.Click();
            apply();
        }    
        public void prevMonth()
        {
            openTimeFrame();
            element_clickable(prevBtn);
            prevBtn.Click();
            apply();
        }
   
        public void setYear(int year)
        {
            element_clickable(month_1);
            year_1.Click();
            year_1.Click();
            getDropDownList();
            shownDropdown[year].Click();
            apply();


        }

        public void setQ(int Q)
        {
            openTimeFrame();
            byQ.Click();

            try
            {
                getDropDownList();
                shownDropdown[Q].Click();
            }
            catch (System.Exception)
            {
                byQ.Click();
                getDropDownList();
                shownDropdown[Q].Click();
            }

            IList<IWebElement> year = driver.FindElements(By.CssSelector("#by-months-fromYear"));
            year[1].Click();
            getDropDownList();
            try
            {
                shownDropdown[0].Click();
            }
            catch (System.Exception)
            {
                year[1].Click();
                getDropDownList();
                shownDropdown[0].Click();
            }
            
            apply();

        }

        public void setBySingleMonth()
        {
            setByPeriod();
            openTimeFrame();

            tryToclick(byMonthFrom, 0);

            IList<IWebElement> year = driver.FindElements(By.CssSelector("#by-months-fromYear"));
            tryToclick(year[2], 1);

            tryToclick(byMonthTo,0);

            tryToclick(byMonthToYear,1);

            apply();
        }
        private void tryToclick( IWebElement tryToOpenWith, int tryToClickOn)
        {
            tryToOpenWith.Click();
            getDropDownList();
            try
            {
                shownDropdown[tryToClickOn].Click();
            }
            catch (System.Exception)
            {
                tryToOpenWith.Click();
                getDropDownList();
                shownDropdown[tryToClickOn].Click();
            }
        }

        public void setValuesToPeriod(int fromMonth, int fromYear, int TomMonth, int ToYear)
        {
            setByPeriod();
            openTimeFrame();

            tryToclick(byMonthFrom, fromMonth);

            IList<IWebElement> year = driver.FindElements(By.CssSelector("#by-months-fromYear"));
            tryToclick(year[2], fromYear);

            tryToclick(byMonthTo, TomMonth);

            tryToclick(byMonthToYear, ToYear);


        }

        public void cancelButSave()
        {
            closeWindow();
            applyAfterCancel.Click();
            LoadingWait(10);
        }


        //to kpi run
        public void setValuesToPeriod2(int fromMonth, int fromYear, int TomMonth, int ToYear)
        {
            
            openTimeFrame();

            tryToclick(byMonthFrom, fromMonth);

            IList<IWebElement> year = driver.FindElements(By.CssSelector("#by-months-fromYear"));
            tryToclick(year[2], fromYear);

            tryToclick(byMonthTo, TomMonth);

            tryToclick(byMonthToYear, ToYear);
            apply();

        }
    }
}
