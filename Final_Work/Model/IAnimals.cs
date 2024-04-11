using System.Data;

namespace Final_Work;

public interface IAnimal
{
    public int Age { get; set; }
    public string Name { get; set; }
    
    void LearnCommand(string commandName, ICommand command);
    void PerformCommand(string commandName);
    List<string> GetCommands();
    void SaveAnimalToDatabase(Database db);
    void SaveCommandToDatabase(Database db, string commandName, string commandValue);
}
