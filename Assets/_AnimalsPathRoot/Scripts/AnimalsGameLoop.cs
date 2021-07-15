using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AnimalsGameLoop : MonoBehaviour
{
    public ScriptableAnimal.AnimalType currentAnimal = ScriptableAnimal.AnimalType.Cat;
    [SerializeField] private AnimalList animalList;
    public static AnimalsGameLoop instance;
    [SerializeField] private GameObject prefabfootprint;
    public List<GameObject> footprints;
    [SerializeField] private float spawnTime;
    [SerializeField] private float currentSpawnTime;
    [SerializeField] private float movementSpeed = 2;
    
    [SerializeField] private int currentActiveAnimal;
    [SerializeField] private Animal[] animals;

    [SerializeField] private float animalSwitchTime = 20;
    private float currentAnimalSwitchTime;
    [SerializeField] private float animalSwitchTimeIncrement = 10;
    private bool firstPaw;
    

    private void OnEnable()
    {
        LoadingAnimalList();
        instance = this;
        currentAnimalSwitchTime = animalSwitchTime;
    }

    public void ColorLerpFirstFootprint()
    {
        if (footprints[0] == null)
            return;
        Debug.Log("SWITCH COLOR");
        footprints[0].GetComponent<Footprint>().LerpColorTo(0);
    }

    private void LoadingAnimalList()
    {
        animals = new Animal[animalList.GetAnimalList().Length];
        for (int i = 0; i < animalList.GetAnimalList().Length; i++)
        {

            ScriptableAnimal sa = animalList.GetAnimal(i);
            var obj = Instantiate(sa.prefab, transform.position, quaternion.identity);
            obj.name = sa.animalType.ToString();
            obj.transform.SetParent(transform);
            Animal a = obj.GetComponent<Animal>();
            animals[i] = a;

            if (i == (int) animalList.currentActiveAnimal)
            {
                currentActiveAnimal = (int) animalList.currentActiveAnimal;
                a.SetVisibility(true);
                currentAnimal = animalList.currentActiveAnimal;
            }
            else
            {
                a.SetRunning(true);
            }

        }
        StartCoroutine(Sit());
    }

    IEnumerator Sit()
    {
        yield return new WaitForSeconds(1);
        animals[currentActiveAnimal].SetRunning(false);
    }

    void Start()
    {
        currentSpawnTime = spawnTime;
    }

    public void GameStart()
    {
        animals[currentActiveAnimal].SetRunning(true);
    }
    
    
    void Update()
    {

        if (!GameManager.instance.isPlaying)
            return;

        if (Input.GetKeyDown(KeyCode.H))
        {
            ColorLerpFirstFootprint();
        }
        
        transform.position = transform.position + new Vector3(0, 0, movementSpeed * Time.deltaTime);
        FootprintSpawnGameLoop();

        currentAnimalSwitchTime -= Time.deltaTime;
        if (currentAnimalSwitchTime <= 0)
        {
            SwitchAnimals();
            animalSwitchTime += animalSwitchTimeIncrement;
            currentAnimalSwitchTime = animalSwitchTime;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchAnimals();
        }
    }

    private void FootprintSpawnGameLoop()
    {
        spawnTime = spawnTime - 0.00002f;
        if (spawnTime < 0.2f)
            spawnTime = 0.2f;
        
        GameManager.instance.SetFollowCam(footprints.Count > 0 ? footprints[0].transform : transform);
        
        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime <= 0)
        {
            var obj = Instantiate(prefabfootprint, transform.position, Quaternion.identity);
            obj.transform.eulerAngles = new Vector3(90, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z);
            currentSpawnTime = spawnTime;
            var r = UnityEngine.Random.Range(0, 2);
            
            Footprint fp = obj.GetComponent<Footprint>();
            fp.SetSprite((int)currentAnimal);
            if (r >= 1)
            {
                obj.transform.position = new Vector3(-0.15f, obj.transform.position.y, obj.transform.position.z);
                fp.isLeft = true;
            }
            else
            {
                obj.transform.position = new Vector3(0.15f, obj.transform.position.y, obj.transform.position.z);
            }
            footprints.Add(obj);
            if (firstPaw) 
                return;
            fp.LerpColorTo(0);
            firstPaw = true;
        }
    }
    
    public void SwitchAnimals()
    {
        int prevAnimal = (int) currentAnimal;

        currentAnimal++;
        Debug.Log("animals.Length: " + animals.Length);
        if ((int)currentAnimal > animals.Length - 1)
        {
            currentAnimal = ScriptableAnimal.AnimalType.Cat;
        }
        
        Debug.Log("Animal: " + prevAnimal + " - " + (int) currentAnimal);
        currentActiveAnimal = (int) currentAnimal;
        switch (currentAnimal)
        {
            case ScriptableAnimal.AnimalType.Cat:
                animals[currentActiveAnimal].SetVisibility(true);
                animals[prevAnimal].SetVisibility(false);
                break;
            case ScriptableAnimal.AnimalType.Deer:
                animals[currentActiveAnimal].SetVisibility(true);
                animals[prevAnimal].SetVisibility(false);
                break;
        }
    }
 
}