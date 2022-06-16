using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ContactBook.UI.Tests
{
    public class UITests
    {
        private const string url = "https://contactbook.nakov.repl.co";
        private WebDriver driver;

        [SetUp]
        public void Open()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();  
        }

        [Test]
        public void Test1_ListOfContacts_FirstContact()
        {
            driver.Navigate().GoToUrl(url);

            var contacButton = driver.FindElement(By.LinkText("Contacts"));
            contacButton.Click();

            var firstName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test2_Search_FindAlbert()
        {
            driver.Navigate().GoToUrl(url);

            var searchButtom = driver.FindElement(By.LinkText("Search"));
            searchButtom.Click();

            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("albert");

            var search = driver.FindElement(By.Id("search"));
            search.Click();

            var response = driver.FindElement(By.Id("searchResult")).Text;

            var firstName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.That(response, Is.EqualTo("1 contacts found."));
            Assert.That(firstName, Is.EqualTo("Albert"));
            Assert.That(lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test3_Search_InvalidData()
        {
            driver.Navigate().GoToUrl(url);

            var searchButtom = driver.FindElement(By.LinkText("Search"));
            searchButtom.Click();

            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("invalid2635");

            var search = driver.FindElement(By.Id("search"));
            search.Click();

            var response = driver.FindElement(By.Id("searchResult")).Text;

            Assert.That(response, Is.EqualTo("No contacts found."));
        }

        [Test]
        public void Test4_Create_InvalidData()
        {
            driver.Navigate().GoToUrl(url);

            var createButton = driver.FindElement(By.LinkText("Create"));
            createButton.Click();

            var firstName = driver.FindElement(By.Id("firstName"));
            firstName.SendKeys("Alina");

            var create = driver.FindElement(By.Id("create"));
            create.Click();

            var err = driver.FindElement(By.CssSelector("div.err")).Text;

            Assert.That(err, Is.EqualTo("Error: Last name cannot be empty!"));
        }

        [Test]
        public void Test5_Create_ValidData()
        {
            driver.Navigate().GoToUrl(url);

            var createButton = driver.FindElement(By.LinkText("Create"));
            createButton.Click();

            var firstName = driver.FindElement(By.Id("firstName"));
            firstName.SendKeys("Alina");

            var lastName = driver.FindElement(By.Id("lastName"));
            lastName.SendKeys("Balina");

            var email = driver.FindElement(By.Id("email"));
            email.SendKeys("aliB1@gigi.com");

            var phone = driver.FindElement(By.Id("phone"));
            phone.SendKeys("088888888");

            var comments = driver.FindElement(By.Id("comments"));
            comments.SendKeys("colleague");

            var create = driver.FindElement(By.Id("create"));
            create.Click();

            var allContact = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContact = allContact.Last();

            var firstNameLabel = lastContact.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastNameLabel = lastContact.FindElement(By.CssSelector("tr.lname > td")).Text;
            
            Assert.That(firstNameLabel, Is.EqualTo("Alina"));
            Assert.That(lastNameLabel, Is.EqualTo("Balina"));
        }
    }
}
