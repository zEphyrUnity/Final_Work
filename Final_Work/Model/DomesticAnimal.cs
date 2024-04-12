using System.Data;

namespace Final_Work.Model.DomesticAnimals;

public class DomesticAnimal : IAnimal
{
    public int Age { get; set; }
    public string Name { get; set; }
    
    public string Breed { get; set; }
    
    public int AnimalId { get; set; }
    

    private Dictionary<string, ICommand> _commands = new();

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

        string query = $"INSERT INTO Animals (Name, Age, BirthDate, Type) VALUES ('{Name}', {Age}, '{birthDate:yyyy-MM-dd}', 'Domestic'); SELECT LAST_INSERT_ID();";
        AnimalId = db.ExecuteScalar(query);

        Console.WriteLine(Breed);
        
        if (AnimalId > 0)
        {
            query = $"INSERT INTO DomesticAnimals (AnimalID, Breed) VALUES ({AnimalId}, '{Breed}')";
            db.ExecuteNonQuery(query);
        }

        db.CloseConnection();
    }
    
    public void SaveCommandToDatabase(Database db, string commandName, string commandValue)
    {
        Console.WriteLine(AnimalId);
        string query = $"INSERT INTO Commands (AnimalID, CommandName, CommandValue) VALUES ({AnimalId}, '{commandName}', '{commandValue}')";
        db.ExecuteScalar(query);
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