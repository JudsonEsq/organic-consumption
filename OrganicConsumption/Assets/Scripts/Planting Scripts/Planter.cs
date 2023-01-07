using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planter : MonoBehaviour
{
    [SerializeField] private float plantingRate;
    [SerializeField] private float plantingWaitTime;

    [SerializeField] private List<Plot> plots;
    [SerializeField] private List<PlantSO> plantTypes;
    [SerializeField] private GameObject plantParent;

    [SerializeField] private GameObject plantPrefab;


    private void Start()
    {
        // Testing purposes
        StartCoroutine(PlantSeeds());
    }

    private void Update()
    {
       
    }

    private IEnumerator PlantSeeds()
    {
        if (plots == null || plots.Count == 0) yield break;
        if (plantTypes == null || plantTypes.Count == 0) yield break;
        if (plantPrefab == null) yield break;

        foreach (var plot in plots)
        {
            // If the current plot already has a seed, move on to the next plot
            if (plot.planted) continue;

            // Choose a random plant
            var randomIndex = Random.Range(0, plantTypes.Count);
            var seed = plantTypes[randomIndex];

            // Move the planter to the location and then plant the seed.
            var plotPosition = plot.transform.position;
            bool destinationReached = false;
            while (!destinationReached)
            {
                transform.position = Vector2.MoveTowards(transform.position, plotPosition, plantingRate * Time.deltaTime);
                //transform.DOMove(plotPosition, plantingRate);

                if (Vector2.Distance(transform.position, plotPosition) <= 0.1f)
                {
                    destinationReached = true;
                }

                yield return null;
            }

            // Wait some seconds before planting
            yield return new WaitForSeconds(plantingWaitTime);

            // Instantiate and plant the seed in the right location
            var plantGO = Instantiate(plantPrefab, plotPosition, Quaternion.identity, plantParent.transform);
            var plant = plantGO.GetComponent<Plant>();

            // Set the plant fields
            plant.plot = plot;
            plant.SetPlantSO(seed);

            // set plot state
            plot.planted = true;
        }
    }


}
