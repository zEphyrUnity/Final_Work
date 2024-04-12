using System.Data;

namespace Final_Work.Model.Menu;

public class ShowAnimalCommands
{
    public void PrintAnimalCommands(Database db, string animalName)
    {
   
        db.OpenConnection();
        
        var commandTable = db.ExecuteQuery($"SELECT CommandName, CommandValue FROM Commands INNER JOIN \n" +
                                           $"Animals ON Commands.AnimalID = Animals.AnimalID WHERE Animals.Name = '{animalName}'");
        
        foreach (DataRow row in commandTable.Rows)
        {
            var commandName = row["CommandName"].ToString();
            var commandValue = row["CommandValue"].ToString();

            Console.WriteLine($"Command Name: {commandName}, Command Value: {commandValue}");
        }

        db.CloseConnection();
    }
}