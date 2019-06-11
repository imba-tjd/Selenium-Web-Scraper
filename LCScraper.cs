using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using OpenQA.Selenium;

class LCScrapper : IDisposable
{
    IWebDriver Driver { get; }
    MyWait Wait { get; }
    List<string> urls = new List<string>(1100);
    List<LCProblem> problems = new List<LCProblem>(1100);

    public LCScrapper(IWebDriver driver)
    {
        Driver = driver;
        Wait = new MyWait(driver);
    }

    public void Dispose() => Driver.Dispose();

    public void Run()
    {
        LoadProblems();
        ParseProblems();
        Save();
    }

    public void LoadProblems()
    {
        const string ProblemsURL = "https://leetcode.com/problemset/all/";

        Driver.Navigate().GoToUrl(ProblemsURL);

        while (true)
        {
            var tbody = Wait.Exists(By.CssSelector("tbody.reactable-data")).GetAttribute("outerHTML");
            urls.AddRange(ParseTBody(HtmlNode.CreateNode(tbody)));

            if (Driver.FindElements(By.CssSelector("a.reactable-next-page")) is var nextButton && nextButton.Count != 0)
                Wait.Clickable(nextButton.ElementAt(0)).Click();
            else
                break;
        }

        Driver.Navigate().GoToUrl("about:blank");

        // local func
        const string URLBase = "https://leetcode.com";
        IEnumerable<string> ParseTBody(HtmlNode tbody) => tbody.SelectNodes("tr").Select(tr => ParseTR(tr));

        string ParseTR(HtmlNode tr) =>
            URLBase + System.Net.WebUtility.HtmlDecode(
                tr.SelectSingleNode(".//a").Attributes["href"].Value
            ).Trim();
    }

    public void ParseProblems()
    {
        foreach (var url in urls.Take(5))
        {
            problems.Add(LCProblemParser.Parse(url, GetProblemDescription(url)));
        }

        // local func
        string GetProblemDescription(string url)
        {
            Driver.Navigate().GoToUrl(url);
            var query = Wait.Exists(By.CssSelector("div[data-key='description-content']"));
            return query.GetAttribute("innerHTML");
            // return Wait.Content(() => query.GetAttribute("innerHTML")); // Not sure whether needs waiting
        }
    }

    void Save()
    {
        File.WriteAllLines("LCUrl.txt", urls.Select(u => u));
        using (var sw = new StreamWriter("LCProblems.csv"))
        {
            sw.WriteLine("No, Title, Difficulty, ACRate, LikeRate, URL,");
            problems.ForEach(p => sw.WriteLine(p.ToString()));
        }
    }
}
