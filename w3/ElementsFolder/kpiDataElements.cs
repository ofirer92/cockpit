using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace cOCKPIT.ElementsFolder
{
    class kpiDataElements : BaseClass
    {
        IWebDriver driver;

        timeFrameElements timeFrame;

        public kpiDataElements(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "#circulargauge_control_7_Axis_0_Annotation_0 > div > div:nth-child(1)")]
        public IWebElement AvgDimScore { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#circulargauge_control_7_Axis_0_Annotation_0 > div > div:nth-child(2)")]
        public IWebElement AvgDimText { get; set; }
        
        [FindsBy(How = How.ClassName, Using = "bad-t")]
        public IList<IWebElement> bad_t { get; set; }

                
        [FindsBy(How = How.ClassName, Using = "good-t")]
        public IList<IWebElement> good_t { get; set; }




        List<string> frontData;
        public string data()
        {
            //for now not decimal! TBD!
            string scoreT = AvgDimScore.Text;
            string[] score = scoreT.Split('.');

            return score[0];
        }

        public bool calculationData(int numberOfMonths)
        {
            int expected = 5;
            timeFrame = new timeFrameElements(driver);
            for (int i = 0; i < 11; i++)
            {
                while (i + numberOfMonths > 11) numberOfMonths--;
                timeFrame.setValuesToPeriod2(0, 5, i+ numberOfMonths, 5);
                Thread.Sleep(500);
                if (!data().Equals(expected.ToString()))
                {
                    return false;
                }
                expected += 5;
            }
            for (int i = 0; i < 11; i++)
            {
                while (i + numberOfMonths > 11) numberOfMonths--;
                timeFrame.setValuesToPeriod2(i + numberOfMonths, 5, 11, 5);
                Thread.Sleep(500);
                if (!data().Equals(expected.ToString()))
                {
                    return false;
                }
                expected += 5;
            }
            return true;
        }

        public void test()
        {
            IList<IWebElement> c = bad_t[0].FindElements(By.XPath("./*"));


        }
    }
}
