namespace Final_Work.Model;

public class PackAnimal : IAnimal
{
    public int Age { get; set; }
    public string Name { get; set; }
    private int AnimalId { get; set; }

    
    private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

    public void LearnCommand(string commandName, ICommand command)
    {
        _commands[commandName] = command;
    }

    public void PerformCommand(string commandName)
    {
        if (_commands.ContainsKey(commandName))
        {
            _commands[commandName].Execute();
        }
    }

    public List<string> GetCommands()
    {
        return _commands.Keys.ToList();
    }
    
    public void SaveAnimalToDatabase(Database db)
    {
        string query = $"INSERT INTO DomesticAnimals (Name, Age) VALUES ('{Name}', {Age}); SELECT LAST_INSERT_ID();";
        AnimalId = db.ExecuteScalar(query);
    }
    
    public void SaveCommandToDatabase(Database db, string commandName, string commandValue)
    {
        string query = $"INSERT INTO Commands (AnimalID, AnimalType, CommandName, CommandValue) VALUES ({AnimalId}, 'Pack', '{commandName}', '{commandValue}')";
        db.ExecuteNonQuery(query);
    }
}