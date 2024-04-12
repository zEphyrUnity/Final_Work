using Final_Work.Controller;
using Final_Work.View;

namespace Final_Work.Model.Menu;

public class Commands
{
    private string? commandName;
    private AnimalController _controller;
    private string? animalName;
    
    public bool EnterName()
    {
        Console.WriteLine("Введите имя животного:");
        animalName = Console.ReadLine();
        if (string.IsNullOrEmpty(animalName))
        {
            Console.WriteLine("Имя животного не может быть пустым.");
            return false;
        }
        
        return true;
    }
    
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

    public void PerformCommand(Database db)
    {
        db.OpenConnection();
        string query = $"SELECT Commands.CommandValue FROM Commands INNER JOIN Animals ON " +
                       $"Commands.AnimalID = Animals.AnimalID WHERE Animals.Name = '{animalName}' " +
                       $"AND Commands.CommandName = '{commandName}'";
        string perform = db.ExecuteScalarString(query);
        db.CloseConnection();
        
        if (String.IsNullOrEmpty(perform))
        {
            Console.WriteLine($"Нет такого животного: {animalName} ");
        }
        else
        {
            Console.WriteLine($"Выполняю: {perform}"); 
        } 
    }
    
    /*public void Execute(string animalName, Dictionary<string, IAnimal> animals, ConsoleView view)
    {
        _controller = new AnimalController(animals[animalName], view);
        _controller.ExecuteCommand(commandName);
    }

    public void ShowCommand(string animalName, Dictionary<string, IAnimal> animals, ConsoleView view)
    {
        _controller = new AnimalController(animals[animalName], view);
        _controller.ShowCommands();
    }*/
}