using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Image PlayerHealthBar;

    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        PlayerHealthBar.fillAmount = player.currHealth / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
    }

    void UpdatePlayerHealth()
    {
        PlayerHealthBar.fillAmount = player.currHealth / 100.0f;
    }
}
