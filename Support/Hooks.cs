using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;

namespace AMAZON_Selenium_C.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        private static readonly ExtentReports _extent =
            ExtentReportManager.GetExtentReport();

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
            _extentTest = _extent.CreateTest(scenarioName);

            // Store ExtentTest
            _scenarioContext["ExtentTest"] = _extentTest;

            _extentTest.Info("Scenario started");
        }

        [AfterStep]
        public void AfterStep()
        {
            if (!_scenarioContext.TryGetValue(
                "ExtentTest",
                out ExtentTest? extentTest))
            {
                return;
            }

            string stepText =
                _scenarioContext.StepContext.StepInfo.Text;

            if (_scenarioContext.TestError == null)
            {
                extentTest.Pass(
                    $"Step Passed: {stepText}"
                );
            }
            else
            {
                extentTest.Fail(
                    $"Step Failed: {stepText}"
                );

                extentTest.Fail(
                    _scenarioContext.TestError.ToString()
                );

                CaptureFailureScreenshot(extentTest);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TryGetValue(
                "ExtentTest",
                out ExtentTest? extentTest))
            {
                if (_scenarioContext.TestError != null)
                {
                    extentTest.Fail("Scenario Failed");
                }
                else
                {
                    extentTest.Pass("Scenario Passed");
                }
            }

            if (_scenarioContext.TryGetValue(
                "Driver",
                out IWebDriver? driver))
            {
                driver.Quit();
                driver.Dispose();
            }

            _extent.Flush();
        }

        private void CaptureFailureScreenshot(
            ExtentTest extentTest)
        {
            try
            {
                if (_scenarioContext.TryGetValue(
                    "Driver",
                    out IWebDriver? driver))
                {
                    if (driver is ITakesScreenshot screenshotDriver)
                    {
                        Screenshot screenshot =
                            screenshotDriver.GetScreenshot();

                        string screenshotDirectory =
                            Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "Screenshots"
                            );

                        Directory.CreateDirectory(
                            screenshotDirectory
                        );

                        string fileName =
                            $"Failure_{DateTime.Now:yyyyMMdd_HHmmssfff}.png";

                        string screenshotPath =
                            Path.Combine(
                                screenshotDirectory,
                                fileName
                            );

                        screenshot.SaveAsFile(
                            screenshotPath
                        );

                        extentTest.AddScreenCaptureFromPath(
                            screenshotPath
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                extentTest.Warning(
                    $"Unable to capture screenshot: {ex.Message}"
                );
            }
        }
    }
}