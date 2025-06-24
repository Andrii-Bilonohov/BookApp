using BookApp.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var menu = new MenuService();
        menu.RunMenuAsync().GetAwaiter().GetResult();
    }
}