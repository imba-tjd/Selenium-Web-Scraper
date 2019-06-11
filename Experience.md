# 写这个项目中总结的一些经验/Some Experience when coding this project

## 关于Selenium/About Selenium

### 安装/Installation

```bash
dotnet add package Selenium.WebDriver;
dotnet add package DotNetSeleniumExtras.WaitHelpers;
```

下载各个浏览器的WebDriver并加到Path里：<https://seleniumhq.github.io/docs/wd.html#quick_reference>\
Download different WebDriver for different browser and add it to Path.

### 使用/Usage

* 和浏览器交互一次至少需要花2秒，打开空白页后直接关闭需要15秒；所以 `FindElement` 的条件是否唯一并不占主要因素\
  A single interaction will cost at least 2s. Open about:blank and then close it will cost about 15s. So it's not very important to write a precise condition in `FindElement`.
* 调试困难：打了断点后watching里加表达式无法获取到值，因为那些函数是和浏览器动态交互的，挂起时无法使用\
  Hard to debug: the watching window won't work during break point time, because those function need to interact with browser dynamically.
* 获取完整DOM：`driver.PageSource`\
  Use `driver.PageSource` to get the DOM.

### 等待加载/Waiting to be loaded

即使浏览器显示网页加载完成，真正想要的数据还是有可能需要加载，因为这些请求可能是动态的、异步的。\
Even though the browser shows the status is complete, the real data you want might still in loading, beacuse these requests might be dynamic and asynchronous.

而且根据其他人的讨论，没有一种完美的方法确定是否加载完成，只能设定超时后等待。\
According to other people's discussion, there isn't a perfect way to distinguish whether the page is full loaded. You can only set the timeout and wait.

但Selenium的等待可能非常坑，具体见[Demo](./Demo)的测试。\
But the waiting in Selenium is hard to use. See the tests in [Demo](./Demo).

### 等待可被点击/Waiting to be clickable

元素生成后，可能会被JS占用，或者未显示，导致交互时会发生异常：\
An element could be invisible after creation, thus making interaction cause error.

```output
An unhandled exception of type 'OpenQA.Selenium.ElementClickInterceptedException' occurred in WebDriver.dll:
'Element <a class="reactable-next-page" href="#page-2"> is not clickable at point (892,720) because another element <aside id="region_switcher" class=""> obscures it'
```

解决办法是调用 `ExpectedConditions.ElementToBeClickable`。但注意如果指定元素不存在，会一直等待直到超时。\
The solution is to use `ExpectedConditions.ElementToBeClickable`. Note it will wait until time out if the specific element really not exits.

### 多线程/Multi-threading

Selenium并不区分tab和window，且没有直接创建tab的方法。但可以用JS创建：`driver.ExecuteScript("window.open('your url','_blank');");`\
Selenium doesn't distinguish tab with window, and it doesn't provide a "CreateTab" method. You can use JS code to create.

`driver.WindowHandles` 是所有的tab的集合，`driver.CurrentWindowHandle` 标识当前tab。但如果要做成事件处理模式，还是要手动管理tab。\
`driver.WindowHandles` is the collecion of tabs, and `driver.CurrentWindowHandle` identify current tab. But if you want to use event model, you still need to manage tabs manually.

最大的问题是 `Navigate().GoToUrl` 默认会等待页面完全加载。而如果用Eager模式，这时就需要：找不到元素时立即做下一件事，不需要也不能等待否则多tab就没意义了。然而 `FindElement` 找不到元素时会抛异常，消耗巨大，更别提交互一次就要花2秒了。\
The biggest problem is that `Navigate().GoToUrl` will wait until full load. If you use Eager mode, it need to do next thing immediately when not finding elements, otherwise multi-tab is meaningless. However `FindElement` will throw an exception when not finding, which costs highly. Let alone a single interaction will cost more than 2s.

所以多tab要不得。多线程就只能多driver。但这样又会大量消耗内存，且我的代码需要改很多。\
So it's not possible to use multi-tabs. Multi-driver is the only way to implement "multi-threading". But this will cost plenty of memory, and my code needs a lot of change.

所以我就没有做多线程了。\
So I didn't use multi-threading.

### 参考/References

* .NET API Docs: <https://seleniumhq.github.io/selenium/docs/api/dotnet/>
* New doc: <https://seleniumhq.github.io/docs/>
* Old doc: <https://www.seleniumhq.org/docs/>

### 总结/Summary

Selenium不适合用作爬虫。如果真的不会JS，可以去找有JS引擎的Headless浏览器。\
Selenium isn't suited for web scrapper. If you really don't know JS, you'd better find another headless engine that supports JS execution.

## 关于HtmlAgilityPack/About HtmlAgilityPack

* 没有CSS选择器的API，否则我也不会去用xpath了\
  Doesn't provide CSS selector API, otherwise I won't use xpath
* 有Element的API，以及有Attribute等属性。但这些文档里都没写
  There are "Element" and "Attribute" API, but they are undocumented
* 相比于[AngleSharp](https://anglesharp.github.io/)，提交更少，功能也更少，但用的人却很多。我不知道为什么\
  Compare to AngleSharp, it has less commits and less features, but more people use it. I don't know why
* `HtmlNode.Name` 指的是 *TagName*，而不是tag的*name*属性\
  `HtmlNode.Name` means  *TagName* rather than the *name* attribute of tag

## 关于xpath/About xpath

这个东西真的能“往回搜”的。比如已经查询到了某个节点，如果再调用xpath的参数没有以“.”开头，真的会从根开始搜。简直不敢相信。\
This can really "search back". For example I have already queried a node, if I use xpath without starting with a ".", it would actually search from root, which I'm totally shocked.

## 其它/Others

* <https://phantomjscloud.com/>：使用在别人服务器上运行的PhantomJs
