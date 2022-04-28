using MySql.Data.MySqlClient;

namespace AnimalsDB
{ 
    public class Refuge
    {
        private SQL_Server m_Server;
        private List<dynamic> m_Animals;
        private List<dynamic> m_Owners;

        public Refuge()
        {
            m_Server = new SQL_Server();

            // Get data from SQL
            m_Owners = m_Server.GetData(ETable.Owners);
            m_Animals = m_Server.GetData(ETable.Animals);
        }

        public void Run()
        {
            Options();
        }

        private void Options()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            int input = 0;
            string[] optionsStr = new string[]
            {
                "╔═════════════════════════════════════════════════════════════════╗\n",
                "║                         ¤ Options ¤                             ║\n",
                "║                                                                 ║\n",
                "║    1- Add animal                                                ║\n",
                "║    2- List of all animals                                       ║\n",
                "║    3- List of all owners                                        ║\n",
                "║    4- See the total amount of animals                           ║\n",
                "║    5- See the total weigth of all animals                       ║\n",
                "║    6- List of all animals with the color (Red, Blue and Purple) ║\n",
                "║    7- Remove an animal from the list                            ║\n",
                "║    8- Modify an animal name from the list                       ║\n",
                "║    9- Quit                                                      ║\n",
                "║                                                                 ║\n",
                "╚═════════════════════════════════════════════════════════════════╝\n"
            };
            foreach (string option in optionsStr)
            {
                Console.Write(string.Format("{0," + ((Console.WindowWidth / 2) + (option.Length / 2)) + "}", option));
            }

            string txt = "Select an option: ";
            do { GetInput(ref input, string.Format("{0," + ((Console.WindowWidth / 2) + (txt.Length / 2)) + "}", txt)); }
            while (input <= 0 || input > 9);

            switch (input)
            {
                case 1: AddAnimal(); break;
                case 2: GetAnimalsList(); break;
                case 3: GetOwnersList(); break;
                case 4: GetAnimalsAmount(); break;
                case 5: GetTotalWeigth(); break;
                case 6: GetAnimalByColor(); break;
                case 7: RemoveAnimal(); break;
                case 8: ModifyAnimalName(); break;
                case 9: Console.Clear(); break;
            }
        }

        private void GetInput(ref int input, string txt)
        {
            Console.Write(txt);
            try { input = int.Parse(Console.ReadLine()); }
            catch { input = -1; }// Just a random number to make sure it will be refused, to prompt user again.
        }

        private void GetInput(ref string input, string txt)
        {
            Console.Write(txt);
            input = Console.ReadLine();
            if (input != null)
                input = FirstCharToUpper(ref input);
        }

        private bool IsValidID(int id)
        {
            foreach(Animal animal in m_Animals)
            {
                if (id == animal.animalID) 
                    return true;
            }

            return false;
        }

        public string FirstCharToUpper(ref string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        private void AddAnimal()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            Animal animal = new Animal();
            Owner owner = new Owner();
            Console.Clear();

            do
            {
                GetInput(ref animal.type, "What type of animal is it? Dog, Cat or Fish? : ");
                Console.Clear();
            } while (animal.type.ToLower() != "dog" && animal.type.ToLower() != "cat" && animal.type.ToLower() != "fish");
            do
            {
                GetInput(ref animal.name, "Enter the name: ");
                Console.Clear();
            } while (animal.name == string.Empty);
            do
            {
                GetInput(ref animal.age, "Enter the age: ");
                Console.Clear();
            } while (animal.age < 0);
            do
            {
                GetInput(ref animal.weigth, "Enter the weigth: ");
                Console.Clear();
            } while (animal.weigth <= 0);
            do
            {
                GetInput(ref animal.color, "Enter the color: ");
                Console.Clear();
            } while (animal.color == string.Empty);
            do
            {
                GetInput(ref owner.ownerName, "Enter owner name: ");
                Console.Clear();
            } while (owner.ownerName == string.Empty);

            // Get the correct id to keep m_Animals and m_Owners values updated
            animal.ownerID = m_Server.GetMaxID(ETable.Owners) + 1;
            animal.animalID = m_Server.GetMaxID(ETable.Animals) + 1;
            
            // Add the owner to the db
            m_Server.SQL_Query("INSERT INTO owners (OwnerName)" +
                            $"VALUES ('{owner.ownerName}')");
            // Add the animal to the db
            m_Server.SQL_Query("INSERT INTO animals (OwnerID, AnimalType, AnimalName, Age, Weigth, Color)" +
                            $"VALUES ('{animal.ownerID}', '{animal.type}', '{animal.name}', '{animal.age}', '{animal.weigth}', '{animal.color}');");

            // Added to a list to keep track on data without making query every time we add something
            m_Animals.Add(animal);
            m_Owners.Add(owner);

            Options();
        }

        private void GetAnimalsList()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Clear();
            int input = 0;

            Table.PrintRow(73, "ID", "Type", "Name", "Age", "Weigth", "Color", "Owner");
            for (int i = 0, j = 0; i < m_Animals.Count && j < m_Owners.Count; i++, j++)
            {
                Table.PrintLine(73);
                Table.PrintRow(73, $"{m_Animals[i].animalID}", $"{m_Animals[i].type}", $"{m_Animals[i].name}", $"{m_Animals[i].age}", $"{m_Animals[i].weigth}", 
                                   $"{m_Animals[i].color}", $"{m_Owners[j].ownerName}");
            }

            do { GetInput(ref input, "Enter 1 to return to the main menu: "); }
            while (input != 1);

            Console.Clear();
            Options();
        }

        private void GetOwnersList()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Clear();
            int input = 0;

            Table.PrintRow(10, "Owner");
            foreach(Owner owner in m_Owners)
            {
                Table.PrintLine(10);
                Table.PrintRow(10, $"{owner.ownerName}");
            }

            do { GetInput(ref input, "Enter 1 to return to the main menu: "); }
            while (input != 1);

            Console.Clear();
            Options();
        }

        private void GetAnimalsAmount()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;

            Console.Clear();
            int input = 0;

            Table.PrintRow(10, "Amount");
            Table.PrintLine(10);
            Table.PrintRow(10, $"{m_Animals.Count}");

            do { GetInput(ref input, "Enter 1 to return to the main menu: "); }
            while (input != 1);

            Console.Clear();
            Options();
        }

        private void GetTotalWeigth()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;

            Console.Clear();
            int input = 0;

            int weigth = 0;
            if (m_Animals.Count > 0)
            {
                foreach(Animal animal in m_Animals)
                {
                    weigth += animal.weigth;
                }
            }

            Table.PrintRow(15, "Total Weigth");
            Table.PrintLine(15);
            Table.PrintRow(15, $"{weigth}");

            do { GetInput(ref input, "Enter 1 to return to the main menu: "); }
            while (input != 1);

            Console.Clear();
            Options();
        }

        private void GetAnimalByColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;

            Console.Clear();
            int input = 0;

            string color = string.Empty;
            Console.WriteLine("Existing Color (don't forget Maj on first letter) -> White, Black, Beige, Grey, Blond, Red, Blue, Green, Purple");
            do { GetInput(ref color, "Enter a color to find: "); }
            while (color == string.Empty);

            Table.PrintRow(65, "ID", "Type", "Name", "Age", "Weigth", "Color", "Owner");
            for (int i = 0, j = 0; i < m_Animals.Count && j < m_Owners.Count; i++, j++)
            {
                if (color == m_Animals[i].color)
                {
                    Table.PrintLine(65);
                    Table.PrintRow(65, $"{m_Animals[i].animalID}", $"{m_Animals[i].type}", $"{m_Animals[i].name}", 
                                       $"{m_Animals[i].age}", $"{m_Animals[i].weigth}", $"{m_Animals[i].color}", $"{m_Owners[j].ownerName}");
                }
            }

            do { GetInput(ref input, "Enter 1 to return to the main menu: "); }
            while (input != 1);

            Console.Clear();
            Options();
        }

        private void RemoveAnimal()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.Clear();

            int index = 0;
            int inputId = 0;
            Console.WriteLine("Enter -1 to return to the main menu");
            do { GetInput(ref inputId, "Enter animal id to remove: "); }
            while (!IsValidID(inputId));

            if (inputId != 0)
            {
                for (int i = 0; i < m_Animals.Count; i++)
                {
                    if (inputId == m_Animals[i].animalID)
                    {
                        index = i;
                        break;
                    }
                }
                m_Server.SQL_Query($"DELETE FROM animals WHERE AnimalID = '{inputId}'");
                m_Animals.RemoveAt(index);
            }

            Console.Clear();
            Options();
        }

        private void ModifyAnimalName()
        {
            Console.Clear();
            
            int index = 0;
            int inputId = 0;
            do { GetInput(ref inputId, "Enter animal id to modify: "); }
            while (!IsValidID(inputId));

            string inputName = string.Empty;
            do { GetInput(ref inputName, "Enter the new name: "); }
            while (inputName == string.Empty);

            for (int i = 0; i < m_Animals.Count; i++)
            {
                if (inputId == m_Animals[i].animalID)
                {
                    index = i;
                    break;
                }
            }

            m_Server.SQL_Query($"UPDATE animals SET AnimalName = '{inputName}' WHERE AnimalID = '{inputId}'");
            m_Animals[index].name = inputName;

            Console.Clear();
            Options();
        }
    }
}