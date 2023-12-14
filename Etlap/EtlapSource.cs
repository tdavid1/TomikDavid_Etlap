using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
    
    public class EtlapSource
    {
        MySqlConnection connection;
        public EtlapSource()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "etlapdb";

            connection = new MySqlConnection(builder.ConnectionString);
        }
        public List<Etel> GetAllTable()
        {
            List<Etel> etelek = new List<Etel>();
            OpenConnection();
            string sql = "SELECT * FROM etlap";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Etel etel = new Etel();
                    etel.Id = reader.GetInt32("id");
                    etel.Nev = reader.GetString("nev");
                    etel.Leiras = reader.GetString("leiras");
                    etel.Ar = reader.GetInt32("ar");
                    etel.Kategoria = reader.GetString("kategoria");
                    etelek.Add(etel);
                }
            }
            CloseConnection();
            return etelek;
        }
        public bool Create(Etel etel)
        {
            OpenConnection();
            string sql = "INSERT INTO etlap(nev,leiras,ar,kategoria) VALUES (@nev,@leiras,@ar,@kategoria)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@nev", etel.Nev);
            command.Parameters.AddWithValue("@leiras", etel.Leiras);
            command.Parameters.AddWithValue("@ar", etel.Ar);
            command.Parameters.AddWithValue("@kategoria", etel.Kategoria);

            int affectedRows = command.ExecuteNonQuery();

            CloseConnection();
            return affectedRows == 1;
        }
        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM etlap WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }
        public bool Aremeles(int id, double ujar)
        {
            OpenConnection();
            string sql = @"UPDATE etlap 
                            SET ar = @ar 
                            WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@ar", ujar);
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }
        public bool Aremeles(int id, int ujar)
        {
            OpenConnection();
            string sql = @"UPDATE etlap 
                            SET ar = @ar 
                            WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@ar", ujar);
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }

        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
