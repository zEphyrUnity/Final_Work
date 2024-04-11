namespace Final_Work;

using MySql.Data.MySqlClient;
using System.Data;

public class Database
{
    private MySqlConnection connection;
    
    public Database(string connectionString="Server=localhost;Port=3306;Database=human_friends;Uid=root;Pwd=2501;")
    {
        connection = new MySqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

    public void ExecuteNonQuery(string query)
    {
        using (var command = new MySqlCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        var dataTable = new DataTable();
        using (var adapter = new MySqlDataAdapter(query, connection))
        {
            adapter.Fill(dataTable);
        }
        return dataTable;
    }
    
    public DataTable ShowTables()
    {
        return ExecuteQuery("SHOW TABLES");
    }
    
    public int ExecuteScalar(string query)
    {
        using (var command = new MySqlCommand(query, connection))
        {
            object result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}