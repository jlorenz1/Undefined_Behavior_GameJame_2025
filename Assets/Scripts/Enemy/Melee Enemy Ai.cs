using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAi : BasicEnemyAI
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
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
}
