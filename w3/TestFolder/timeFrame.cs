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
        public int minYear = 2016;
        [SetUp]
        public void setup()
        {
            test.AssignCategory("time frame settings");
            login();
            timeframe = new timeFrameElements(driver);
        }


        [Test]
        [Description("by month: set by single month (January 2020)")]
        public void test1() 
        {
            timeframe.openTimeFrame();
            timeframe.setTimeFrameType(0);
            timeframe.SetByMonthJan20();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2020 - January 2020"));
        }

        [Test]
        [Description("by month: skip months next and prev")]
        public void test2()
        {
            timeframe.SetByMonthJan20();
            logger("current month: "+ timeframe.currentDate.Text);

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("February 2020 - February 2020"));
            logger("current month: " + timeframe.currentDate.Text);
            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("March 2020 - March 2020"));
            logger("current month: " + timeframe.currentDate.Text);

            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("February 2020 - February 2020"));
            logger("current month: " + timeframe.currentDate.Text);
        }
        
        [Test]
        [Description("by month: change years")]
        public void test3()
        {
            
            timeframe.SetByMonthJan20();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2020 - January 2020"));

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
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2016 - March 2016"));

            timeframe.setQ(1);
            Assert.IsTrue(timeframe.currentDate.Text.Equals("April 2016 - June 2016"));

            timeframe.setQ(2);
            Assert.IsTrue(timeframe.currentDate.Text.Equals("July 2016 - September 2016"));

            timeframe.setQ(3);
            Assert.IsTrue(timeframe.currentDate.Text.Equals("October 2016 - December 2016"));

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2017 - March 2017"));

            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("October 2016 - December 2016"));
        }

        [Test]
        [Description("View By Period: set single month (January 2017)")]
        public void test5()
        {
            timeframe.setBySingleMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2017 - January 2017"));

            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("February 2017 - February 2017"));
            
            timeframe.nextMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("March 2017 - March 2017"));

            timeframe.setBySingleMonth();
            timeframe.prevMonth();
            Assert.IsTrue(timeframe.currentDate.Text.Equals("December 2016 - December 2016"));
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
            Assert.IsTrue(timeframe.currentDate.Text.Equals("January 2017 - April 2018"));
        }

        //TBD add seasons tests!!!!
    }
}
