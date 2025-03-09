using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeweaponscript : MonoBehaviour
{
    IDamage TargetDamage;


    //tag info
    string CasterTag;
    public GameObject target;

    [SerializeField] float Damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.isTrigger || other.tag == CasterTag)
        {
            return;
        }
        else if (other.tag != target.tag)
        {
            Destroy(gameObject);
        }
        else if (other.tag == target.tag)
        {

            Debug.Log("hit");
            TargetDamage = other.GetComponent<IDamage>();
            if (TargetDamage != null)
            {
                TargetDamage.TakeDamage(Damage);


                Destroy(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
        }



    }
}
