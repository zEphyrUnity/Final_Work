using Final_Work.Controller;
using Final_Work.View;

namespace Final_Work.Model.Menu;

public class LearnNewCommand
{
    private string? commandText;
    private string? commandName;
    
    public bool EnterCommandName()
    {
        Console.WriteLine("Введите имя команды:");
        commandName = Console.ReadLine();
        if (string.IsNullOrEmpty(commandName))
        {
            Console.WriteLine("Имя команды не может быть пустым.");
            return false;
        }

        return true;
    }

    public bool EnterCommand()
    {
        Console.WriteLine("Введите текст команды:");
        commandText = Console.ReadLine();
        if (string.IsNullOrEmpty(commandText))
        {
            Console.WriteLine("Текст команды не может быть пустым.");
            return false;
        }

        return true;
    }

    public void TeachCommand(string animalName, Dictionary<string, IAnimal> animals, ConsoleView view, Database db)
    {
        var controller = new AnimalController(animals[animalName], view);
        controller.TeachCommand(commandName, commandText);
        
        db.OpenConnection();
        animals[animalName].SaveCommandToDatabase(db, commandName, commandText);
        db.CloseConnection();
    }
}