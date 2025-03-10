using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("hit");
            GameManager.gameInstance.Fade();
            CharacterController player = GameManager.gameInstance.Amy.gameObject.GetComponent<CharacterController>();
            if (player != null)
            {
                player.enabled = false;
                GameManager.gameInstance.endGameTriggered = true;
            }

        }
    }
}
