using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; // WebDriverWait
using SeleniumExtras.WaitHelpers; // ExpectedConditions

class MyWait
{
    WebDriverWait realWait;
    public MyWait(IWebDriver driver) =>
        realWait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));

    public IWebElement UntilExists(By condition) =>
        realWait.Until(ExpectedConditions.ElementExists(condition));
    public IWebElement UntilClickable(IWebElement element) =>
        realWait.Until(ExpectedConditions.ElementToBeClickable(element));
}
