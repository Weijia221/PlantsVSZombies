using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantState
{
    Enable,
    Disable
}
public class Plant : MonoBehaviour
{
    PlantState plantState= PlantState.Disable;
    public PlantType plantType=PlantType.Sunflower;
    private void update()
    {
        switch(plantState)
        {
            case PlantState.Enable:
                EnableUpdate();
                break;
            case PlantState.Disable:
                DisableUpdate();
                break;
            default:
                break;
        }
    }
    void DisableUpdate()
    {

    }
    void EnableUpdate()
    {

    }
}
