namespace AnimalsDB
{
    public class Animal
    {
        public int animalID = 0;
        public int ownerID = 0;
        public string type = string.Empty;
        public string name = string.Empty;
        public int age = 0;
        public int weigth = 0;
        public string color = string.Empty;

        public Animal() { }
        public Animal(int animalID, int ownerID, string type, string name, int age, int weigth, string color)
        {
            this.animalID = animalID;
            this.ownerID = ownerID;
            this.type = type;
            this.name = name;
            this.age = age;
            this.weigth = weigth;
            this.color = color;
        }
    }
}
