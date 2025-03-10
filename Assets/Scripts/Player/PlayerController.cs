using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [Header("PLAYER GAME VARIABLES")]
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currHealth;
    [SerializeField] public GameObject Weapon;
    private Collider weaponCollider;

    [Header("PLAYER COMPONENTS")]
    [SerializeField] public CharacterController cController;
  
    [SerializeField] public Transform Cam;
    [SerializeField] public Animator anim;

    [SerializeField] Transform groundCheck;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public LayerMask platformMask;

    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed = 3.0f;
    [SerializeField] float turnDampTime = 0.1f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] float jumpHeight = 1.5f;


    float turnSmoothVelocity;
    float verticalVelocity;

    //float horizontalInput;
    //float verticalInput;

    //bools
    bool isGrounded;

    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        currHealth = maxHealth;
        currentSpeed = walkSpeed;
        weaponCollider = Weapon.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
        Movement();
        Jump();
        if(GameManager.gameInstance.isAmyActive == false)
        {
            Attack();
        }
       
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float speed = direction.magnitude * currentSpeed;
        float animSpeed = anim.GetFloat("Speed");
        float smoothSpeed = Mathf.Lerp(animSpeed, speed, Time.deltaTime * 10f);
        anim.SetFloat("Speed", smoothSpeed);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnDampTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cController.Move(moveDir * currentSpeed * Time.deltaTime);

        }

       
    }

    void Jump()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;

            anim.SetBool("Jump", false);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (!isGrounded && verticalVelocity < 0)
        {

            anim.SetBool("Jump", false);
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
        cController.Move(gravityMove * Time.deltaTime);

    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            currentSpeed = 0.0f;
            //StartCoroutine(performAttack());
            anim.SetTrigger("Attack");
        }

        if(!isAttacking)
        {
            currentSpeed = 3.0f;
            anim.SetTrigger("Move");
        }

    }

    IEnumerator performAttack()
    {
        isAttacking = true;
        weaponCollider.enabled = true;
        currentSpeed = 0.0f;
        anim.SetTrigger("Attack");

        
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        weaponCollider.enabled = false;
        anim.SetTrigger("Move");
        currentSpeed = 3.0f;
        isAttacking = false;
    }

    public void Respawn(Transform t)
    {
        transform.position = t.position;
    }

    public void TakeDamage(float amount)
    {
        GameManager.gameInstance.playerHealth -= amount;

        if(currHealth >= 0)
        {
            //GAME OVER
        }
    }

}
