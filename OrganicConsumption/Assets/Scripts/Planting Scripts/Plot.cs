using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [HideInInspector] public bool planted;
    [HideInInspector] public Plant plant;

    private bool interact;

    private void Harvest()
    {
        // Whatever is supposed to happen during harvest goes here
        plant.StopGrowth();

        Destroy(plant.gameObject);
        planted = false;

        numberOfButtonPress = 0;
    }

    private int numberOfButtonPress;
    private void DeadlyStateHarvest() 
    {
        switch (plant.GetPlantSO().deadlyStateInteraction)
        {
            default:
            case PlantSO.HarvestInteraction.Mash:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    numberOfButtonPress++;
                }
                
                if (numberOfButtonPress >= plant.GetPlantSO().numberOfButtonPress)
                {
                    Harvest();
                }
                break;
        }
    }

    private void Update()
    {
        // Testing for input in update
        if (!interact) return;
        if (plant == null) return;

        if (plant.plantState == Plant.PlantState.Ripe) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Harvest();
            }
        }
        else if (plant.plantState == Plant.PlantState.Deadly)
        {
            DeadlyStateHarvest();
        }
    }

    // If the player is the plot range, make it possible to harvest. 
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interact = false;
        }
    }
}
