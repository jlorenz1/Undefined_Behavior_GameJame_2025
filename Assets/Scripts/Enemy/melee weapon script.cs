using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeweaponscript : MonoBehaviour
{
    public IDamage TargetDamage;

  

    //tag info
    string CasterTag;
    public GameObject target;

    [SerializeField] float Damage;
    [SerializeField] Collider Weaponcollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

   private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        if (other.isTrigger || other.tag == CasterTag)
        {
            Debug.Log("hit non target");
            return;
        }
       
        else if (other.tag == target.tag)
        {

            Debug.Log("hit");
            TargetDamage = other.GetComponent<IDamage>();
            if (TargetDamage != null)
            {
                TargetDamage.TakeDamage(Damage);
                
            }

        }



    }

    public void SetStats(GameObject Target,  string mCasterTag)
    {
        target = Target;
        
        CasterTag = mCasterTag;
    }
}
