using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class NUnitTest
    {
        IWebDriver driver;
        public static readonly string BASE_URL = "http://automationpractice.com/index.php";
        public string accountUrl = "?controller=authentication&back=my-account";
        public string cartUrl = "?controller=order";
        public string resetPasswordUrl = "?controller=password";

        public void TestApp()
        {
           Initialize();
        
           EndTest();
        }

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(BASE_URL);
        }
       
 
        [Test]
        public void T_01_CreatAccountCorrectData()
        {
            string email = "address@name.pl";
            string successUrl = BASE_URL + "?controller=authentication&back=my-account";

            driver.Navigate().GoToUrl(BASE_URL + accountUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email_create"));
            IWebElement btnSubmit = driver.FindElement(By.Id("SubmitCreate"));

            emailForm.SendKeys(email);
            btnSubmit.Click();

            Assert.AreEqual(successUrl, driver.Url);


        }

        [Test]
        public void T_02_CreatAccountIncorrectData()
        {
            string incorrectEmail = "testData";

            driver.Navigate().GoToUrl(BASE_URL + accountUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email_create"));
            IWebElement btnSubmit = driver.FindElement(By.Id("SubmitCreate"));

            emailForm.SendKeys(incorrectEmail);
            btnSubmit.Click();

            IWebElement errorAlert = driver.FindElement(By.Id("create_account_error"));
            Assert.IsNotNull(errorAlert);
        
        }

        [Test]
        public void T_03_LogInOnSuccess()
        {
            string email = "grazyna.binkiewicz91@gmail.com";
            string password = "haslo1";
            string successUrl = BASE_URL + "?controller=my-account";

            driver.Navigate().GoToUrl(BASE_URL + accountUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email"));
            IWebElement passwordForm = driver.FindElement(By.Id("passwd"));
            IWebElement btnSubmit = driver.FindElement(By.Id("SubmitLogin"));

            emailForm.SendKeys(email);
            passwordForm.SendKeys(password);
            btnSubmit.Click();

            Assert.AreEqual(successUrl, driver.Url);
        }

        [Test]
        public void T_04_LogInOnFailure()
        {
            string email = "grazyna.binkiewicz91@gmail.com";
            string password = "haslo123";
            string successUrl = BASE_URL + "?controller=my-account";

            driver.Navigate().GoToUrl(BASE_URL + accountUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email"));
            IWebElement passwordForm = driver.FindElement(By.Id("passwd"));
            IWebElement btnSubmit = driver.FindElement(By.Id("SubmitLogin"));

            emailForm.SendKeys(email);
            passwordForm.SendKeys(password);
            btnSubmit.Click();

            IWebElement errorAlert = driver.FindElement(By.ClassName("alert-danger"));
            Assert.IsNotNull(errorAlert);

        }

        [Test]
        public void T_05_RestetPasswordOnSuccess()
        {
            string email = "grazyna.binkiewicz91@gmail.com";

            driver.Navigate().GoToUrl(BASE_URL + resetPasswordUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email"));
            IWebElement parent = driver.FindElement(By.ClassName("submit"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            emailForm.SendKeys(email);
           
            btnSubmit.Click();
           
            IWebElement successAlert = driver.FindElement(By.ClassName("alert-success"));
            Assert.IsNotNull(successAlert);
        }


        [Test]
        public void T_06_RestetPasswordOnFailure()
        {
            string email = "1234@sdadw.lko";

            driver.Navigate().GoToUrl(BASE_URL + resetPasswordUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email"));
            IWebElement parent = driver.FindElement(By.ClassName("submit"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            emailForm.SendKeys(email);

            btnSubmit.Click();


            IWebElement errorAlert = driver.FindElement(By.ClassName("alert-danger"));
            Assert.IsNotNull(errorAlert);
        }

        [Test]
        public void T_07_SignOut()
        {
            string email = "grazyna.binkiewicz91@gmail.com";
            string password = "haslo1";
            string successUrl = BASE_URL + "?controller=authentication&back=my-account";

            driver.Navigate().GoToUrl(BASE_URL + accountUrl);
            IWebElement emailForm = driver.FindElement(By.Id("email"));
            IWebElement passwordForm = driver.FindElement(By.Id("passwd"));
            IWebElement btnSubmit = driver.FindElement(By.Id("SubmitLogin"));

            emailForm.SendKeys(email);
            passwordForm.SendKeys(password);
            btnSubmit.Click();

            IWebElement btnSignOut = driver.FindElement(By.ClassName("logout"));
            btnSignOut.Click();

            Assert.AreEqual(successUrl, driver.Url);
        }

        [Test]
        public void T_08_AddToCart()
        {
            driver.Navigate().GoToUrl(BASE_URL + "?id_product=3&controller=product");

            IWebElement parent = driver.FindElement(By.Id("add_to_cart"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            btnSubmit.Click();

            IWebElement success = driver.FindElement(By.ClassName("icon-ok"));
            Assert.IsNotNull(success);
        }

        [Test]
        public void T_09_ChangeQuantityInCart()
        {
            driver.Navigate().GoToUrl(BASE_URL + "?id_product=3&controller=product");

            IWebElement parent = driver.FindElement(By.Id("add_to_cart"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            btnSubmit.Click();
            Thread.Sleep(200);
            driver.FindElement(By.CssSelector("a[href *= 'controller=order']")).Click();
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl(BASE_URL + cartUrl);
            driver.FindElement(By.ClassName("cart_quantity_up")).Click();

            Thread.Sleep(3000);
            string quantity  = driver.FindElement(By.ClassName("cart_quantity_input")).GetAttribute("value");
            Assert.AreEqual(quantity, "2");
        }

        [Test]
       
        public void T_10_RemoveFromCart()
        {
            driver.Navigate().GoToUrl(BASE_URL + "?id_product=3&controller=product");

            IWebElement parent = driver.FindElement(By.Id("add_to_cart"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            btnSubmit.Click();
            Thread.Sleep(200);
            driver.FindElement(By.CssSelector("a[href *= 'controller=order']")).Click();
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl(BASE_URL + cartUrl);

            driver.FindElement(By.ClassName("cart_quantity_delete")).Click();

            IWebElement successAlert = driver.FindElement(By.ClassName("alert-warning"));
            Assert.IsNotNull(successAlert);

        }

        [Test]
        public void T_11_GoToCheckOut()
        {
            string successUrl = BASE_URL + "?controller=authentication&multi-shipping=0&display_guest_checkout=0&back=http%3A%2F%2Fautomationpractice.com%2Findex.php%3Fcontroller%3Dorder%26step%3D1%26multi-shipping%3D0";

            driver.Navigate().GoToUrl(BASE_URL + "?id_product=3&controller=product");

            IWebElement parent = driver.FindElement(By.Id("add_to_cart"));
            IWebElement btnSubmit = parent.FindElement(By.CssSelector("button[type=submit]"));

            btnSubmit.Click();
            Thread.Sleep(200);
            driver.FindElement(By.CssSelector("a[href *= 'controller=order']")).Click();
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl(BASE_URL + cartUrl);

            driver.FindElement(By.CssSelector("a[href *= 'controller=order&step=1']")).Click();


            Assert.AreEqual(successUrl, driver.Url);

        }

        [Test]
        public void T_12_Search()
        {
            string input = "dresses";
            string successUrl = BASE_URL + "?controller=search&orderby=position&orderway=desc&search_query=dresses&submit_search=";

            driver.Navigate().GoToUrl(BASE_URL);


            IWebElement searchForm = driver.FindElement(By.Id("search_query_top"));
            IWebElement btnSearch = driver.FindElement(By.ClassName("button-search"));

            searchForm.SendKeys(input);
            btnSearch.Click();

            Assert.AreEqual(successUrl, driver.Url);

        }
 

        [TearDown]
        public void EndTest()
        {
            //driver.Close();
        }

    }
}
