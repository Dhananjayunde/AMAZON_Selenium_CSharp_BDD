using OpenQA.Selenium;

namespace AMAZON_Selenium_C.Pages
{
    public class AmazonPage
    {
        private readonly IWebDriver _driver;

        private readonly By searchBox =
            By.Id("twotabsearchtextbox");

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
            var search = _driver.FindElement(searchBox);

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