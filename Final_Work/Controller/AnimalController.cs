using Final_Work.View;

namespace Final_Work.Controller;

public class AnimalController
{
    private IAnimal _animal;
    private ConsoleView _view;

    public AnimalController(IAnimal animal, ConsoleView view)
    {
        _animal = animal;
        _view = view;
    }

    public void TeachCommand(string commandName, string commandOutput)
    {
        _animal.LearnCommand(commandName, new CustomCommand(commandOutput));
    }

    public void ExecuteCommand(string commandName)
    {
        _animal.PerformCommand(commandName);
    }

    public void ShowCommands()
    {
        var commands = _animal.GetCommands();
        foreach (var command in commands)
        {
            _view.DisplayCommand(command);
        }
    }
}