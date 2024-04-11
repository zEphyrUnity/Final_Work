using System.Data;
using Final_Work.Model;
using Final_Work.Model.DomesticAnimals;
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

                string? choice = Console.ReadLine();

                string? animalName;
                string? commandName;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите имя животного:");
                        animalName = Console.ReadLine();
                        if (string.IsNullOrEmpty(animalName))
                        {
                            Console.WriteLine("Имя животного не может быть пустым.");
                            break;
                        }
                        
                        Console.WriteLine("Введите возраст животного:");
                        var animalAge = Console.ReadLine();
                        if (!int.TryParse(animalAge, out int age))
                        {
                            Console.WriteLine("Неверный возраст животного.");
                            break;
                        }

                        int type = 0;
                        while (true)
                        {
                            Console.WriteLine("Введите тип животного (1 - кошка, 2 - собака, 3 - хомяк, 4 - лошадь, 5 - верблюд, 6 - осел):");
                            
                            var animalType = Console.ReadLine();
                            if (!int.TryParse(animalType, out type) || (type < 1 || type > 6))
                            {
                                Console.WriteLine("Неверный тип животного");
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                        if (type >= 1 && type <= 3)
                        {
                            var animal = new DomesticAnimal { Age = age };
                            animal.Name = animalName;
                            animals[animalName] = animal;
                            
                            counter.Add();
                            
                            db.OpenConnection();
                            animal.SaveAnimalToDatabase(db);
                            db.CloseConnection();
                        }
                        else if (type >= 4 && type <= 6)
                        {
                            var animal = new PackAnimal { Age = age };
                            animal.Name = animalName;
                            animals[animalName] = animal;
                            
                            counter.Add();
                            
                            db.OpenConnection();
                            animal.SaveAnimalToDatabase(db);
                            db.CloseConnection();
                        }
                        else
                        {
                            Console.WriteLine("Введите вид животного от 1 до 6");
                        }

                        break;

                    case "2":
                        Console.WriteLine("Введите имя животного:");
                        animalName = Console.ReadLine();
                        if (string.IsNullOrEmpty(animalName) || !animals.ContainsKey(animalName))
                        {
                            Console.WriteLine("Животное с таким именем не найдено.");
                            break;
                        }

                        Console.WriteLine("Введите имя команды:");
                        commandName = Console.ReadLine();
                        if (string.IsNullOrEmpty(commandName))
                        {
                            Console.WriteLine("Имя команды не может быть пустым.");
                            break;
                        }

                        Console.WriteLine("Введите текст команды:");
                        var commandText = Console.ReadLine();
                        if (string.IsNullOrEmpty(commandText))
                        {
                            Console.WriteLine("Текст команды не может быть пустым.");
                            break;
                        }

                        var controller = new AnimalController(animals[animalName], view);
                        controller.TeachCommand(commandName, commandText);
                        break;

                    case "3":
                        Console.WriteLine("Введите имя животного:");
                        animalName = Console.ReadLine();
                        if (string.IsNullOrEmpty(animalName) || !animals.ContainsKey(animalName))
                        {
                            Console.WriteLine("Животное с таким именем не найдено.");
                            break;
                        }

                        Console.WriteLine("Введите имя команды:");
                        commandName = Console.ReadLine();
                        if (string.IsNullOrEmpty(commandName))
                        {
                            Console.WriteLine("Имя команды не может быть пустым.");
                            break;
                        }

                        controller = new AnimalController(animals[animalName], view);
                        controller.ExecuteCommand(commandName);
                        break;

                    case "4":
                        Console.WriteLine("Введите имя животного:");
                        animalName = Console.ReadLine();
                        if (string.IsNullOrEmpty(animalName) || !animals.ContainsKey(animalName))
                        {
                            Console.WriteLine("Животное с таким именем не найдено.");
                            break;
                        }

                        controller = new AnimalController(animals[animalName], view);
                        controller.ShowCommands();
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