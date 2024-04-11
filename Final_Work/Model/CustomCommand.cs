namespace Final_Work;

public class CustomCommand : ICommand
{
    private string _output;

    public CustomCommand(string output)
    {
        _output = output;
    }

    public void Execute()
    {
        Console.WriteLine(_output);
    }
}