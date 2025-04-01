namespace WordReferenceCLI;

using System.Reflection;
using System.Diagnostics;
using Figgle;
/// <summary>
/// Contains all the commands that can be run.
/// </summary>
public class Commands
{
    /// <summary>
    /// Prints all the commands
    /// </summary>

    private const string UNDERLINE = "\x1B[4m";
    private const string RESET = "\x1B[0m";
    public static void Help()
    {
        Console.WriteLine(
        FiggleFonts.Slant.Render("Word Reference CLI")
        );
        Console.WriteLine("WordReferenceCLI is a CLI for a webscraper that gets the translations for words off of https://wordreference.com.");
        Console.WriteLine("usage: <?options> [from][to] [word]");
        Console.WriteLine("Ex: wordreferencecli fren examen");
        Console.WriteLine("\n");
        Console.WriteLine(UNDERLINE + "Commands:" + RESET);
        Console.WriteLine("        --version, -v            prints the current version");// 12 spaces, 3 groups of 4 spaces. 8 spaces indented
    }
    public static void Version()
    {
        Console.WriteLine("0.0.1");
    }
    public static void NotFound()
    {
        Console.WriteLine("Invalid Query.");
    }

}