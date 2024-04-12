using System.Security;
using Final_Work.Model.DomesticAnimals;

namespace Final_Work.Model.Menu;

public class CreateNewAnimal
{
    private IAnimal animal;
    private int _age;
    private DateTime _dateTime;
    private string animalType;
    private int _type = 0;
    

    public bool EnterAnimalAge()
    {
        Console.WriteLine("Введите дату рождения в формате: yyyy-MM-dd");
        var animalAge = Console.ReadLine();
        if (!DateTime.TryParse(animalAge, out _dateTime))
        {
            Console.WriteLine("Неверный возраст животного.");
            return false;
        }

        _age = DateTime.Now.Year - _dateTime.Year;
        
        return true;
    }

    public bool EnterAnimalType()
    {
        while (true)
        {
            Console.WriteLine("Введите тип животного (1 - кошка, 2 - собака, 3 - хомяк, 4 - лошадь, 5 - верблюд, 6 - осел):");
                            
            animalType = Console.ReadLine();
            if (!int.TryParse(animalType, out _type) || (_type < 1 || _type > 6))
            {
                Console.WriteLine("Неверный тип животного");
            }
            
            else break;
        }

        return true;
    }
        
    public bool SaveAnimal(string animalName, Dictionary<string, IAnimal> animals, Counter counter, Database db)
    {
        if (_type >= 1 && _type <= 3)
        {
            animal = new DomesticAnimal { Age = _age };
            animal.Name = animalName;
            animal.Breed = _type switch
            {
                1 => "Кошка",
                2 => "Собака",
                _ => "Хомяк"
            };

            animals[animalName] = animal;
            
            counter.Add();
            
            db.OpenConnection();
            animal.AddAnimal(db, _dateTime);
            Console.WriteLine(animal.GetAnimalID());
            db.CloseConnection();
        }
        else if (_type >= 4 && _type <= 6)
        {
            animal = new PackAnimal { Age = _age };
            animal.Name = animalName;
            animal.Breed = _type switch
            {
                4 => "Лошадь",
                5 => "Верблюд",
                _ => "Осел"
            };
            
            animals[animalName] = animal;
            
            counter.Add();
            
            db.OpenConnection();
            animal.AddAnimal(db, _dateTime);
            db.CloseConnection();
        }
        else
        {
            Console.WriteLine("Введите вид животного от 1 до 6");
        }
        return true;
    }
}