using System.Data;

namespace Final_Work.Model.Menu;

public class ShowAnimals
{
    public void ShowTable(Database db)
    {
        db.OpenConnection();

        string query = "SELECT * FROM Animals";
        
        DataTable dataTable = db.ExecuteQuery(query);
        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine($"ID: {row["AnimalID"]}, Name: {row["Name"]}, Age: {row["Age"]}, BirthDate: {row["BirthDate"]}, Type: {row["Type"]}");
        }
        
        db.CloseConnection();
    }

    public void ShowTableDapper(Database db)
    {
        var result = db.Query("SELECT * FROM Animals").ToList();

        if (result.Any())
        {
            var firstRow = result.First() as IDictionary<string, object>;

            if (firstRow != null)
            {
                foreach (var column in firstRow.Keys)
                {
                    Console.Write($"{column.PadRight(20)}\t");
                }
                Console.WriteLine();
            }

            foreach (var row in result)
            {
                var dictRow = row as IDictionary<string, object>;
                if (dictRow != null)
                {
                    foreach (var value in dictRow.Values)
                    {
                        Console.Write($"{value.ToString().PadRight(20) ?? "NULL"}\t");
                    }
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("No rows returned from the query.");
        }
    }
}