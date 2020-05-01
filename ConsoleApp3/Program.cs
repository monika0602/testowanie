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
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            //IWebDriver driver = new FirefoxDriver();

            //driver.Url = "http://ux.up.krakow.pl/~mazela";
            driver.Url = "http://www.demoqa.com";
            makeTest(driver);
            driver = new ChromeDriver();
            makeTest(driver);
        }

        static void makeTest(IWebDriver browser)
        {
            browser.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");

            //Otwarcie pierwszego okna za pomoc¹ przycisku
            browser.FindElement(By.Id("button1")).Click();

            //Otwarcie drugiego okna bezpoœrednio za pomoc¹ funkcji javascript
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;
            try
            {
                js.ExecuteScript("newMsgWin()");
            }
            catch
            {

            }


            //Testujemy iloœæ okien, by zobaczyæ czy s¹ otwarte
            IList<string> allWindowHandles = browser.WindowHandles;

            Assert.True(allWindowHandles.Count == 3);


            string newWindow;
            try
            {
                //Zamykamy dodatkowe okna i wracamy do g³ównego
                newWindow = allWindowHandles[1];
                browser.SwitchTo().Window(newWindow);
                browser.Close();
                newWindow = allWindowHandles[2];
                browser.SwitchTo().Window(newWindow);
                browser.Close();
                newWindow = allWindowHandles[0];
                browser.SwitchTo().Window(newWindow);
            }
            catch
            {
            }

            //Testujemy formularz

            //Przygotowanie elementów
            IList<IWebElement> formElements = new List<IWebElement>();
            IWebElement name = browser.FindElement(By.Name("name"));
            formElements.Add(name);
            IWebElement email = browser.FindElement(By.XPath("//input[@placeholder='E-mail *']"));
            formElements.Add(email);
            IWebElement telephone = browser.FindElement(By.Name("telephone"));
            formElements.Add(telephone);
            IWebElement country = browser.FindElement(By.Name("country"));
            formElements.Add(country);
            IWebElement company = browser.FindElement(By.Name("company"));
            formElements.Add(company);
            IWebElement message = browser.FindElement(By.Name("message"));
            formElements.Add(message);

            IWebElement submit = browser.FindElement(By.ClassName("dt-btn-submit"));


            //Przypadek wpisania b³êdnych wartoœci
            clearElementsInList(formElements);
            formElements[0].SendKeys("name");
            formElements[1].SendKeys("e-mail");
            formElements[2].SendKeys("telephone");
            formElements[3].SendKeys("country");
            formElements[4].SendKeys("company");
            formElements[5].SendKeys("message");

            submit.Click();
            try
            {
                IWebElement errorPopUp0 = browser.FindElement(By.ClassName("form-validation-field-1formError"));
                IWebElement errorPopUp1 = browser.FindElement(By.ClassName("form-validation-field-2formError"));
                //Testujemy czy s¹ wyœwietlane komunikaty b³êdów
                Assert.True(errorPopUp0.Displayed);
                Assert.True(errorPopUp1.Displayed);

                //Usuwamy popupy poprzez klikniêcie na nie
                errorPopUp0.Click();
                errorPopUp1.Click();
            }
            catch
            {

            }

            //Przypadek wpisania prawid³owych wartoœci
            clearElementsInList(formElements);
            formElements[0].SendKeys("name");
            formElements[1].SendKeys("aaa@aaa.com");
            formElements[2].SendKeys("123456789");
            formElements[3].SendKeys("country");
            formElements[4].SendKeys("company");
            formElements[5].SendKeys("message");

            submit.Click();
            try
            {
                browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement sucessPopUp0 = browser.FindElement(By.ClassName("greenPopup"));
                //Testujemy czy s¹ wyœwietlany komunikaty sukcesu
                Assert.True(sucessPopUp0.Displayed);

                //Usuwamy popup poprzez klikniêcie na niego
                sucessPopUp0.Click();
            }
            catch
            {

            }



            browser.Close();

        }
        static void clearElementsInList(IList<IWebElement> list)
        {
            foreach (var element in list)
            {
                element.Clear();
            }
        }
    }
}

