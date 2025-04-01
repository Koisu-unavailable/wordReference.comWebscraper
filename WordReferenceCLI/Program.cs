namespace WordReferenceCLI;

public class Program{
    public static int Main(string[] args)
    {
        if (!args.Any()){
            Commands.Help();
            return 0;
        }
        if (args.Length > 3 ){
            Console.WriteLine("Too many arguments there should only never be more than 3.");
            Commands.Help();
            return 1;
        }
        if (args[0] == "-h" && args.Length == 1){
            Commands.Help();
            return 0;
        }
        if ((args[0] == "--version" && args.Length == 1) || (args[0] == "-v" && args.Length == 1)){
            Commands.Version();
            return 0;
        }
        Commands.NotFound();
        return 0;
    }
}