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
    public PlantSO GetPlantSO()
    {
        return plantSO;
    }

    private GameObject player;
    float attackCooldown;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackCooldown = 0;
    }

    public void Update()
    {
        attackCooldown += Time.deltaTime;
        if (attackCooldown > plantSO.AttackCooldown && plantState == PlantState.Deadly)
        {
            Attack();
        }
    }

    public void Attack()
    {
        switch(plantSO.plantVariety)
        {
            case PlantSO.PlantVariety.Pineapple:
                Vector3 targetPos = player.transform.position;
                GameObject pineapple = Instantiate(plantSO.AttackPrefab, transform.position, new Quaternion(0, 0, 0, 0));
                pineapple.GetComponent<ProjectileBehavior>().SetTargetPos(targetPos);
                attackCooldown = 0;
                break;
            case PlantSO.PlantVariety.Berry:
                break;
            default:
                break;
        }

    }
}
