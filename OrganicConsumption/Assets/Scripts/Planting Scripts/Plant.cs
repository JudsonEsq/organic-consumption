using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private PlantSO plantSO;
    private SpriteRenderer spriteRenderer;
    public BreakScreenUI breaktime;
    public enum PlantState { Seed, Sprout, Ripe, Deadly}
    public PlantState plantState;

    private LayerMask playerLayerMask;
    public void Init()
    {
        playerLayerMask = LayerMask.GetMask("Player");
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
    private PlayerStats playerStats;
    float attackCooldown;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
      
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


    private Vector3 animPunch = new Vector3(1f, 0.7f, 1f);
    private float animDuration = 0.35f;

    [SerializeField] private float meleeAttackTriggerCounter; // For Melee attacks 

    public void Attack()
    {
        switch(plantSO.plantVariety)
        {
            case PlantSO.PlantVariety.Pineapple:
                Vector3 targetPos = player.transform.position;
                GameObject pineapple = Instantiate(plantSO.AttackPrefab, transform.position, new Quaternion(0, 0, 0, 0));

                ProjectileBehavior pinenade = pineapple.GetComponent<ProjectileBehavior>();
                pinenade.SetTargetPos(targetPos);
                pinenade.breaktime = breaktime;

                transform.DOPunchScale(animPunch, animDuration);
                StartCoroutine(animateAttack(animDuration));
                attackCooldown = 0;
                break;
            case PlantSO.PlantVariety.Berry:
                // Melee Attack
                // Check if player is in range and wind up for attack.
                var result = Physics2D.OverlapCircle(transform.position, plantSO.meleeAtackRadius, playerLayerMask);
                if(result == null)
                {
                    meleeAttackTriggerCounter -= Time.deltaTime * plantSO.meleeAttackTriggerRate;
                }
                else
                {
                    meleeAttackTriggerCounter += Time.deltaTime * plantSO.meleeAttackTriggerRate;
                }
                meleeAttackTriggerCounter = Mathf.Clamp(meleeAttackTriggerCounter, 0, 100);

                // Color indication 
                ColorIndication();
                // Trigger attack after wind up is complete
                if (meleeAttackTriggerCounter >= 100)
                {
                    // Trigger Melee Attack

                    if (playerStats != null)
                    {
                        // Damage player
                        playerStats.Damage(1);
                        transform.DOPunchScale(animPunch, animDuration);
                        StartCoroutine(animateAttack(animDuration));
                        FindObjectOfType<AudioManager>().Play("Thwack");
                    }
                   
                    meleeAttackTriggerCounter = 0;
                    spriteRenderer.color = Color.white;

                    attackCooldown = 0;
                    
                }

                break;
            default:
                break;
        }

    }

    private IEnumerator animateAttack(float dur)
    {
        spriteRenderer.sprite = plantSO.attackSprite;
        if(transform.position.x > player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        yield return new WaitForSeconds(dur * 2);
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = plantSO.plantGrowthStages[3];
    }

    public void ColorIndication()
    {
        if (meleeAttackTriggerCounter <= 30)
        {
            // White 
            spriteRenderer.color = Color.white;
        }
        else if (meleeAttackTriggerCounter > 30 && meleeAttackTriggerCounter <= 60)
        {
            // Yellow
            spriteRenderer.color = Color.yellow;
        }
        else if (meleeAttackTriggerCounter > 60 && meleeAttackTriggerCounter <= 90)
        {
            // Orange
            var color = new Color(1, 0.64f, 0f);
            spriteRenderer.color = color;
        }
        else
        {
            // Red
            spriteRenderer.color = Color.red;
        }
    }
}
