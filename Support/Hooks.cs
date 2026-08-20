using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;

namespace AMAZON_Selenium_C.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        private static readonly ExtentReports _extent = ExtentReportManager.GetExtentReport();

        private ExtentTest? _extentTest;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Create WebDriver
            IWebDriver driver = DriverFactory.CreateDriver();

            // Store driver in ScenarioContext
            _scenarioContext["Driver"] = driver;

            // Get scenario name
            string scenarioName =
                _scenarioContext.ScenarioInfo.Title;

            // Create Extent test
            _extentTest =
                _extent.CreateTest(scenarioName);

            // Store ExtentTest in ScenarioContext
            _scenarioContext["ExtentTest"] = _extentTest;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Get driver
            if (_scenarioContext.TryGetValue(
                "Driver",
                out IWebDriver? driver))
            {
                driver.Quit();
                driver.Dispose();
            }

            // Get ExtentTest
            if (_scenarioContext.TryGetValue(
                "ExtentTest",
                out ExtentTest? extentTest))
            {
                if (_scenarioContext.TestError != null)
                {
                    extentTest.Fail(
                        _scenarioContext.TestError.ToString()
                    );
                }
                else
                {
                    extentTest.Pass(         "Scenario passed successfully."
                    );
                }
            }

            // Write report to HTML
            _extent.Flush();
        }
    }
}