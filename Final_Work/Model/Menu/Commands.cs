using Final_Work.Controller;
using Final_Work.View;

namespace Final_Work.Model.Menu;

public class Commands
{
    private string? commandName;
    private AnimalController _controller;
    
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

    public void Execute(string animalName, Dictionary<string, IAnimal> animals, ConsoleView view)
    {
        _controller = new AnimalController(animals[animalName], view);
        _controller.ExecuteCommand(commandName);
    }

    public void ShowCommand(string animalName, Dictionary<string, IAnimal> animals, ConsoleView view)
    {
        _controller = new AnimalController(animals[animalName], view);
        _controller.ShowCommands();
    }
}