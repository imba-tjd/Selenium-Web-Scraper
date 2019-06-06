using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Demo
{
    class BaiduDemo
    {
        public void Run()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl("http://www.baidu.com/");
                IWebElement query = driver.FindElement(By.Id("kw"));
                query.SendKeys("奶酪");
                query.Submit();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(_ => true);
                Console.WriteLine("Page title is: " + driver.Title);
            }
        }
    }
}
