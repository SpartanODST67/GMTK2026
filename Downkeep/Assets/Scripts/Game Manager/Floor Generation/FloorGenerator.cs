using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public static FloorGenerator Instance;

    [SerializeField] SerializableDictionary<int, List<GameObject>> floorsPerLevel;
    Dictionary<int, List<GameObject>> floorsPerLevelDict;
    LinkedList<GameObject> generatedFloors;
    [SerializeField] int maxFloorsGenerated = 5;
    [SerializeField] int initialFloorsGenerated = 3;
    [SerializeField] Vector3 nextFloorPosition;
    [SerializeField] Vector3 floorSize;
    public int totalGeneratedFloors = 0;

    void Awake()
    {
        Instance = this;
        generatedFloors = new();
        floorsPerLevelDict = floorsPerLevel.ToDict();
    }

    void Start()
    {
        InitialFloors();
    }

    public void InitialFloors()
    {
        for(int i = 0; i < initialFloorsGenerated; i++)
        {
            GenerateFloor();
        }
    }

    public void GenerateFloor()
    {
        if(generatedFloors.Count >= maxFloorsGenerated)
        {
            GameObject sacrifice = generatedFloors.Last.Value;
            generatedFloors.RemoveLast();
            Destroy(sacrifice);
        }

        List<GameObject> floorPool;

        int curLevel = totalGeneratedFloors / 5;
        
        if(curLevel == 0)
        {
            floorPool = floorsPerLevelDict[0];
        } else
        {
            int upperBound = Math.Min(totalGeneratedFloors + 1, floorsPerLevelDict.Keys.Count);
            floorPool = floorsPerLevelDict[UnityEngine.Random.Range(0, upperBound)];
        }

        GameObject chosenFloor = floorPool[UnityEngine.Random.Range(0, floorPool.Count)];
        generatedFloors.AddFirst(Instantiate(chosenFloor, nextFloorPosition, Quaternion.identity, null));

        nextFloorPosition -= floorSize;
        totalGeneratedFloors++;
    }
}
