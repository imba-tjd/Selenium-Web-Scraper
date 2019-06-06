using static System.Console;
using HtmlAgilityPack;

namespace Demo
{
    class FirstChildDemo
    {
        string html1 =
@"<div>
    <p></p>
</div>";
        string html2 = "<div><p></p></div>";
        public void Run()
        {
            var node = HtmlNode.CreateNode(html1);
            OutputMessage(node);
            WriteLine();
            node = HtmlNode.CreateNode(html2);
            OutputMessage(node);
        }
        void OutputMessage(HtmlNode node)
        {
            var child = node.FirstChild;
            WriteLine(node.Name);
            WriteLine(child.Name);
            WriteLine(child.NodeType);
            WriteLine(child.NextSibling?.Name);
        }
    }
}
