using BookApp.Services;
using BookApp.Api.Interfaces;
using BookApp.Core.Interfaces;
using BookApp.Api.Models;
using BookApp.Core.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var menu = new MenuService();
        menu.RunMenuAsync().GetAwaiter().GetResult();
    }
}