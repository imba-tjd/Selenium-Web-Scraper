# LeetCode Selenium 动态爬虫 / LeetCode Selenium Dynamic Web Scraper

[![Build Status](https://travis-ci.com/imba-tjd/Selenium-Web-Scraper.svg?branch=master)](https://travis-ci.com/imba-tjd/Selenium-Web-Scraper)

有时候网页内容是JS动态生成的，直接用http请求无法获取到。此时可以用Selenium控制浏览器加载网页，获取加载完后的DOM。\
Sometimes the web page are produced dynamically by JS, which can't be gotten only by https protocol. At this time you can use Selenium to control browser to load web page, and get the loaded DOM.

Selenium是一个自动测试框架，有许多功能，我们只需要用到极小的一部分。\
Selenium is a automatic web-testing framework with many features. We only need a few of them.

这个项目是演示了如何用C#和Selenium获取LeetCode上的problem，仅供练习Selenium使用。\
This project demonstrated how to use C# and Selenium to get the problems on LeetCode. **Only for learning Selenium**.

[Experience.md](./Experience.md)是我写这个项目中总结的一些经验。\
[Experience.md](./Experience.md) contains my thoughts when coding this project.

## Pre-requests

* [.Net Core SDK](https://dot.net)
* [Mozilla Firefox](https://www.firefox.com/)
* [geckodriver](https://github.com/mozilla/geckodriver/releases) **in path**.

## Build & Run

```bash
dotnet run
```

会自动打开FF，自动点击下一页。此时可以用鼠标点到21页加速一下获取过程。没有下一页按钮后自动打开具体的问题页面，获取内容后继续访问下一个。\
It will open FF automatically. You can click the next button to page 21 to accelerate. It will open specific problem page when the next button disappear, and get the content and access next one.

演示中只会获取5个问题的数据，可以在 [LCScraper.cs](LCScraper.cs) 的 `ParseProblems` 中改。\
The demo will only get 5 problems' data. You can change it in [LCScraper.cs](LCScraper.cs)'s `ParseProblems`.

完成后会把数据保存到 `LCurl.txt` 和 `LCProblems.csv` 中。\
It will save the data to `LCurl.txt` and `LCProblems.csv`.

另外，有的题目需要会员才能查看，我的代码中没有进行相关的处理，会导致 `GetProblemDescription` 的获取元素失败。\
There are problems that need LeetCode premium to see. My code didn't handle this. It will fail in `GetProblemDescription` when upcoming this.

## 项目结构/Project Structure Graphic

```graph
                                   -- LoadProblems()
                                   |
Program -- FFFactory -- LCScrapper -- ParseProblems() -- LCProblemParser -- LCProblem
        |                          |
        |                          -- Save()
        -- Demo
```

* Program：入口点 / Entry point
* LCProblem：数据类。有题目的编号、难度等属性 / Data class with problems' No, difficulty, etc.
* LCProblemParser：静态类。从 `div class="description..` 的字符串中提取各个属性并生成 `LCProblem` 对象 / Static class. Get problems' properties from `div class="description..` string and return `LCProblem` object.
* FFFactory：创建火狐实例，生成 `IWebDriver` 依赖 / Create instance of FireFox, which implements `IWebDriver`
* MyWait：`WebDriverWait` 和 `ExpectedConditions` 的包装类 / Wrapper of `WebDriverWait` and `ExpectedConditions`
* LCScraper：主类。`LoadProblems()` 获取所有问题的URL，`ParseProblems()` 获取问题内容，生成问题对象，`Save()` 保存数据 / Core class. `LoadProblems()` gets problems' URL, `ParseProblems()` gets problems' content, `Save()` saves the data.
* Demo：一些测试，与程序主体无关，具体见[Demo](./Demo) / Some important tests while which doesn't directly related to core. See [Demo](./Demo) for details.
