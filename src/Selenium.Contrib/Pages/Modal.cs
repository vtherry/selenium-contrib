using System;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.Contrib.Pages
{
    public abstract class Modal<TCaller> : PartialPage<TCaller> where TCaller : PageObject
    {
        protected string TitleText { get; }
        protected abstract IWebElement ModalTitle { get; set; }

        protected Modal(IWebDriver webDriver, string titleText)
            : base(webDriver)
        {
            if (titleText == null)
                throw new ArgumentNullException(nameof(titleText));

            TitleText = titleText;

            EnsureTitleIsValid();
        }

        private void EnsureTitleIsValid()
        {
            var modalTitleText = ModalTitle.Text;
            if (!string.Equals(modalTitleText, TitleText))
            {
                throw new InvalidOperationException($"This is not the '{TitleText}' modal, current modal is '{modalTitleText}' ({WebDriver.Url}).");
            }
        }

        public override TCaller Done()
        {
            // Attente de la fermeture de la fenêtre modale
            Thread.Sleep(Timeouts.DefaultTimeoutWaitForComponentToHide);

            // Retourne la page qui à ouvert la fenêtre modale
            return base.Done();
        }
    }
}
