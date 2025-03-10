using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;

    private void OnTriggerEnter(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();
        if(dmg != null)
        {
            dmg.TakeDamage(damage);
        }
    }
}
