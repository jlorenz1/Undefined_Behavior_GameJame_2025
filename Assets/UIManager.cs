using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Image PlayerHealthBar;
    [SerializeField] public Image amyIcon;
    [SerializeField] public Image yamIcon;

    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
    
        PlayerHealthBar.fillAmount = GameManager.gameInstance.playerControl.currHealth / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
    }

    void UpdatePlayerHealth()
    {
        PlayerHealthBar.fillAmount = GameManager.gameInstance.playerControl.currHealth / 100.0f;
    }
}
