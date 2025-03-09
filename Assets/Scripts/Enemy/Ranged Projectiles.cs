using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;



public class Projectile : MonoBehaviour
{



    //Stats Based On the user
  
    float followtime;

   

    [SerializeField] AudioSource ProjectileAudio;


    Rigidbody rb;
    Transform playerTransform;

    bool followTarget;
    
   //components 
    Vector3 currentDirection;
    
    //target info
    GameObject TargetTransform;
    public Transform target;
    IDamage TargetDamage;


    //tag info
    string CasterTag;



    [Header("Stats")]
    [SerializeField] public float Speed;
    [SerializeField] public float Damage;
    [SerializeField] public float Slow;
    [SerializeField] public float LifeTime;


    void Start()
    {
        followTarget = true;
        int projectileLayer = gameObject.layer;
       
    
    


        // Destroy the projectile after a certain amount of time
        Destroy(gameObject, LifeTime);

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        if (rb != null)
        {
            rb.useGravity = false; // Ensure gravity does not affect the projectile
        }

        // Find the TargetTransform object and initialize playerTransform
        TargetTransform = target.gameObject;
        if (TargetTransform != null)
        {
            playerTransform = TargetTransform.transform;
        }

        StartCoroutine(FollowPlayer(followtime));

    }
    void Update()
    {
        if (followTarget && TargetTransform != null)
        {
            // During the first second, follow the TargetTransform
            currentDirection = (TargetTransform.transform.position - transform.position).normalized;
        }

        // Move the projectile in the current direction

        if (rb != null)
        {
            rb.velocity = currentDirection * Speed;
        }
        else
        {
            transform.Translate(currentDirection * Speed * Time.deltaTime);
        }


     
    }

    IEnumerator FollowPlayer(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        // Stop following the TargetTransform and continue in the last known direction
        followTarget = false;
    }

    void OnDestroy()
    {
        // Stop the audio when the object is destroyed
        if (ProjectileAudio != null)
        {
            ProjectileAudio.Stop();
        }
        Debug.Log("obj destroyed");
    }

    void OnTriggerEnter(Collider other)
    {

      
        if (other.isTrigger || other.tag == CasterTag)
        {
            return;
        }
        else if(other.tag != target.tag)
        {
            Destroy(gameObject);
        }
     else if(other.tag == target.tag)
        {

            Debug.Log("hit");
            TargetDamage = other.GetComponent<IDamage>();
            if(TargetDamage != null)
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

    public void SetStats(Transform Target, int CasterLevel, string mCasterTag)
    {
        target = Target;
        Speed += CasterLevel / 25;
        Damage += CasterLevel /2;
        CasterTag = mCasterTag;
    }

 



   
}



