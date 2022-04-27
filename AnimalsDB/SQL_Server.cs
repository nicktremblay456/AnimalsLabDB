using MySql.Data.MySqlClient;

namespace AnimalsDB
{
    public enum ETable
    {
        Animals,
        Owners
    }

    public class SQL_Server
    {
        private string connectionString = "server=localhost;database=animalsdb;uid=root;pwd=;";

        public void SQL_Query(string query)
        {
            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot open connection {ex.Message}");
                    return;
                }

                MySqlCommand cmd = new MySqlCommand(query, cnn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();
            }
        }

        public int GetMaxID(ETable table)
        {
            string query = string.Empty;
            int id = 0;

            switch(table)
            {
                case ETable.Animals: query = "SELECT MAX(AnimalID) FROM animals"; break;
                case ETable.Owners: query = "SELECT MAX(OwnerID) FROM owners"; break;
            }

            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot open connection, {ex.Message}");
                    return id;
                }

                MySqlCommand cmd = new MySqlCommand(query, cnn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    switch(table)
                    {
                        case ETable.Animals: id = (int)dataReader["MAX(AnimalID)"]; break;
                        case ETable.Owners: id = (int)dataReader["MAX(OwnerID)"]; break;
                    }
                }
            }

            return id;
        }

        public List<dynamic> GetData(ETable table)
        {
            List<dynamic> datas = new List<dynamic>();
            string query = string.Empty;

            switch(table)
            {
                case ETable.Animals: query = "SELECT * FROM animals"; break;
                case ETable.Owners: query = "SELECT * FROM owners"; break;
            }
            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot open connection, {ex.Message}");
                    return datas;
                }

                MySqlCommand cmd = new MySqlCommand(query, cnn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    switch (table)
                    {
                        case ETable.Animals:
                            datas.Add(new Animal
                            (
                                (int)dataReader["AnimalID"],
                                (int)dataReader["OwnerID"],
                                (string)dataReader["AnimalType"],
                                (string)dataReader["AnimalName"],
                                (int)dataReader["Age"],
                                (int)dataReader["Weigth"],
                                (string)dataReader["Color"]
                            ));
                            break;
                        case ETable.Owners:
                            datas.Add(new Owner
                            (
                                (int)dataReader["OwnerID"],
                                (string)dataReader["OwnerName"]
                            ));
                            break;
                    }
                }
            }

            return datas;
        }
    }
}