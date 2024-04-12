namespace Final_Work.Model.Menu;

public class EnterAnimalName
{
    public bool EnterName(ref string animalName)
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
}