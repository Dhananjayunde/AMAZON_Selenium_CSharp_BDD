using OpenQA.Selenium;
using Reqnroll;

namespace AMAZON_Selenium_C.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = DriverFactory.CreateDriver();

            _scenarioContext["Driver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TryGetValue("Driver", out IWebDriver? driver))
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}