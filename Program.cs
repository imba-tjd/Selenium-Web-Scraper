class Program
{
    static void Main()
    {
        var app = new LCScrapper(new FFFactory().GetFF());
        app.Run();
        app.Dispose();
    }
}
