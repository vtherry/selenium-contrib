using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Selenium.Contrib.Extensions;
using Selenium.Contrib.Navigation;

namespace Selenium.Contrib.Pages
{
    public abstract class PageObject
    {
        protected const int TIMEOUT = 2000; // en millisecondes
        public IWebDriver WebDriver { get; }
        public string Title => WebDriver.Title;

        protected PageObject(IWebDriver webDriver)
        {
            if (webDriver == null)
                throw new ArgumentNullException(nameof(webDriver));

            WebDriver = webDriver;

            // Attend que la page web soit totalement chargée dans le navigateur avant de tenter d'initialiser tous les éléments de la page
            WebDriver.WaitForPageLoaded();

            // Initialise tous les éléments du Page Object
            PageFactory.InitElements(WebDriver, this);

            // Attend que le Page Object soit totalement chargé
            EnsurePageObjectIsLoaded();
        }

        /// <summary>
        ///     Méthode abstraite à overrider pour s'assurer que le Page Object est complètement affiché
        /// </summary>
        /// <remarks>
        ///     On peut s'appuyer sur la présence/visibilité d'un élément sur la page, etc ...
        ///     ATTENTION : Il ne faut pas que l'élément sur lequel on s'appuie soit annoté par un attribut [CacheLookup].
        ///     Le WebDriver n'ira pas rechercher l'élément (s'il a déjà été chargé) sinon, et donc la page semblera toujours
        ///     chargée.
        /// </remarks>
        public abstract bool IsPageObjectLoaded();

        public INavigator<TSource> Navigate<TSource>(TSource pageObject) where TSource : PageObject
        {
            // Retourne une Navigation typée adaptée au Page Object source
            return NavigatorFactory<TSource>.Create(pageObject);
        }

        protected void EnsurePageObjectIsLoaded()
        {
            // Attend que le Page Object soit totalement chargé
            var webDriverWait = new WebDriverWait(WebDriver, Timeouts.DefaultTimeoutPageLoaded);
            webDriverWait.Until(PageObjectHasLoaded);
        }

        protected virtual bool PageObjectHasLoaded(IWebDriver webDriver)
        {
            return IsPageObjectLoaded();
        }

        public static T LoadPage<T>(IWebDriver webDriver, string url) where T : PageObject
        {
            // Navigue sur l'url demandée
            webDriver.Navigate().GoToUrl(url);

            // Instancie la page demandée en fonction de son type
            return (T)Activator.CreateInstance(typeof(T), webDriver);
        }

        public T As<T>() where T : PageObject
        {
            return (T)this;
        }
    }
}
