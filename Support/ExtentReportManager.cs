using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace AMAZON_Selenium_C.Support
{
    public static class ExtentReportManager
    {
        private static ExtentReports? _extent;

        public static ExtentReports GetExtentReport()
        {
            if (_extent == null)
            {
                // Project root folder
                string projectDirectory =
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
                    .Parent!
                    .Parent!
                    .Parent!
                    .FullName;

                // Reports folder
                string reportDirectory =
                    Path.Combine(projectDirectory, "C:\\Selenium C#\\AMAZON_Selenium_c#\\Reports");

                Directory.CreateDirectory(reportDirectory);

                string reportPath =
                    Path.Combine(reportDirectory, "ExtentReport.html");

                Console.WriteLine("======================================");
                Console.WriteLine("Extent Report Path:");
                Console.WriteLine(reportPath);
                Console.WriteLine("======================================");

                ExtentSparkReporter sparkReporter =
                    new ExtentSparkReporter(reportPath);

                sparkReporter.Config.DocumentTitle =
                    "Selenium C# BDD Automation Report";

                sparkReporter.Config.ReportName =
                    "CoverForce Selenium C# BDD Test Execution";

                _extent = new ExtentReports();

                _extent.AttachReporter(sparkReporter);

                _extent.AddSystemInfo(
                    "Project",
                    "COVERFORCE Selenium C# BDD"
                );

                _extent.AddSystemInfo(
                    "Framework",
                    "Selenium + Reqnroll + NUnit"
                );

                _extent.AddSystemInfo(
                    "Browser",
                    "Chrome"
                );

                _extent.AddSystemInfo(
                    "Environment",
                    "QA"
                );
            }

            return _extent;
        }
    }
}