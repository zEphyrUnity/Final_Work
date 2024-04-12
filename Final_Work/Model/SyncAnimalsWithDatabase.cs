using System.Data;
using Final_Work.Model.DomesticAnimals;

namespace Final_Work.Model;

public class SyncAnimalsWithDatabase
{
    public void SyncAnimals(Dictionary<string, IAnimal> animals)
    {
        var database = new Database();
        database.OpenConnection();
        
        var domesticAnimalsTable = database.ExecuteQuery("SELECT Animals.AnimalID, Animals.Name, Animals.Age, DomesticAnimals.Breed FROM \n" +
                                                         "Animals INNER JOIN DomesticAnimals ON Animals.AnimalID = DomesticAnimals.AnimalID \n" +
                                                         "WHERE Animals.Type = 'Domestic'");
        
        foreach (DataRow row in domesticAnimalsTable.Rows)
        {
            var animalId = int.Parse(row["AnimalID"].ToString());
            var name = row["Name"].ToString();
            var age = int.Parse(row["Age"].ToString());
            var breed = row["Breed"].ToString();

            var animal = new DomesticAnimal { Age = age, Name = name, Breed = breed, AnimalId =  animalId};
            animals[name] = animal;
        }
        
        var packAnimalsTable = database.ExecuteQuery("SELECT Animals.AnimalID, Animals.Name, Animals.Age, PackAnimals.Breed FROM \n" +
                                                     "Animals INNER JOIN PackAnimals ON Animals.AnimalID = PackAnimals.AnimalID WHERE \n" +
                                                     "Animals.Type = 'Pack'");
        
        foreach (DataRow row in packAnimalsTable.Rows)
        {
            var animalId = int.Parse(row["AnimalID"].ToString());
            var name = row["Name"].ToString();
            var age = int.Parse(row["Age"].ToString());
            var breed = row["Breed"].ToString();

            var animal = new PackAnimal { Age = age, Name = name, Breed = breed, AnimalId =  animalId };
            animals[name] = animal;
        }

        database.CloseConnection();
    }
}