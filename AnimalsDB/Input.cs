namespace AnimalsDB
{
    public static class Input
    {
        public static void GetInput(ref int input, string txt)
        {
            Console.Write(txt);
            try { input = int.Parse(Console.ReadLine()); }
            catch { input = -1; }// Just a random number to make sure it will be refused, to prompt user again.
        }

        public static void GetInput(ref string input, string txt)
        {
            Console.Write(txt);
            input = Console.ReadLine();
            if (input != null)
                input = FirstCharToUpper(ref input);
        }

        private static string FirstCharToUpper(ref string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}