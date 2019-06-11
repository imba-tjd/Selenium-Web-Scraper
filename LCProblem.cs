enum LCDifficulty
{
    Easy,
    Medium,
    Hard
}

class LCProblem
{
    public int No { get; }
    public string Title { get; }
    public LCDifficulty Difficulty { get; }
    public double ACRate { get; }
    public double LikeRate { get; }
    public string URL { get; }
    public string Content { get; }

    public override string ToString() =>
        $"{No}, {Title}, {Difficulty}, {ACRate:P}, {LikeRate:P}, {URL},";

    public LCProblem(int no, string title, LCDifficulty diffi,
    double acRate, double likeRate, string url, string content) =>
        (No, Title, Difficulty, ACRate, LikeRate, URL, Content) =
        (no, title, diffi, acRate, likeRate, url, content);
}
