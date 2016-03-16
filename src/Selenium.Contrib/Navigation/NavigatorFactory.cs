using System;
using Selenium.Contrib.Pages;

namespace Selenium.Contrib.Navigation
{
    public static class NavigatorFactory<TSource> where TSource : PageObject
    {
        /// <summary>
        ///     Crée un navigator adapté au Page Object source
        /// </summary>
        public static Func<TSource, INavigator<TSource>> Create = pageObject => new Navigator<TSource>(pageObject);
    }
}