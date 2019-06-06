using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

class FFFactory
{
    public IWebDriver GetFF()
    {
        var ffo = new FirefoxOptions();
        ffo.PageLoadStrategy = PageLoadStrategy.Eager;
        // ffo.AddArgument("--headless");

        var ff = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), ffo, System.TimeSpan.FromMinutes(2));
        ff.Manage().Timeouts().PageLoad = System.TimeSpan.FromMinutes(2);

        return ff;
    }
}
