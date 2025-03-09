using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public CharacterController cController;
    [SerializeField] public Transform Cam;

    [SerializeField] float currentSpeed = 3.0f;
    [SerializeField] float turnDampTime = 0.1f;
    float turnSmoothVelocity;

    //float horizontalInput;
    //float verticalInput;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnDampTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cController.Move(moveDir * currentSpeed * Time.deltaTime);
        }    
        
    }

}
