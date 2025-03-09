using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemyAI : BasicEnemyAI, IDamage
{



    public GameObject projectilePrefab;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    [SerializeField] Transform launchPoint;


    [SerializeField] int castAmount;

    float castDelay = 1;
    [SerializeField] float castRange;
    bool canAttack;



    [SerializeField] GameObject RangedAttack;
    Projectile RangeStats;






    public override void Start()
    {
        base.Start();
        EnemyNav.stoppingDistance = castRange / 2;
        canAttack = true;


    }


    public override void Update()
    {
        base.Update();



        if (CheckPlayerDistance() && canAttack && PlayerInSight)
        {
            canAttack = false;
            CastBaseAttack();

        }

        // EnemyNav.stoppingDistance = AttackRange / 2;
        EnemyNav.SetDestination(Target.transform.position);



    }


    public void CastBaseAttack()
    {
        if (RangedAttack == null)
        {
            Debug.LogError("RangedAttack prefab is missing!");
            return;
        }

        GameObject newProjectile = Instantiate(RangedAttack, launchPoint.position, Quaternion.identity);
        RangeStats = newProjectile.GetComponent<Projectile>();

        if (RangeStats != null)
        {
            RangeStats.SetStats(Target.transform, level, this.tag);
            Debug.Log("shot");
        }
        else
        {
            Debug.LogError("Projectile script is missing on RangedAttack prefab!");
        }


        StartCoroutine(AttackCooldown());
    }


    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }


    public void EnableAttack()
    {
        canAttack = true;
    }


    protected override void Die()
    {
        // Additional ranged-specific death logic (if any)
        base.Die();
    }


}


