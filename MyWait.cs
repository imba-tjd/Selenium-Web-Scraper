using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; // WebDriverWait
using SeleniumExtras.WaitHelpers; // ExpectedConditions

class MyWait
{
    WebDriverWait realWait;
    public MyWait(IWebDriver driver) =>
        realWait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(15));

    public IWebElement Exists(By condition) =>
        realWait.Until(ExpectedConditions.ElementExists(condition));
    public IWebElement Clickable(IWebElement element) =>
        realWait.Until(ExpectedConditions.ElementToBeClickable(element));
    public TResult Content<TResult>(System.Func<TResult> content) =>
        realWait.Until(_ => content());
}
