using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance {  get; private set; }
    public List<Plant> plantPrefabList;
    private void Awake()
    {
        Instance = this;
    }
    public void AddPlant(PlantType plantType)
    {
        Plant plantPrefab=GetPlantPrefab(plantType);
        if(plantPrefab==null)
        {
            print("要种植的植物不存在");
            return;
        }
    }
    private Plant GetPlantPrefab(PlantType plantType)
    {
        foreach(Plant plant  in plantPrefabList)
        {
            if(plant.plantType == plantType) return plant;
        }
        return null;
    }
}
