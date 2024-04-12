using Final_Work.Model.Menu;
using Final_Work.View;

namespace Final_Work.Controller;

class Program
{
    static void Main(string[] args)
    {
        var animals = new Dictionary<string, IAnimal>();
        var view = new ConsoleView();
        var counter = new Counter();
        
        var db = new Database();
        EnterAnimalName enterAnimalName = new EnterAnimalName();
        CreateNewAnimal createNewAnimal = new CreateNewAnimal();
        LearnNewCommand learnNewCommand = new LearnNewCommand();
        Commands commands = new Commands();
        
        try
        {
            while (true)
            {
                Console.WriteLine("1. Создать новое животное");
                Console.WriteLine("2. Обучить животное команде");
                Console.WriteLine("3. Выполнить команду");
                Console.WriteLine("4. Показать список команд");
                Console.WriteLine("5. Показать количество животных");
                Console.WriteLine("0. Выход");

                string? animalName = "";
                string? commandName = "";
                
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        if (!enterAnimalName.EnterName(ref animalName)) break;
                        if (!createNewAnimal.EnterAnimalAge()) break;
                        if (!createNewAnimal.EnterAnimalType()) break;
                        if (!createNewAnimal.SaveAnimal(animalName, animals, counter, db));
                        Console.WriteLine($"ID {animals[animalName].GetAnimalID()}");
                        break;

                    case "2":
                        if (!enterAnimalName.EnterName(ref animalName)) break;
                        if (!learnNewCommand.EnterCommandName()) break;
                        if (!learnNewCommand.EnterCommand()) break;
                        learnNewCommand.TeachCommand(animalName, animals, view, db);
                        break;

                    case "3":
                        if (!enterAnimalName.EnterName(ref animalName)) break;
                        if (!commands.EnterCommandName()) break;
                        commands.Execute(animalName, animals, view);
                        break;

                    case "4":
                        if (!enterAnimalName.EnterName(ref animalName)) break;
                        commands.ShowCommand(animalName, animals, view);
                        break;

                    case "5":
                        Console.WriteLine($"Количество животных: {counter.GetValue()}");
                        break;
                    
                    case "0":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        finally
        {
            counter.Dispose();
        }
    }
}