using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApps.BaseFolder;
using WebApps.ElementsFolder;

namespace WebApps.TestFolder
{

    class timeFrame: BaseClass
    {
        timeFrameElements timeframe;
        static int currentYear = 2022;
        public int minYear = currentYear-5;
        
        [SetUp]
        public void setup()
        {
            test.AssignCategory("time frame settings");
            login();
            timeframe = new timeFrameElements(driver);
        }


        [Test]
        [Description("by month: set by single month (January 2022)")]
        public void test1() 
        {
            timeframe.openTimeFrame();
            timeframe.setTimeFrameType(0);
            timeframe.SetByMonthJan20();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {currentYear-1} - January {currentYear-1}"));
        }

        [Test]
        [Description("by month: skip months next and prev")]
        public void test2()
        {
            timeframe.SetByMonthJan20();
            logger("current month: "+ timeframe.currentDate.Text);

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"February {currentYear - 1} - February {currentYear - 1}"));
            logger("current month: " + timeframe.currentDate.Text);
            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"March {currentYear-1} - March {currentYear-1}"));
            logger("current month: " + timeframe.currentDate.Text);

            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"February {currentYear - 1} - February {currentYear - 1}"));
            logger("current month: " + timeframe.currentDate.Text);
        }
        
        [Test]
        [Description("by month: change years")]
        public void test3()
        {
            
            timeframe.SetByMonthJan20();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {currentYear - 1} - January {currentYear - 1}"));

            for (int i = 0; i <= 5; i++)
            {
                timeframe.openTimeFrame();
                string year = minYear.ToString();
                timeframe.setYear(i);
                string testText = "January +" + year + " - January " + year;
                Assert.IsTrue(timeframe.currentDate.Text.Equals("January "+ year + " - January " + year));
                minYear++;
            }
            
            


        }

        [Test]
        [Description("By Quarter: set by Quarter")]
        public void test4()
        {
            timeframe.setByQ();

            timeframe.setQ(0);
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {minYear} - March {minYear}"));

            timeframe.setQ(1);
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"April {minYear} - June {minYear}"));

            timeframe.setQ(2);
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"July {minYear} - September {minYear}"));

            timeframe.setQ(3);
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"October {minYear} - December {minYear}"));

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {minYear+1} - March {minYear+1}"));

            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"October {minYear} - December {minYear}"));
        }

        [Test]
        [Description("View By Period: set single month (January 2017)")]
        public void test5()
        {
            timeframe.setBySingleMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {minYear+1} - January {minYear+1}"));

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"February {minYear+1} - February {minYear+1}"));
            
            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"March {minYear+1} - March {minYear+1}"));

            timeframe.setBySingleMonth();
            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"December {minYear} - December {minYear}"));
        }

        [Test]
        [Description("View By Period: invalid dates")]
        public void test6()
        {
            //year is wrong
            timeframe.setValuesToPeriod(0, 2, 0, 1);
            Assert.IsTrue(timeframe.errorMessage.Text.Equals("Your dates are not valid"));
            Assert.IsFalse(timeframe.timeFrameApply.Enabled);
            closeWindow();
            Assert.IsFalse(timeframe.applyAfterCancel.Enabled);
            timeframe.cancel.Click();
            LoadingWait(5);

            //months is wrong
            timeframe.setValuesToPeriod(4, 1, 2, 1);
            Assert.IsTrue(timeframe.errorMessage.Text.Equals("Your dates are not valid"));
            Assert.IsFalse(timeframe.timeFrameApply.Enabled);
            closeWindow();
            Assert.IsFalse(timeframe.applyAfterCancel.Enabled);
            timeframe.cancel.Click();
            LoadingWait(5);


            //both year and months is wrong
            timeframe.setValuesToPeriod(4, 4, 2, 1);
            Assert.IsTrue(timeframe.errorMessage.Text.Equals("Your dates are not valid"));
            Assert.IsFalse(timeframe.timeFrameApply.Enabled);
            closeWindow();
            Assert.IsFalse(timeframe.applyAfterCancel.Enabled);
        }

        [Test]
        [Description("View By Period: apply via cencel")]
        public void test7()
        {
            timeframe.setValuesToPeriod(0, 1, 3, 2);
            timeframe.cancelButSave();
            Assert.IsTrue(timeframe.currentDate.Text.Equals($"January {minYear+1} - April {minYear+2}"));
        }

        //TBD add seasons tests!!!!
    }
}
