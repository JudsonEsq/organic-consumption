using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [HideInInspector] public bool planted;
    [HideInInspector] public Plant plant;



    private void Harvest()
    {
        Debug.Log("Harvest");
        // Whatever is supposed to happen during harvest goes here
        plant.StopGrowth();

        Destroy(plant.gameObject);
        planted = false;
        
    }

    // If the player is the plot range, make it possible to harvest. 
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (plant == null) return;
        if (plant.plantState != Plant.PlantState.Ripe) return;
      
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Harvest();
            }
        }
    }
}
