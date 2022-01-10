using cOCKPIT.ElementsFolder;
using cockpit_new;
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
    class KPIs_t : BaseClass
    {
        cockpitAdmin setData;
        timeFrameElements timeFrame;
        kpiSettingsElements kpis ;
        private List<kpiSettingsElements> kpisList;
        kpiDataElements data;
        bool flag = false;
        public List<string> goodT_list = new List<string>();
        public void setvalues(int heifers, int Lact1, int Lact2, int Lact3)
        {
            
            setData.setdata();
            setData.setInventory(heifers, Lact1, Lact2, Lact3);
        }

 

        //[SetUp]
        public void tt()
        {
            if (!flag)
            {
                setData = new cockpitAdmin(driver);
                setData.setdata();
                setData.setInventory2(60, 20, 20, 20);

            }
            flag = true;
        }


        [SetUp]
        public void setup()
        {
            test.AssignCategory("KPI thresholds and new Req");

            login();

            kpis = new kpiSettingsElements(driver);
            kpis.openRebbons();
            //kpis.restoreToDefaults();
            kpis.showAllKpis();

        }


        [Test]
        [Description("Threasholds data same as kpi settings")]
        public void t1()
        {
            int kpi_To_Check = 1;
            int good_t = 10;
            for (int i = 1; i < 11; i++)
            {
                kpis.openRebbons();
                
                kpis.setTargetBox(i*10);
                kpis.setAlertBox(1);
                
                kpis.apply();
                foreach (var item in getAll_kpis())
                {
                   Assert.AreEqual(item.good_threshold.Split('.')[0],(i*10).ToString());

                }

            }

        }
        private List<kpi> getAll_kpis()
        {
            List<kpi> kpislist =new List<kpi>();
            for (int i = 1; i < 22; i++)
            {
                kpi c = new kpi(driver, i);
                kpislist.Add(c);
            }
            return kpislist;
        }


        [Test]
        [Description("hide sections by disable kpis - new req")]
        public void t2()
        {
            kpis = new kpiSettingsElements(driver);
            Assert.IsTrue(kpis.titles.Count.Equals(3));
            logger();
            kpis.hideSection(0);
            logger();
            Assert.IsTrue(kpis.titles.Count.Equals(2));
            kpis.hideSection(1);
            logger();
            Assert.IsTrue(kpis.titles.Count.Equals(1));
            kpis.hideSection(2);
            Assert.IsTrue(kpis.titles.Count.Equals(0));




        }

    }
}
