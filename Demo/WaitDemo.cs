using System;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Demo
{
    class WaitDemo
    {
        FirefoxOptions FO = new FirefoxOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
        string url = "https://leetcode.com/problemset/all/";
        public void Run()
        {
            using (IWebDriver driver = new FirefoxDriver(FO))
            {
                driver.Navigate().GoToUrl(url);

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                var tbody = wait.Until(d => d.FindElement(By.CssSelector("tbody.reactable-data")));

                Console.WriteLine(tbody.TagName);
            }
        }

        public void Run2()
        {
            using (IWebDriver driver = new FirefoxDriver(FO))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

                IReadOnlyCollection<IWebElement> query;
                while ((query = driver.FindElements(By.CssSelector("tbody.reactable-data"))).Count == 0)
                    Console.WriteLine("waiting..."); // continue;
                var tbody = query.ElementAt(0);

                Console.WriteLine(tbody.TagName);
            }
        }

        public void Run3()
        {
            using (IWebDriver driver = new FirefoxDriver(FO))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);

                IReadOnlyCollection<IWebElement> query;
                while ((query = driver.FindElements(By.CssSelector("tbody.reactable-data"))).Count == 0)
                    Console.WriteLine("waiting..."); // continue;
                query = driver.FindElements(By.CssSelector("tbody.reactable-data"));
                var tbody = query.ElementAt(0);

                Console.WriteLine(tbody.TagName);
            }
        }
    }
}
