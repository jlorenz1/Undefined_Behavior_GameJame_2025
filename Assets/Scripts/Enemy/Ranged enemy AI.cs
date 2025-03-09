using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemyAI : BasicEnemyAI, IDamage
{



   
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    [SerializeField] Transform launchPoint;


    [SerializeField] int castAmount;

    float castDelay = 1;
    [SerializeField] float castRange;
   



    [SerializeField] GameObject RangedAttack;
    Projectile RangeStats;






    public override void Start()
    {
        base.Start();
        EnemyNav.stoppingDistance = castRange / 2;
        


    }


    public override void Update()
    {
        base.Update();

        


        if (CheckPlayerDistance() && CanAttack && PlayerInSight)
        {
            CanAttack = false;


            controler.SetTrigger("Attack");
           

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


        
    }


    protected override void Die()
    {
        // Additional ranged-specific death logic (if any)
        base.Die();
    }


}


