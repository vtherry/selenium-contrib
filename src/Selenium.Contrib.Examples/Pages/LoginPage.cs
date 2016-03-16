using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.Contrib.Extensions;
using Selenium.Contrib.Pages;

namespace Selenium.Contrib.Examples.Pages
{
    public class LoginPage : PageObject
    {
        [CacheLookup]
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement UsernameTextBox { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement PasswordTextBox { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "submit")]
        private IWebElement LoginButton { get; set; }

        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public HomePage SignIn(string username, string password)
        {
            UsernameTextBox.SetText(username);
            PasswordTextBox.SetText(password);
            LoginButton.Click();

            return Navigate(this).PostBack<HomePage>(WebDriver);
        }

        public override bool IsPageObjectLoaded()
        {
            return UsernameTextBox.Displayed;
        }
    }
}