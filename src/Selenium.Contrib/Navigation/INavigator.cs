using Selenium.Contrib.Pages;

namespace Selenium.Contrib.Navigation
{
    public interface INavigator<TSource> where TSource : PageObject
    {
        TDest PostBack<TDest>(params object[] args) where TDest : PageObject;
        TPartial Partial<TPartial>(params object[] args) where TPartial : PartialPage<TSource>;
        TModal Modal<TModal>(params object[] args) where TModal : Modal<TSource>;
        TSource ContinueWithSamePage();
        TSource WaitForAjaxComplete();
    }
}
