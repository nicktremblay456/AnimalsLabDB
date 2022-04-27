using AnimalsDB;

public class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "Animals Database";

        Refuge refuge = new Refuge();
        refuge.Run();
    }
}