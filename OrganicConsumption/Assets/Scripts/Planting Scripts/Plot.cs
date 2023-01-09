using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plot : MonoBehaviour
{
    [HideInInspector] public bool planted;
    [HideInInspector] public Plant plant;
    [SerializeField] private MiniGameVisuals minigame;

    private bool interact;
    private bool gameplayActive;
    private MashPrompt masher;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        masher = gameObject.GetComponentInChildren<MashPrompt>();
        masher.Deactivate();
    }

    private void Harvest()
    {
        if(plant.plantState == Plant.PlantState.Ripe)
        {
            playerStats.scrip += plant.GetPlantSO().RipeReward;
            FindObjectOfType<AudioManager>().Play("Dig");
        }
        else
        {
            playerStats.scrip += plant.GetPlantSO().DeadlyReward;
            FindObjectOfType<AudioManager>().Play("Dig");
        }

        minigame.CloseMinigame();
        masher.Deactivate();
        plant.GetComponent<SpriteRenderer>().DOKill();

        Reset();
    }

    private int numberOfButtonPress;
    private Vector3 barOffset = new Vector3(0, 4, 0);
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
            case PlantSO.HarvestInteraction.Hold:
                minigame.OpenMinigame(transform.position + barOffset);
                masher.Deactivate();
                if(Input.GetKey(KeyCode.E))
                {
                    if(minigame.FillBar(Time.deltaTime))
                    {
                        Harvest();
                    }
                }
                else
                {
                    minigame.CloseMinigame();
                    masher.Activate(plant.plantState, plant.GetPlantSO().deadlyStateInteraction);
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
            if(plant != null && (plant.plantState == Plant.PlantState.Deadly || plant.plantState == Plant.PlantState.Ripe))
            {
                masher.Activate(plant.plantState, plant.GetPlantSO().deadlyStateInteraction);
            }
                
            interact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            masher.Deactivate();
            interact = false;
        }
    }

    public void Activate()
    {
        gameplayActive = true;
    }

    public void Reset()
    {
        if(plant != null) Destroy(plant.gameObject);
        planted = false;
        numberOfButtonPress = 0;
    }
}
