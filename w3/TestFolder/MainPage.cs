using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace WebApps.TestFolder
{
    class MainPage:BaseClass
    {
        mainPage_elements main;

        [SetUp]
        public void setup()
        {
            test.AssignCategory("front page");
            login();
        }

        [Test]
        [Description("ToolBar links")]
        public void test1()
        {
            main = new mainPage_elements(driver);

            main.openInfo();
            logger("info");
            closeWindow();
            //
            main.openKpiSettings();
            logger("KPI settings");
            closeWindow();
            //
            main.openUserSettings();
            logger("User Settings");
            closeWindow();
            //
            main.openTimeFrame();
            logger("Time Frame");
            closeWindow();
            //      
            main.logOut();
            logger("logged out");
            closeWindow();
            //



        }

        [Test]
        [Description("User Info: user guide PDF")]
        public void test2()
        {
            main = new mainPage_elements(driver);
            main.openInfo();
            Assert.IsTrue(main.checkVersion());
            logger("Same version as login page");
            Assert.IsTrue(main.PDF_Url_Adress());
        }
        
        [Test]
        [Description("timeframe: save changes")]
        public void test3()
        {
            main = new mainPage_elements(driver);
            for (int i = 0; i <= 3; i++)
            {
                main.openTimeFrame();
                logger();
                main.selectTimeFrameByInt(i,"save");
            }

        }
        [Test]
        [Description("timeframe: cancel changes (select and cancel)")]
        public void test4()
        {
            main = new mainPage_elements(driver);
            for (int i = 0; i <= 3; i++)
            {
                main.openTimeFrame();
                logger();
                main.selectTimeFrameByInt(i, "dontSave");
            }
        }

        [Test]
        [Description("timeframe: apply changes via close window")]
        public void test5()
        {
            main = new mainPage_elements(driver);
            for (int i = 0; i <= 3; i++)
            {
                main.openTimeFrame();
                logger();
                main.selectTimeFrameByInt(i, "cancelAndSave");
            }
        }
    }
}
