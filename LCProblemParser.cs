using System;
using System.Globalization;
using HtmlAgilityPack;

static class LCProblemParser
{
    static (int no, string title) ParseNoAndTitle(string text)
    {
        int splitPoint = text.IndexOf(' ');
        var no = int.Parse(text.Substring(0, splitPoint - 1)); // 去掉点
        var title = text.Substring(splitPoint + 1);
        return (no, title);
    }
    static LCDifficulty ParseDifficulty(string text) => Enum.Parse<LCDifficulty>(text);
    static double ParseLikeRate(string like, string dislike) =>
        1.0 * int.Parse(like) / (int.Parse(dislike) + int.Parse(like));
    static double ParseACRate(string ac, string total) =>
        1.0 * int.Parse(ac, NumberStyles.Number) / int.Parse(total, NumberStyles.Number);
    static string ParseContent(string text) => text.Trim();

    public static LCProblem Parse(string url, string desc)
    {
        var root = HtmlNode.CreateNode(desc); // div class="description__24sA"

        var titleQuery = root.SelectSingleNode("//div[@id='question-title']"); // div id="question-title"
        var (no, title) = ParseNoAndTitle(titleQuery.InnerText);
        // Console.WriteLine(LCP.No);
        // Console.WriteLine(LCP.Title);

        var diffiQuery = root.SelectSingleNode("//div[@diff]"); // div diff
        var diffi = ParseDifficulty(diffiQuery.InnerText);
        // Console.WriteLine(LCP.Difficulty);

        var buttons = root.SelectNodes("//button"); // button class="btn__r7r7
        var like = buttons[0].SelectSingleNode("span");
        var dislike = buttons[1].SelectSingleNode("span");
        var likeRate = ParseLikeRate(like.InnerText, dislike.InnerText);
        // Console.WriteLine(LCP.LikeRate);

        var contentQuery = root.SelectSingleNode("div[starts-with(@class,'content')]"); // div class="content
        var content = ParseContent(contentQuery.SelectSingleNode("div").InnerHtml);
        // Console.WriteLine(LCP.Content);

        var actext = root.SelectSingleNode("//div[@style='position: relative;']/div"); // div style="position>div>div
        var ac = actext.SelectSingleNode("div[1]/div[2]");
        var total = actext.SelectSingleNode("div[2]/div[2]");
        var acRate = ParseACRate(ac.InnerText, total.InnerText);
        // Console.WriteLine(LCP.ACRate);

        return new LCProblem(no, title, diffi, acRate, likeRate, url.Trim(), content);
    }
}
