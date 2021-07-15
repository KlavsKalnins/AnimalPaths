using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Animal List")]
public class AnimalList : ScriptableObject
{
    
    public ScriptableAnimal.AnimalType currentActiveAnimal;
    [SerializeField] protected ScriptableAnimal[] animals;

    public ScriptableAnimal[] GetAnimalList()
    {
        return animals;
    }
    public ScriptableAnimal GetAnimal(int i)
    {
        return animals[i];
    }

    public void ModifyAnimalPawsAmount(int i,int amount)
    {
        animals[i].pawsCount += amount;
    }
}