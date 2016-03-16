using System;

namespace Selenium.Contrib
{
    public static class Timeouts
    {
        public static readonly TimeSpan DefaultTimeoutPageLoaded = TimeSpan.FromMilliseconds(30000); // 30s
        public static readonly TimeSpan DefaultTimeoutPostBack = TimeSpan.FromMilliseconds(30000); // 30s
        public static readonly TimeSpan DefaultTimeoutAjaxCall = TimeSpan.FromMilliseconds(30000); // 30s
        public static readonly TimeSpan DefaultTimeoutWaitForComponentToShow = TimeSpan.FromMilliseconds(1000); // 1s
        public static readonly TimeSpan DefaultTimeoutWaitForComponentToHide = TimeSpan.FromMilliseconds(1000); // 1s
    }
}