using cOCKPIT.ElementsFolder;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebApps.BaseFolder;

namespace WebApps.TestFolder
{
    class userSettings : BaseClass
    {
        userSettingsElements settings;


        [SetUp]
        public void setup()
        {
            test.AssignCategory("User settings");
            login();
            settings = new userSettingsElements(driver, test);
        }


        [Test]
        [Description("Lang check")]
        public void test1()
        {
            settings.runLangs();
        }
        [Test]
        [Description("KG units")]
        public void test2()
        {
            
            settings.setAsKg();
            Assert.IsTrue(settings.ouuterKpisUnits("Kg"));
            Assert.IsTrue(settings.kpiSettingsUnits("Kg"));
        }   
        [Test]
        [Description("LB units")]
        public void test3()
        {
            settings.setAsLb();
            Assert.IsTrue(settings.ouuterKpisUnits("Lb"));
            Assert.IsTrue(settings.kpiSettingsUnits("Lb"));
        }

        [Test]
        [Description("KG and LB datas")]
        public void test4()
        {
            
            Assert.IsTrue(settings.kpiSettingsValues());
            Assert.IsTrue(settings.frontValues());
        }
        [Test]
        [Description("seasons 1")]
        public void test5()
        {
            //seasons need new IDS
        }
    }
}
