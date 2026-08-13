using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AMAZON_Selenium_C.Support
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);

            return driver;
        }
    }
}