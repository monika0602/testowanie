using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class NUnitTest
    {
        IWebDriver driver;
        //public void TestApp()
        //{
        //    Initialise();
        //    OpenAppTest();
        //    EndTest();
        //}

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void OpenAppTest()
        {
            driver.Url = "http://www.gmail.com";
            //driver.Url = www.demoqa.com";
        }

        [Test]
        public void OpenAppTest2()
        {
            driver.Url = "http://ux.up.krakow.pl/~mazela";
        }


        [TearDown]
        public void EndTest()
        {
            //driver.Close();
        }

    }
}
