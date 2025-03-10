using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    [SerializeField] Transform spawn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            Debug.Log("Collide");
            if (GameManager.gameInstance.isAmyActive)
            {
                CharacterController player = GameManager.gameInstance.Amy.gameObject.GetComponent<CharacterController>();
                if (player != null)
                {
                    player.enabled = false;
                }

                GameManager.gameInstance.Amy.gameObject.transform.position = spawn.position;
                if (player != null)
                {
                    player.enabled = true;
                }
            }
            else
            {
                CharacterController player = GameManager.gameInstance.Yam.gameObject.GetComponent<CharacterController>();
                if (player != null)
                {
                    player.enabled = false;
                }

                GameManager.gameInstance.Yam.gameObject.transform.position = spawn.position;

                if (player != null)
                {
                    player.enabled = true;
                }
            }
           

        }
    }
}
