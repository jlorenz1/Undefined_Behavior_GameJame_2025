using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Image PlayerHealthBar;
    [SerializeField] public Image PlayerHealthBarBack;
    [SerializeField] public Image amyIcon;
    [SerializeField] public Image yamIcon;

    [SerializeField] public GameObject PauseMenu;
    [SerializeField] public GameObject CreditsMenu;
    public GameObject gameOver;
    [SerializeField] public Image FadeScreen;

   
    
    // Start is called before the first frame update
    void Start()
    {
    
        PlayerHealthBar.fillAmount = GameManager.gameInstance.playerHealth / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
    }

    void UpdatePlayerHealth()
    {
        PlayerHealthBar.fillAmount = GameManager.gameInstance.playerHealth / 100.0f;
    }

 
}
