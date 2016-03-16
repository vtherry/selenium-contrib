using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.Contrib.Pages;

namespace Selenium.Contrib.Examples.Pages
{
    public class HomePage : PageObject
    {
        [CacheLookup]
        [FindsBy(How = How.ClassName, Using = "iconochive-person")]
        private IWebElement SignInLink { get; set; }

        [CacheLookup]
        [FindsBy(How = How.ClassName, Using = "mypic")]
        private IWebElement UsernameLink { get; set; }

        [CacheLookup]
        [FindsBy(How = How.ClassName, Using = "logout")]
        private IWebElement LogoutLink { get; set; }

        [CacheLookup]
        [FindsBy(How = How.Id, Using = "search-bar-1")]
        private IWebElement MainSearchTextBox { get; set; }

        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public LoginPage GoToSignInPage()
        {
            SignInLink.Click();

            return Navigate(this).PostBack<LoginPage>(WebDriver);
        }

        public override bool IsPageObjectLoaded()
        {
            return MainSearchTextBox.Displayed;
        }

        public HomePage SignOut()
        {
            UsernameLink.Click();
            LogoutLink.Click();

            return Navigate(this).PostBack<HomePage>(WebDriver);
        }
    }
}