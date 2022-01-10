using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Today.Base;
using Today.Elements;
using Today.Helpers;

namespace Today.Tests
{
    internal class MainPageTests : BaseClass
    {
        private Help help;
        MainPage main;
        LoginPage loginPage;
        [SetUp]
        public void test0()
        {
            help= new Help();
            main = new MainPage(driver);
            loginPage = new LoginPage(driver);
            loginPage.setLoginDetails(userName, Password);
            loginPage.acceptTerms();
            loginPage.clickLogin();     
            help.MainloadingWait(driver); 
        }
        [Test]  
        [Description("run Langs")]
        public void test1()
        {
            
            main.runLangs(reporter);

        }
        [Test]
        [Description("Get data")]
        public void test2()
        {
            main.chngeUnitsToKg();
            Thread.Sleep(1000);

            IList<string> KG24HR = main.get24Hr10DaysData2();
            IList<string> KgMilkSessions = main.getMilkingSession10DaysData();
            IList<string> KgMilkProduction = main.getMilkProduction10DaysData();

            main.chngeUnitsToLb();
            Thread.Sleep(1000);

            IList<string> Lb24HR = main.get24Hr10DaysData();
            IList<string> LbMilkSessions = main.getMilkingSession10DaysData();
            IList<string> LbMilkProduction = main.getMilkProduction10DaysData();
            

            main.getTasksFaults_Data();
            main.getHealthIssuesData();

        }

        [Test]
        [Description("colors by delta")]
        public void test3()
        {
            Thread.Sleep(1000); 
            Assert.IsTrue(main.DeltaMilkProduction() && main.DeltaMilkSession() /*TBD add 24Hr*/);   

        }


        [Test]
        [Description("Milk Production - Day")]
        public void test4()
        {
            main.chngeUnitsToKg();
            string x = main.lastMilkProduction.Text;

            List<float> MilkProduction_Day_InnerData_Kg = main.getMilkProduction10DaysData().Select(s => float.Parse(s)).ToList();
            double average_Kg = Math.Round(MilkProduction_Day_InnerData_Kg.Average());
            help.refresh(driver);
            help.MainloadingWait2(driver);
            Thread.Sleep(5000);

            main.chngeUnitsToLb();
            List<float> MilkProduction_Day_InnerData_Lb = main.getMilkProduction10DaysData().Select(s => float.Parse(s)).ToList();
            double average_Lb = Math.Round(MilkProduction_Day_InnerData_Lb.Average());

            bool ans = main.compareKgAndLb(MilkProduction_Day_InnerData_Kg, MilkProduction_Day_InnerData_Lb);
            
            Assert.IsTrue(ans); 

        }
        [Test]
        [Description("Milk production - Milking session")]
        public void test5()
        {
            main.chngeUnitsToKg();
            Thread.Sleep(1000);

            IList<string> MilkProduction_MilkingSession_innerData = main.getMilkingSession10DaysData();

            main.chngeUnitsToLb();

        }
        [Test]
        [Description("Milk production - 24Hr, Avg milk/Cow")]
        public void test6()
        {
            main.chngeUnitsToKg();
            Thread.Sleep(1000);

            IList<string> MilkProduction_24Hr_InnerData = main.get24Hr10DaysData();

            main.chngeUnitsToLb();

        }


    }
}
