using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [HideInInspector] public bool planted;
    [HideInInspector] public Plant plant;

    private bool interact;
    private bool gameplayActive;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Harvest()
    {
        if(plant.plantState == Plant.PlantState.Ripe)
        {
            playerStats.scrip += plant.GetPlantSO().RipeReward;
        }
        else
        {
            playerStats.scrip += plant.GetPlantSO().DeadlyReward;
        }

        Reset();
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
        if (gameplayActive)
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

    public void Activate()
    {
        gameplayActive = true;
    }

    public void Reset()
    {
        plant.StopGrowth();
        Destroy(plant.gameObject);
        planted = false;
        numberOfButtonPress = 0;
    }
}
