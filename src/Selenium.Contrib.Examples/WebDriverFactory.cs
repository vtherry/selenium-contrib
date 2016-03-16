using OpenQA.Selenium.Firefox;

namespace Selenium.Contrib.Examples
{
    public static class WebDriverFactory
    {
        public static FirefoxDriver GetWebDriver()
        {
            var profile = new FirefoxProfile();
            var driver = new FirefoxDriver(profile);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}