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
    internal class licensing: BaseClass
    {
        private Help help;
        MainPage main;
        LoginPage loginPage;
        LicensingPOM Licens;
        

        [SetUp]
        public void setup()
        {
            Licens = new LicensingPOM(driver);
        }

        [Test]
        [Description("Full - active user")]
        public void test1()
        {
            Help help = new Help();
            help.changeLicensing(userName, Password, farmCode, "13/05/2024", "Full");
            bool ans = Licens.assertLogin(userName, Password);
            Assert.IsTrue(ans);
        }
        [Test]
        [Description("Full - near to expire")]
        public void test2()
        {
            Help help = new Help();
            help.changeLicensing(userName, Password, farmCode, DateTime.Now.AddDays(7).ToString("dd/MM/yy"), "Full");
            bool ans = Licens.assertNearExpired(userName, Password);
            Assert.IsTrue(ans);
        }


        [Test]
        [Description("Full - expire")]
        public void test3()
        {
            Help help = new Help();
            help.changeLicensing(userName, Password, farmCode, "13/05/2020", "Full");
            bool ans = Licens.assertExpired(userName, Password);
            Assert.IsTrue(ans);
        }

        [OneTimeTearDown]
        public void backToActive()
        {
            Help help = new Help();
            help.changeLicensing(userName, Password, farmCode, "13/05/2024", "Full");
        }
    }
}
