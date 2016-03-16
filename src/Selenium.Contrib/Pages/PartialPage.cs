using OpenQA.Selenium;

namespace Selenium.Contrib.Pages
{
    public abstract class PartialPage<TCaller> : PageObject where TCaller : PageObject
    {
        protected TCaller CallerPageObject { get; set; }

        protected PartialPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void SetCaller(TCaller callerPageObject)
        {
            // Enregistre la page qui a accédé au partial
            CallerPageObject = callerPageObject;
        }

        public virtual TCaller Done()
        {
            // Retourne la page qui a accédé au partial
            return CallerPageObject;
        }
    }
}