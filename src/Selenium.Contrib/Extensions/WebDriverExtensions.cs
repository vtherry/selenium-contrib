using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Contrib.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInMilliseconds = 1000)
        {
            if (timeoutInMilliseconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static void WaitForPageLoaded(this IWebDriver driver)
        {
            var webDriverWait = new WebDriverWait(driver, Timeouts.DefaultTimeoutPageLoaded);
            webDriverWait.Until(webDriver => (bool) webDriver.ExecuteScript("return document.readyState == 'complete' && (!window.jQuery || !window.jQuery.active)"));
        }

        public static void WaitForAjaxComplete(this IWebDriver driver)
        {
            Thread.Sleep(500); // 500ms d'attente pour laisser le temps à jQuery d'updater son statut Ajax

            var webDriverWait = new WebDriverWait(driver, Timeouts.DefaultTimeoutAjaxCall);
            webDriverWait.Until(webDriver => (bool) webDriver.ExecuteScript("return !window.jQuery || !window.jQuery.active"));
        }

        public static object ExecuteScript(this IWebDriver driver, string script, params object[] args)
        {
            var javascriptExecutor = (IJavaScriptExecutor) driver;
            return javascriptExecutor.ExecuteScript(script, args);
        }
    }
}