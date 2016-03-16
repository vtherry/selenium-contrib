using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Contrib.Extensions
{
    public static class WebElementExtensions
    {
        public static void ClickAndWait(this IWebElement element, int timeoutInMilliseconds = 1000)
        {
            element.Click();
            Thread.Sleep(timeoutInMilliseconds);
        }

        public static void SelectByText(this IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        public static void SelectByValue(this IWebElement element, string value)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);
        }

        public static void SelectByValue(this IWebElement element, int value)
        {
            element.SelectByValue(value.ToString());
        }

        public static void SetText(this IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static void SetText(this IWebElement element, object value)
        {
            element.SetText(value.ToString());
        }
    }
}