internal class Program
{
    private static async Task Main(string[] args)
    {
        var game = new Game();
        await game.Start();
    }
}