using Final_Work.Model;
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
        ShowAnimals showAnimals = new ShowAnimals();
        Commands commands = new Commands();
        SyncAnimalsWithDatabase syncAnimalsWithDatabase = new SyncAnimalsWithDatabase();
        syncAnimalsWithDatabase.SyncAnimals(animals);
        ShowAnimalCommands showAnimalCommands = new ShowAnimalCommands();
        
        try
        {
            while (true)
            {
                Console.WriteLine("1. Создать новое животное");
                Console.WriteLine("2. Обучить животное команде");
                Console.WriteLine("3. Выполнить команду");
                Console.WriteLine("4. Показать список команд");
                Console.WriteLine("5. Показать количество животных");
                Console.WriteLine("6. Показать все животные в питомнике");
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
                        if (!commands.EnterName()) break;
                        if (!commands.EnterCommandName()) break;
                        commands.PerformCommand(db);
                        break;

                    case "4":
                        if (!enterAnimalName.EnterName(ref animalName)) break;
                        showAnimalCommands.PrintAnimalCommands(db, animalName);
                        // commands.ShowCommand(animalName, animals, view);
                        break;

                    case "5":
                        // Console.WriteLine($"Количество животных: {counter.GetValue()}");
                        db.OpenConnection();
                        Console.WriteLine($"Количество животных: {db.ExecuteScalar("SELECT COUNT(*) FROM Animals")}");
                        db.CloseConnection();
                        break;
                    
                    case "6":
                        // showAnimals.ShowTable(db);
                        showAnimals.ShowTableDapper(db);
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