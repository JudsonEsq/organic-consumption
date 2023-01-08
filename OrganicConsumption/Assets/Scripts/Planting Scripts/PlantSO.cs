using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Plant", menuName = "PlantSO")]
public class PlantSO : ScriptableObject
{
    public enum  PlantVariety { Berry, Pineapple }
    public PlantVariety plantVariety;
    public enum HarvestInteraction { Mash, }
    public HarvestInteraction deadlyStateInteraction;
    [Tooltip("This is for the mash interaction")]
    public int numberOfButtonPress = 3;

    public float AttackCooldown;
    public GameObject AttackPrefab;

    public Sprite[] plantGrowthStages = new Sprite[4];
    // This is time it takes for the plant to move from the first stage to the last
    public float growthTime = 5f; 
}
