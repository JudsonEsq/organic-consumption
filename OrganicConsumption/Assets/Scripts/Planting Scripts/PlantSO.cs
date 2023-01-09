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
    [Tooltip("Minimum time, in seconds, between attacks")]
    public float AttackCooldown;
    [Tooltip("Radius for melee plants with melee attacks")]
    public float meleeAtackRadius = 5f;
    [Tooltip("Rate at which melee plant gets ready to attack")]
    public float meleeAttackTriggerRate = 2f;
    [Tooltip("What prefab to spawn as an attack when attacking")]
    public GameObject AttackPrefab;

    public int RipeReward;
    public int DeadlyReward;

    [Tooltip("Sprites to use at each stage of growth")]
    public Sprite[] plantGrowthStages = new Sprite[4];
    [Tooltip("Sprite to be used for the attacking frame")]
    public Sprite attackSprite;
    [Tooltip("This is time it takes for the plant to move from the first stage to the last")]
    public float growthTime = 5f; 
}
