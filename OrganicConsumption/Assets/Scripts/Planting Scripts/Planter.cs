using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planter : MonoBehaviour
{
    [SerializeField] private float plantingRate = 2f;
    [SerializeField] private float plantingWaitTime = 1f;
    [SerializeField] private float deliveryDelay = 20f;
    [SerializeField] private int numberOfDeliveries;
    [SerializeField] private int numberOfSeeds;

    [SerializeField] private List<Plot> plots;
    [SerializeField] private List<PlantSO> plantTypes;
    [SerializeField] private GameObject plantParent;

    [SerializeField] private GameObject plantPrefab;

    private float nextLoop;


    private void Start()
    {
        // The start of deliveries
        numberOfDeliveries = 1;
        numberOfSeeds = 0;

        // Scale the number of seeds
        PlantingSession();
        // Start the planting
        StartCoroutine(PlantSeeds());

        nextLoop = Time.time;
    }

    private void Update()
    {
        // Running A planting session after a specified time
        if (Time.time > nextLoop + deliveryDelay)
        {
            StopAllCoroutines();

            // Increase number of deliveries
            numberOfDeliveries++;
            // Scale the number of seeds
            PlantingSession();

            // Start the planting
            StartCoroutine(PlantSeeds());

            // Set next loop
            nextLoop = Time.time;
        }
    }


    private void PlantingSession()
    {
        if (plots == null || plots.Count == 0) return;

        // Make sure the number of seeds do not exceed the number of plots.
        if (numberOfSeeds < plots.Count)
        {
            Scaling();
        }
        
    }

    private IEnumerator PlantSeeds()
    {
        if (plots == null || plots.Count == 0) yield break;
        if (plantTypes == null || plantTypes.Count == 0) yield break;
        if (plantPrefab == null) yield break;

        bool destinationReached = false;

        // Shuffle the list before a planting session
        ShuffleList(plots);

        var numberOfPlantedSeeds = 0;

        foreach (var plot in plots)
        {
            // Stop planting after reaching the right number
            if (numberOfPlantedSeeds == numberOfSeeds) break;

            // If the current plot already has a seed, move on to the next plot
            if (plot.planted) continue;

            // Choose a random plant
            var randomIndex = Random.Range(0, plantTypes.Count);
            var seed = plantTypes[randomIndex];

            // Move the planter to the location and then plant the seed.
            var plotPosition = plot.transform.position;
            destinationReached = false;
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

            // Set the plant on the plot
            plot.plant = plant;

            // Set the plant fields
            plant.Init();
            plant.SetPlantSO(seed);
            // Start plant growth
            plant.StartGrowth();
           
            // set plot state
            plot.planted = true;

            numberOfPlantedSeeds++;
        }

        // The planter goes offscreen when it is done planting, until it is needed again.
        var homePosition = new Vector3(-15, 12, 0);
        destinationReached = false;
        while (!destinationReached)
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition, plantingRate * 2f * Time.deltaTime);
            //transform.DOMove(plotPosition, plantingRate);

            if (Vector2.Distance(transform.position, homePosition) <= 0.1f)
            {
                destinationReached = true;
            }

            yield return null;
        }

    }

    private void Scaling() {
        var power = Mathf.Pow(numberOfDeliveries, 5);
        var log = Mathf.Log(power, 10);
        numberOfSeeds = Mathf.RoundToInt((log + 4) / 2);
    }

    private void ShuffleList<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int randomIndex = Random.Range(i, inputList.Count);
            inputList[i] = inputList[randomIndex];
            inputList[randomIndex] = temp;
        }
    }

}
