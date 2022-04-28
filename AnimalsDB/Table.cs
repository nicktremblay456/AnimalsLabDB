namespace AnimalsDB
{
    public static class Table
    {
        public static void PrintLine(int tableWidth)
        {
            Console.WriteLine(new String('═', tableWidth));
        }

        public static void PrintRow(int tableWidth, params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "║";

            foreach (string column in columns)
            {
                Thread.Sleep(50);
                row += AlignCenter(column, width) + "║";
            }

            Console.WriteLine(row);
        }

        public static string AlignCenter(string txt, int width)
        {
            txt = txt.Length > width ? txt.Substring(0, width - 3) + "..." : txt;

            if (string.IsNullOrEmpty(txt)) return new string(' ', width);
            else return txt.PadRight(width - (width - txt.Length) / 2).PadLeft(width);
        }

        public static void PrintOptions()
        {
            string[] optionsStr = new string[]
            {
                "╔══════════════════════════════════════════════════════╗\n",
                "║                  ¤ Options ¤                         ║\n",
                "║                                                      ║\n",
                "║    1- Add animal                                     ║\n",
                "║    2- List of all animals                            ║\n",
                "║    3- List of all owners                             ║\n",
                "║    4- See the total amount of animals                ║\n",
                "║    5- See the total weigth of all animals            ║\n",
                "║    6- List of all animals with the selected color    ║\n",
                "║    7- Remove an animal from the list                 ║\n",
                "║    8- Modify an animal name from the list            ║\n",
                "║    9- Quit                                           ║\n",
                "║                                                      ║\n",
                "╚══════════════════════════════════════════════════════╝\n"
            };
            foreach (string option in optionsStr)
            {
                Thread.Sleep(50);
                Console.Write(string.Format("{0," + ((Console.WindowWidth / 2) + (option.Length / 2)) + "}", option));
            }
        }
    }
}