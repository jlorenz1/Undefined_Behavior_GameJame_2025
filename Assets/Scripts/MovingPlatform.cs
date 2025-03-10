using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float offset;
    [SerializeField] public float waitTime;


    private Vector3 startPos;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while(true)
        {
            float moveDirection = moving ? 1 : -1;
            Vector3 targetPos = startPos + new Vector3(0.0f, moveDirection * moveSpeed, 0.0f);

            while(Vector3.Distance(transform.position, targetPos) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            moving = !moving;
            yield return new WaitForSeconds(waitTime);
        }
    }

   
}
