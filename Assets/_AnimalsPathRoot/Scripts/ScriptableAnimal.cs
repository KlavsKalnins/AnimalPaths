using UnityEngine;
[System.Serializable]
public class ScriptableAnimal : System.ICloneable
{

    public AnimalType animalType;
    public TerrainType terrainType;
    public MinigameType minigameType;
    public GameObject prefab;
    public int pawsCount;

    public object Clone()
    {
        ScriptableAnimal animal = new ScriptableAnimal();
        animal.animalType = animalType;
        animal.prefab = prefab;

        return animal;
    }
    public enum AnimalType { Cat, Deer, Horse }
    public enum TerrainType { Land, Water }
    public enum MinigameType { PianoTiles }
}
