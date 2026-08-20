using System;
using System.Threading;
using OpenQA.Selenium;

namespace AMAZON_Selenium_C.Pages
{
    public class AmazonPage
    {
        private readonly IWebDriver _driver;

        private readonly By searchBox = By.Id("twotabsearchtextbo");

        public AmazonPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateToAmazon()
        {
            _driver.Navigate().GoToUrl("https://www.amazon.in");
            _driver.Manage().Window.Maximize();
        }

        public void SearchProduct(string productName)
        {
            IWebElement search = null;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    search = _driver.FindElement(searchBox);
                    if (search.Displayed)
                        break;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    Thread.Sleep(500);
                }
            }

            if (search == null)
                throw new OpenQA.Selenium.NoSuchElementException($"Search box with locator {searchBox} not found.");

            search.Clear();
            search.SendKeys(productName);
            search.SendKeys(Keys.Enter);
        }

        public bool IsSearchResultDisplayed()
        {
            return _driver.Url.Contains("s?k=");
        }
    }
}