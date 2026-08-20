namespace AMAZON_Selenium_C.Support
{
    public class TestExecutionContext
    {
        public string FeatureName { get; set; } = string.Empty;

        public string ScenarioName { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public string Environment { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Duration =>
            EndTime - StartTime;
    }
}