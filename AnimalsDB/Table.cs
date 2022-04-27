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
    }
}