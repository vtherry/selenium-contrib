using System;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Contrib.Extensions;
using Selenium.Contrib.Pages;

namespace Selenium.Contrib.Navigation
{
    /// <summary>
    /// Classe qui pemet d'encapsuler la complexité de la navigation web (synchronisation avec le driver)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public class Navigator<TSource> : INavigator<TSource> where TSource : PageObject
    {
        private readonly TSource _pageObject;

        public Navigator(TSource pageObject)
        {
            _pageObject = pageObject;
        }

        /// <summary>
        /// Navigue vers une page de destination. Le navigateur charge la nouvelle page.
        /// </summary>
        /// <typeparam name="TDest">Type du Page Object sur lequel on navigue</typeparam>
        /// <param name="args">Arguments à donner lors de l'instanciation du Page Object de destination</param>
        /// <returns>Le Page Object de destination instancié</returns>
        public TDest PostBack<TDest>(params object[] args) where TDest : PageObject
        {
            // Attend que la page source soit totalement déchargée (lorsque l'accès aux éléments n'est plus possible)
            var webDriverWait = new WebDriverWait(_pageObject.WebDriver, Timeouts.DefaultTimeoutPostBack);
            webDriverWait.Until(PageUnloaded);

            // Instancie le Page Object de destination
            return (TDest)Activator.CreateInstance(typeof(TDest), args);
        }

        /// <summary>
        /// Navigue à l'intérieur de la page pour retourner un partial Page Object (UserControl, composant spécifique, fieldset, formulaire, etc ...)
        /// </summary>
        /// <typeparam name="TPartial">Type du partial Page Object sur lequel on navigue</typeparam>
        /// <param name="args">Arguments à donner lors de l'instanciation du partial Page Object de destination</param>
        /// <returns>Le partial Page Object instancié</returns>
        public TPartial Partial<TPartial>(params object[] args)
            where TPartial : PartialPage<TSource>
        {
            // Instancie le partial
            var partialPage = (TPartial)Activator.CreateInstance(typeof(TPartial), args);

            // Enregistre le Page Object source
            partialPage.SetCaller(_pageObject);

            return partialPage;
        }

        /// <summary>
        /// Navigue à l'intérieur de la page pour retourner une fênetre modale
        /// </summary>
        /// <typeparam name="TModal">Type de fenêtre modale sur laquelle on navigue</typeparam>
        /// <param name="args">Arguments à donner lors de l'instanciation de la fenêtre modale de destination</param>
        /// <returns>La fenêtre modale instanciée</returns>
        public TModal Modal<TModal>(params object[] args) where TModal : Modal<TSource>
        {
            // Attente de l'ouverture de la fenêtre modale (souvent avec un effet JS)
            Thread.Sleep(Timeouts.DefaultTimeoutWaitForComponentToShow);

            // Instancie le Page Object de la fenêtre modale
            var partialPage = (TModal)Activator.CreateInstance(typeof(TModal), args);

            // Enregistre le Page Object source
            partialPage.SetCaller(_pageObject);

            return partialPage;
        }

        public TSource ContinueWithSamePage()
        {
            // Pas besoin d'instancier un nouveau Page Object lorsqu'on reste sur la même page web
            // Retourne le Page Object source, on ne fait rien
            return _pageObject;
        }

        public TSource WaitForAjaxComplete()
        {
            // Attente de la fin d'un appel Ajax
            _pageObject.WebDriver.WaitForAjaxComplete();

            return _pageObject;
        }

        private bool PageUnloaded(IWebDriver webDriver)
        {
            try
            {
                // Tentative d'accès à un élément du Page Object source afin de s'assurer que la page web source est bien déchargée
                _pageObject.IsPageObjectLoaded();
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException is StaleElementReferenceException;
            }
        }
    }
}
