using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private PlantSO plantSO;
    private SpriteRenderer spriteRenderer;
    public enum PlantState { Seed, Sprout, Ripe, Deadly}
    public PlantState plantState;

    public void Init()
    {
        plantState = PlantState.Seed;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private IEnumerator StartGrowthSequence()
    {
        if (plantSO == null) yield break;
        if (plantSO.plantGrowthStages.Length == 0) yield break;

        // There are 4 stages
        for (int i = 0; i < 4; i++)
        {
            if (plantSO.plantGrowthStages[i] != null)
            {
                if (i == 1) {
                    plantState = PlantState.Sprout;
                }
                else if (i == 2)
                {
                    plantState = PlantState.Ripe;
                }
                else if (i == 3)
                {
                    plantState = PlantState.Deadly;
                }

                // Set the right image
                spriteRenderer.sprite = plantSO.plantGrowthStages[i];
                yield return new WaitForSeconds(plantSO.growthTime / 3);
            }
            
        }
    }

    public void StartGrowth()
    {
        StartCoroutine(StartGrowthSequence());
    }

    public void StopGrowth() 
    {
        StopAllCoroutines();
    }

    public void SetPlantSO(PlantSO plantSO)
    {
        this.plantSO = plantSO;
    }
}
