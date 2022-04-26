namespace AnimalsDB
{
    public class Owner
    {
        public int ownerID = 0;
        public string ownerName = string.Empty;

        public Owner() { }
        public Owner(int ownerID, string ownerName)
        {
            this.ownerID = ownerID;
            this.ownerName = ownerName;
        }
    }
}