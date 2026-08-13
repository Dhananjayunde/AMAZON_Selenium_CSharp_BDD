using AMAZON_Selenium_C.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using AMAZON_Selenium_C.Pages;
using Reqnroll;

namespace AMAZON_Selenium_C.StepDefinitions
{
    [Binding]
    public class AmazonSearchSteps
    {
        private readonly IWebDriver _driver;
        private readonly AmazonPage _amazonPage;

        public AmazonSearchSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("Driver");

            _amazonPage = new AmazonPage(_driver);
        }

        [Given("I navigate to Amazon")]
        public void GivenINavigateToAmazon()
        {
            _amazonPage.NavigateToAmazon();
        }

        [When("I search for {string}")]
        public void WhenISearchFor(string productName)
        {
            _amazonPage.SearchProduct(productName);
        }

        [Then("Amazon search results should be displayed")]
        public void ThenAmazonSearchResultsShouldBeDisplayed()
        {
            Assert.That(
                _amazonPage.IsSearchResultDisplayed(),
                Is.True,
                "Amazon search results were not displayed."
            );
        }
    }
}