using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planter : MonoBehaviour
{
    [SerializeField] private float plantingRate = 2f;
    [SerializeField] private float plantingWaitTime = 1f;
    [SerializeField] private float deliveryDelay = 20f;
    private int numberOfDeliveries;
    private int numberOfSeeds;

    private bool onBreak;
    private int sessionDeliveries;

    [SerializeField] private int breakInterval = 8;

    [SerializeField] private List<Plot> plots;
    [SerializeField] private List<PlantSO> plantTypes;
    [SerializeField] private GameObject plantParent;

    [SerializeField] private UnityEngine.UI.Slider breakBar;
    [SerializeField] private GameObject plantPrefab;

    private float nextLoop;

    private Vector3 homePosition;
    private bool gameplayActive = false;

    [SerializeField] private GameObject breakPanel;
    private AudioManager audioMan;
    private BreakScreenUI breakScrn;


    private void Start()
    {
        homePosition = transform.position;
        // The start of deliveries
        numberOfDeliveries = 1;
        sessionDeliveries = 0;
        numberOfSeeds = 0;
        onBreak = false;
        breakScrn = breakPanel.GetComponent<BreakScreenUI>();
        audioMan = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        breakBar.value += Time.deltaTime / (breakInterval * deliveryDelay);
        if(sessionDeliveries == breakInterval)
        {
            StopAllCoroutines();
            if (!onBreak)
            {
                gameplayActive = false;
                breakPanel.SetActive(true);

                audioMan.StopAll();
                audioMan.Play("Menu");
                audioMan.Play("Whistle");

                breakScrn.BreakTime();
                onBreak = true;
                HardReset(false);
            }
            else if (!breakScrn.IsShopping())
            {
                // Reset the break meter's progress
                breakBar.value = 0;
                onBreak = false;
                
                // Transition back to game audio
                audioMan.StopAll();
                audioMan.Play("Game");
                
                // Breaks get farther apart with each break (inflation ): )
                breakInterval++;
                gameplayActive = true;
                sessionDeliveries = 0;
                breakPanel.SetActive(false);
            }
        }
        // Running A planting session after a specified time
        if (gameplayActive)
        {
            if (Time.time > nextLoop + deliveryDelay)
            {
                StopAllCoroutines();

                // Increase number of deliveries
                numberOfDeliveries++;
                // Scale the number of seeds
                PlantingSession();

                // Start the planting
                StartCoroutine(PlantSeeds());

                sessionDeliveries++;

                // Set next loop
                nextLoop = Time.time;
            }
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

        bool destinationReached;

        // Shuffle the list before a planting session
        ShuffleList(plots);

        var numberOfPlantedSeeds = 0;
        if(numberOfDeliveries > 1) FindObjectOfType<AudioManager>().Play("Siren");

        foreach (var plot in plots)
        {
            // Stop planting after reaching the right number
            if (numberOfPlantedSeeds >= numberOfSeeds) break;

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
            FindObjectOfType<AudioManager>().Play("Dig");
            var plantGO = Instantiate(plantPrefab, plotPosition, Quaternion.identity, plantParent.transform);
            var plant = plantGO.GetComponent<Plant>();

            plant.breaktime = breakScrn;

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

    public void Activate()
    {
        StopAllCoroutines();
        // Scale the number of seeds
        PlantingSession();
        // Start the planting
        StartCoroutine(PlantSeeds());

        foreach(Plot p in plots)
        {
            p.Activate();
        }

        nextLoop = Time.time;

        gameplayActive = true;
    }

    // Hard is true if you are resetting the entire game. False if simply going on break (slacker)
    public void HardReset(bool hard)
    {
        StopAllCoroutines();
        transform.position = homePosition;
        gameplayActive = false;
        
        foreach(Plot p in plots)
        {
            p.Reset();
        }

        if(hard)
        {
            numberOfDeliveries = 1;
            sessionDeliveries = 0;
        }
        numberOfSeeds = 0;
        breakBar.value = 0;
    }

}
