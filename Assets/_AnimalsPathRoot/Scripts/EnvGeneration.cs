using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGeneration : MonoBehaviour
{
    [SerializeField] private GameObject EnvPrefab;
    [SerializeField] private List<GameObject> envBlocks;
    [SerializeField] private int pooledEnvAmount = 4;
    [SerializeField] private int blockGap = 10;
    
    
    [SerializeField] private int currentEnvBlockIndex;
    [SerializeField] private int editEnvPoint = 20;
    [SerializeField] private int nextEnvPos = 40;
    
    [SerializeField] private Transform target;

    [SerializeField] private bool isRunning;

    private void OnEnable()
    {
        SetEnv();
    }

    void Start()
    {
    }


    void Update()
    {
        if (!isRunning)
            return;
        if (target.transform.position.z >= editEnvPoint)
        {
            editEnvPoint += 10;
            nextEnvPos += 10;
            envBlocks[currentEnvBlockIndex].transform.position = new Vector3(0, 0, nextEnvPos);
            currentEnvBlockIndex++;
            if (currentEnvBlockIndex >= envBlocks.Count)
                currentEnvBlockIndex = 0;
        }
    }

    private void SetEnv()
    {
        var blockStep = 0;
        for (int i = 0; i < pooledEnvAmount; i++)
        {
            var obj = Instantiate(EnvPrefab, transform.position, Quaternion.identity);
            obj.transform.position = new Vector3(0, 0, blockStep);
            envBlocks.Add(obj);
            blockStep += blockGap;
        }
        nextEnvPos = (int)envBlocks[envBlocks.Count - 1].transform.position.z;
    }
}