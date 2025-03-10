using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;
    public IDamage dmg;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            return;
        }
      
         dmg = other.GetComponent<IDamage>();
        IDamage ParentDamage = other.GetComponentInParent<IDamage>();
        if(dmg != null)
        {
            Debug.Log("enemyHit");
            dmg.TakeDamage(damage);
        }
        else if(ParentDamage != null)
        {
            Debug.Log("enemyHit");

            ParentDamage.TakeDamage(damage);
        }
    }
}
