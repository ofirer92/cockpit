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
    class mainPage_elements : BaseClass
    {
        #region must have
        private IWebDriver driver;

        public mainPage_elements(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        private string pdfUrl = "https://afimilkcockpitqa.z6.web.core.windows.net/assets/images/user_guide.pdf";

        #region elements
        [FindsBy(How = How.CssSelector, Using = "body > app-root > app-home > app-tool-bar > nav > a > svg > use")]
        public IWebElement afimilkWebsiteBtn { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(2) > span")]
        public IWebElement infoBtn { get; set; }
     
        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(3) > span")]
        public IWebElement TimeframeBtn { get; set; }
                        
        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(5) > span")]
        public IWebElement KpiSettingsBtn { get; set; }
                   
        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(6) > span")]
        public IWebElement userSettingsBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#navbarSupportedContent > ul > li:nth-child(7) > span")]
        public IWebElement logOutBtn { get; set; }
        
        [FindsBy(How = How.ClassName, Using = "close")]
        public IList<IWebElement> Close { get; set; }
              
        [FindsBy(How = How.ClassName, Using = "version-label")]
        public IWebElement versionByInfo { get; set; }

        [FindsBy(How = How.ClassName, Using = "btn-file")]
        public IWebElement PdfBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-body > div:nth-child(1) > div.radio-container > h4")]
        public IWebElement ByMonth { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-body > div:nth-child(1) > div.radio-container > h4")]
        public IWebElement ByQuarter { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-body > div:nth-child(1) > div.radio-container > h4")]
        public IWebElement ByPeriod { get; set; }
                
        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-body > div:nth-child(1) > div.radio-container > h4")]
        public IWebElement BySeasons { get; set; }
                        
        [FindsBy(How = How.ClassName, Using = "radio-out")]
        public IList<IWebElement> radios { get; set; }
              
        [FindsBy(How = How.CssSelector, Using = "#datesSettingsModal > div > div.modal-footer > button")]
        public IWebElement timeFrameApply { get; set; }
                      
        [FindsBy(How = How.CssSelector, Using = "#datesLoosDataModal > div > div.modal-footer > button:nth-child(2)")]
        public IWebElement timeFrameCancel { get; set; }
                              
        [FindsBy(How = How.CssSelector, Using = "#datesLoosDataModal > div > div.modal-footer > button:nth-child(1)")]
        public IWebElement applyUpdate { get; set; }
        #endregion


        public void openInfo()
        {
            infoBtn.Click();
        }      
        public void openTimeFrame()
        {
            Thread.Sleep(1000);
            TimeframeBtn.Click();
        }
        public void openKpiSettings()
        {
            KpiSettingsBtn.Click();
        }
        public void openUserSettings()
        {
            userSettingsBtn.Click();

        }
        public void logOut()
        {
            logOutBtn.Click();
        }
        public bool PDF_Url_Adress()
        {
            PdfBtn.Click();

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            string currentURL = driver.Url;

            return pdfUrl.Equals(currentURL);
        }
        public bool checkVersion()
        {

            return versionByInfo.Text.ToLower().Equals(version);
        }
        public void selectTimeFrameByInt(int x,string saveOrNot)
        {
            switch (saveOrNot)
            {
                case "dontSave":
                    radios[x].Click();
                    closeWindow();
                    timeFrameCancel.Click();
                    LoadingWait(1);
                    break;
                case "cancelAndSave":
                    radios[x].Click();
                    closeWindow();
                    applyUpdate.Click();
                    LoadingWait(1);
                    break;
                case "save":
                    radios[x].Click();
                    timeFrameApply.Click();
                    LoadingWait(10);
                    break;
                default:
                    break;
            }

        }

    }
}
