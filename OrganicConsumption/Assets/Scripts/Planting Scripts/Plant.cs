using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private PlantSO plantSO;
    [HideInInspector] public Plot plot;
    private SpriteRenderer spriteRenderer;


    public void SetPlantSO(PlantSO plantSO)
    {
        this.plantSO = plantSO;
    }
}
