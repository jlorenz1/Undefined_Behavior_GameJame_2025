using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAi : BasicEnemyAI
{
    public GameObject MeleeWeapon;
    meleeweaponscript Weapon;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        CanAttack = true;

        Weapon = MeleeWeapon.GetComponent<meleeweaponscript>();
        Weapon.SetStats(Target, this.tag);


    }


    public override void Update()
    {
        base.Update();


        EnemyNav.stoppingDistance = AttackRange;

        if (CheckPlayerDistance() && CanAttack && PlayerInSight)
        {
            CanAttack = false;


            controler.SetTrigger("Attack");

            Debug.Log("attack called");
        }

        // EnemyNav.stoppingDistance = AttackRange / 2;
        EnemyNav.SetDestination(Target.transform.position);



    }
}
