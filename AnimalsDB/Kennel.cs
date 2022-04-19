using MySql.Data.MySqlClient;

namespace AnimalsDB
{
    public class Kennel
    {
        public void Options()
        {
            byte input = byte.MinValue;

            Console.WriteLine("1- Add animal\n" +
                              "2- List of all animals\n" +
                              "3- List of all owners\n" +
                              "4- See the total amount of animals\n" +
                              "5- See the total weigth of all animals\n" +
                              "6- List of all animals with the color (Red, Blue and Purple)\n" +
                              "7- Remove an animal from the list\n" +
                              "8- Modify an animal from the list (name, age, weigth, color and owner name)\n" +
                              "9- Quit");

            do { GetInput(ref input, "Select an option: "); }
            while (input == 0 || input > 9);

            switch (input)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    break;
            }
        }

        private void GetInput(ref byte input, string txt)
        {
            Console.Write(txt);
            try { input = byte.Parse(Console.ReadLine()); }
            catch { input = 99; }// Just a random number to make sure it will be refused, to prompt user again.
        }
    }
}