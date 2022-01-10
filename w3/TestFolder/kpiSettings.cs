using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace WebApps.TestFolder
{
    class kpiSettings : BaseClass
    {
        kpiSettingsElements kpis;
        public string[] defaultValues = {"170","190", "22", "70", "65", "42", "37", "119", "32", "35", "1.5", "1.8", "15", "15", "1.12", "4.5", "9", "8"
        , "12", "33", "22", "0", "2", "0.5", "0.5", "2", "5", "", "", "", "90", "89", "88", "", ""};
        public string[] alertValues = { "", "", "", "89", "88", "87", "", "", };

        [SetUp]
        public void setup()
        {
            test.AssignCategory("KPI settings");
            login();


            #region becuse of bug
            driver.Navigate().Refresh();
            LoadingWait(10);
            #endregion


            kpis = new kpiSettingsElements(driver,test);
        }

        [Test]
        [Description("kpi values: values limits ")]
        public void Test1()
        {

            kpis.openRebbons();

            //correct values
            kpis.setTargetBox(90);
            kpis.setAlertBox(80);
            Assert.IsTrue(kpis.applyBtn.Enabled);
            logger("apply is enabled");
            
            //wrong values
            kpis.setAlertBox(91);

            //assert settings cannot be saved with error
            Assert.IsFalse(kpis.applyBtn.Enabled);
            logger("apply is disabled");
            closeWindow();
            Assert.IsFalse(kpis.applyBtnAfterCancel.Enabled);
        }

        [Test]
        [Description("kpi values: save data")]
        public void test2()
        {
            kpis.openRebbons();
            kpis.setTargetBox(90);
            kpis.setAlertBox(80);
            kpis.apply();

            kpis.openRebbons();
            Assert.IsTrue(kpis.valid_Data_targetBox(90));
            logger("correct values at target box");
                     
            Assert.IsTrue(kpis.valid_Data_Alertbox(80));
            logger("correct values at alert box");
        }


        [Test]
        [Description("kpi: enable and disable kpis via settings")]
        public void test3()
        {


            //assert disabling all kpis via kpi settings
            kpis.openRebbons();
            kpis.showAllKpis();
            Thread.Sleep(4000);
            int shownKPIs = kpis.shownKPIS();
            Assert.IsTrue(shownKPIs.Equals(22));

            //assert enabling all kpis via kpi settings
            kpis.openRebbons();
            kpis.hideAllKpis();
            Thread.Sleep(4000);
            int shownKPIs2 = kpis.shownKPIS();
            Assert.IsTrue(shownKPIs2.Equals(0));

            //restor to default
            kpis.openRebbons();
            kpis.showAllKpis();

        }


        [Test]
        [Description("disable kpis via main screen")]
        public void test4()
        {

            //set all kpi to be visable
            kpis.openRebbons();
            kpis.showAllKpis();

            kpis.infosClick();
            while (kpis.hidesClick() > 0)
            {
                kpis.hidesClick();
            };
            logger("zero KPI left to see");
            
            kpis.openRebbons();
            Assert.IsTrue(kpis.shownElements(kpis.visableKPIs).Equals(0));

            //show all kpi to next tests
            kpis.openRebbons();
            kpis.showAllKpis();
        }

        [Test]
        [Description("assert all info titles arte same as kpi titles")]
        public void test5()
        {
            loggerNoScreenshotList(kpis.sameTitles());
            Assert.IsTrue(kpis.sameTitles().Count.Equals(0));
        }

        [Test]
        [Description("max and min values")]
        public void test6()
        {

            // TBD!
        }

        [Test]
        [Description("default values")]
        public void test7()
        {
            kpis.openRebbons();
            kpis.restoreToDefaults();
            bool alertboxAns = kpis.checkDefaultValues2(alertValues);
            bool TargetBoxAns = kpis.checkDefaultValues(defaultValues);
            Assert.IsTrue(alertboxAns);
            Assert.IsTrue(TargetBoxAns);
        }



        [Test]
        [Description("hide and show all sections")]
        public void test9()
        {
            
            kpis.openRebbons();
            Assert.IsTrue(kpis.hideAndCheck());
            
            
        }
    }
}
