using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class BasicEnemyAI : MonoBehaviour,  IDamage
{
    [SerializeField] protected Animator controler;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] protected NavMeshAgent  EnemyNav;
    [SerializeField] public float CurrentHealth;
    [SerializeField] public float MaxHealth;
    [SerializeField] public float CurrentArmor;
    [SerializeField] public float MaxArmor;
    [SerializeField] public float AttackRange;
    [SerializeField] protected Transform HeadPos;
    [SerializeField] protected int ViewAngle;
    [SerializeField] protected float sight = 25;
    [SerializeField] float AutoDetectRange;


    [SerializeField] protected int level;



    public float currentSpeed;
    public bool PlayerInSight;
    float AngleToPlayer;
    Vector3 PlayerDrr;

    
    public GameObject Target;
   public bool CanAttack;
    // Start is called before the first frame update


    public void Awake()
    {
       
    }


    public virtual void Start()
    {

        Target = GameManager.gameInstance.player;

        PlayerInSight = true;
        CanAttack = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        EnemyNav.SetDestination(Target.transform.position);

        AutoDetect();

        if (CurrentHealth <= 0)
        {
            Die();
        }

        //  CanSeePlayer();


       currentSpeed =  EnemyNav.velocity.magnitude;

        controler.SetFloat("Speed", currentSpeed);
    }


   public void TakeDamage(float Amount)
    {
        CurrentHealth -= Amount;
    }
 

   protected virtual void Die()
    {
        Destroy(gameObject);
    }


   protected  bool CheckPlayerDistance()
    {
            if (Target == null) return false; // Ensure Target is assigned

            float distance = Vector3.Distance(transform.position, Target.transform.position);

            return distance <= AttackRange; // Change 10f to your desired detection range
     
    }


    void AutoDetect()
    {

        float distance = Vector3.Distance(transform.position, Target.transform.position);


        if (distance < AutoDetectRange)
        {
            PlayerInSight = true;

            FacePlayer();

        }
    }
   

   protected  void CanSeePlayer()
    {
        PlayerDrr = Target.transform.position - HeadPos.position;
        AngleToPlayer = Vector3.Angle(PlayerDrr, transform.forward);

        // Draw a ray for debugging
        Debug.DrawRay(HeadPos.position, PlayerDrr, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(HeadPos.position, PlayerDrr, out hit, sight))
        {
            string TargetTag = Target.tag;

            Debug.Log(TargetTag);
            if (hit.collider.CompareTag(TargetTag) && AngleToPlayer <= ViewAngle)
            {
                PlayerInSight = true;
                Debug.Log("Player in site");
            }
            else
            {
                PlayerInSight = false;
                Debug.Log("Player not in site");
            }
        }


    }


    protected void FacePlayer()
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));  // Ignore y-axis for rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // Adjust the rotation speed
    }

    public void CutSpeed(float amount)
    {
        EnemyNav.speed /= amount;
    }

    public void ResetAttack()
    {
        CanAttack = true;
    }
}
