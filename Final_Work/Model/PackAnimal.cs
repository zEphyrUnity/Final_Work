namespace Final_Work.Model;

public class PackAnimal : IAnimal
{
    public int Age { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public int AnimalId { get; set; }

    
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

    public void AddAnimal(Database db, DateTime birthDate = default)
    {
        db.OpenConnection();

        string query = $"INSERT INTO Animals (Name, Age, BirthDate, Type) VALUES ('{Name}', {Age}, '{birthDate:yyyy-MM-dd}', 'Pack'); SELECT LAST_INSERT_ID();";
        AnimalId = db.ExecuteScalar(query);

        if (AnimalId > 0)
        {
            query = $"INSERT INTO PackAnimals (AnimalID, Breed) VALUES ({AnimalId}, '{Breed}')";
            db.ExecuteNonQuery(query);
        }

        db.CloseConnection();
    }

    public void SaveCommandToDatabase(Database db, string commandName, string commandValue)
    {
        string query = $"INSERT INTO Commands (PackAnimalID, AnimalType, CommandName, CommandValue) VALUES ({AnimalId}, 'Pack', '{commandName}', '{commandValue}')";
        db.ExecuteNonQuery(query);
    }
    
    public int GetAnimalID()
    {
        return AnimalId;
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Breed: {Breed}, AnimalId: {AnimalId}";
    }
}