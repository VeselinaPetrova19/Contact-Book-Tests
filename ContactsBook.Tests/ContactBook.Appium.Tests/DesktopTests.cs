using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace ContactBook.Appium.Tests
{
    public class DesktopTests
    {
        private const string appiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string appUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Users\Mitax\Desktop\ContactBook-DesktopClient\ContactBook-DesktopClient.exe";

        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void OpenBrowser()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);
            driver = new WindowsDriver<WindowsElement>(new Uri(appiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Search_VerityFirstResult()
        {
            var urlTextBox = driver.FindElementByAccessibilityId("textBoxApiUrl");
            urlTextBox.Clear();
            urlTextBox.SendKeys(appUrl);
            driver.FindElementByAccessibilityId("buttonConnect").Click();

            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName);

            var searchField = driver.FindElementByAccessibilityId("textBoxSearch");
            searchField.SendKeys("steve");

            driver.FindElementByAccessibilityId("buttonSearch").Click();

            Thread.Sleep(2000);

            var resultLabel = driver.FindElementByAccessibilityId("labelResult").Text;
            Assert.That(resultLabel, Is.EqualTo("Contacts found: 1"));

            var firstName = driver.FindElement(By.XPath("//Edit[@Name=\"FirstName Row 0, Not sorted.\"]")).Text;
            var lastName = driver.FindElement(By.XPath("//Edit[@Name=\"LastName Row 0, Not sorted.\"]")).Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }
    }
}