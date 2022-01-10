using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace cockpit_new
{
    internal class kpi
    {
        public string value;
        public string name;
        public string description;//TBD
        public string score; //TBD

        public string good_threshold;
        public string bad_threshold;


        private IWebDriver driver;

        public kpi(IWebDriver driver,int kpi_Number)
        {
            this.driver = driver;
            this.name = driver.FindElements(By.ClassName("kpi-title"))[kpi_Number].Text;
            value = driver.FindElements(By.Id("score-display"))[kpi_Number].Text.Split('\r')[0];
            this.bad_threshold = driver.FindElements(By.ClassName("bad-t"))[kpi_Number+1].FindElements(By.XPath("./*"))[0].Text;
            this.good_threshold = driver.FindElements(By.ClassName("good-t"))[kpi_Number+1].FindElements(By.XPath("./*"))[0].Text;
            
        }




    }
}
