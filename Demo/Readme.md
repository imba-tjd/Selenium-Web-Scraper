# Demo

## How to use

Modify [Program.cs](../Program.cs):

```c#
using Demo;
...
var app = new GoogleDemo();
app.Run();
```

## GoogleDemo

来自于[官方教程][1]，未做更改。\
From [official documentation][1] without any change.

## BaiduDemo

根据Google的模仿了一下，区别是百度的搜索框用的是Id唯一标识的。很容易。\
Imitate from Google demo. The difference is that Baidu uses Id to identify search box. Easy to write.

## FirstChildDemo

HTML允许在标签中间写东西。对于`html1`，`div`的第一个子节点是个纯文本节点，内容是 `\r\n` 和空格；而`html2`的第一个子节点是`p`。这非常不好，所以我的实际代码中改用了xpath获取节点。\
HTML allows you to write something between tags. For `html1`, the first child of `div` is a plain text node, that is `\r\n` and spaces, while for `html2` is `p`. This is pretty bad, so I chose to use xpath in my real code.

## WaitDemo

如果找不到元素，`FindElement` 会抛 `NoSuchElementException` 异常，而不是返回null。官方说这是常规操作，且符合标准。\
`FindElement` would throw `NoSuchElementException` exception if the specific element doesn't exist, rather than return null. The collaborator said it's by designed and is standard compliant.

### `Run()`

`Run()` 演示了 `FindElement` 在使用了 `WebDriverWait` 和 `IgnoreExceptionTypes` 后仍然会抛异常。我不知道这是不是bug，但是网上许多教程，甚至[官方教程][2]都是这样做的。\
`Run()` shows that `FindElement` will still throw the exception even though using `WebDriverWait` and `IgnoreExceptionTypes`. I wonder whether this is a bug, because many tutorial as well as the [official tutorial] do the same way.

### `Run2()`

`Run2()` 演示了一种不使用 `ExpectedConditions` 的等待方法，但即使不加 `ImplicitWait` 使得默认的隐式等待时间为0，也只会输出很少的几次“waiting...”，因为每次执行 `FindElements` 都会消耗2秒左右的时间。\
`Run2()` demonstrate a way to wait without `ExpectedConditions`. But even not using `ImplicitWait` so that the default implicit waiting time being 0, you would still see only few "waiting...". This is because each time calling `FindElements` consumes about 2 seconds.

### `Run3()`

`Run3()` 演示了隐式等待并不是在每次执行查询前都等待指定的时间：如果找到指定元素，**立即返回**；否则等待指定时间后抛异常（但不是超时异常）。\
`Run3()` shows that implicit waiting isn't waiting for a const time each time querying: return immediately if successfully finding the element, or wait for a certain time and then throw an exception (not timeout exception).

### Correct approach

用 `SeleniumExtras.WaitHelpers.ExpectedConditions` 是可以的，具体参见[MyWait.cs](../MyWait.cs)。网上也有一些人提到了这个方法，但对于dotnet，它从 `OpenQA.Selenium.Support.UI` 分离到了 `DotNetSeleniumExtras.WaitHelpers` 这个包中，而且看起来官方不会再维护了。\
`SeleniumExtras.WaitHelpers.ExpectedConditions` works, see [MyWait.cs](../MyWait.cs). There are people mentioned this function, but for dotnet it split from `OpenQA.Selenium.Support.UI` to another nuget package `DotNetSeleniumExtras.WaitHelpers` which seems not maintained anymore.

另外不要写成 `wait.Until(d => ExpectedConditions ...)`，这样的代码不会报错但实际无法生效。\
Note not `wait.Until(d => ExpectedConditions ...)`, this won't work.

### explicit and implicit waiting comparison

理论上隐式等待是足够我这个爬虫使用了的，但许多文章不建议使用隐式等待。\
Theoretically implicit waiting is enough for my project, but many articles suggest not using it.

显式等待可以每次设定自己想要的超时时间，比如希望找不到某个元素时立即失败，如果不修改隐式等待的时间，就会一直等到超时才会报告失败。\
Explicit waiting can set timeout every time it's used. For example you want a immediate failure when not finding a specific element, you must change the implicit timeout value otherwise it would wait until timeout.

另外隐式等待好像只会影响选取元素的超时时间，显式等待的 `ExpectedConditions` 可以等待元素可点击等条件。\
Besides implicit waiting seems to only affect the time that finding element, while explicit waiting's `ExpectedConditions` can wait for elements to be clickable.

还有就是不要混用显式等待和隐式等待。\
What's more, don't mix up explicit waiting with implicit waiting.

### 总结/Summary

不要使用 `PageLoadStrategy.Eager` 。\
All in all, it's best not using `PageLoadStrategy.Eager`.

[1]: https://www.seleniumhq.org/docs/03_webdriver.jsp#introducing-the-selenium-webdriver-api-by-example
[2]: https://www.seleniumhq.org/docs/04_webdriver_advanced.jsp#explicit-waits
