using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Selenium.Contrib.Examples.Pages;

namespace Selenium.Contrib.Examples
{
    [TestClass]
    public class AccountTests
    {
        public IWebDriver WebDriver { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            WebDriver = WebDriverFactory.GetWebDriver();
            WebDriver.Navigate().GoToUrl("http://archive.org");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WebDriver.Quit();
        }

        [TestMethod]
        public void SignIn()
        {
            new HomePage(WebDriver)
                .GoToSignInPage()
                .SignIn("vincent.test@spamhereplease.com", "azerty");

            Assert.AreEqual(WebDriver.Title, "Internet Archive: Digital Library of Free Books, Movies, Music & Wayback Machine");
        }

        [TestMethod]
        public void SignOut()
        {
            new HomePage(WebDriver)
                .GoToSignInPage()
                .SignIn("vincent.test@spamhereplease.com", "azerty")
                .SignOut();

            Assert.AreEqual(WebDriver.Title, "Internet Archive: Digital Library of Free Books, Movies, Music & Wayback Machine");
        }
    }
}
