using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float playerHealth = 1.0f;
    public PlayerController playerControl;
    public UIManager UI;

    public GameObject Amy;
    public GameObject Yam;
    public bool isAmyActive;

    public int KillCount;
    public int PickupCount;
    public int pickupCountOne = 5;

   public doorScript Door1;
    public doorScript Door2;
    public doorScript Door3;
    public GameObject gameOver;

    Transform spawn1;
    Transform spawn2;

   public Transform DreamSpawn1;
   public Transform DreamSpawn2;

    float currentFadeTime = 0.0f;
    float fadeDuration = 1.5f;


    PlayerController yamcontorler;


    bool D1Delay;
    bool endNegative = false;
    bool paused = false;
    bool isInScreen = false;
    bool won = false;
    public bool creditsActive = false;
    public bool respawned = false;
    public bool endGameTriggered = false;
    // Private static instance to the gameManager
    private static GameManager _gameInstance;

    // Public static property to access the instance
    public static GameManager gameInstance
    {
        get
        {
            if (_gameInstance == null)
            {
                _gameInstance = FindObjectOfType<GameManager>();
                if (_gameInstance == null)
                {

                    Debug.LogError("GameManager instance is missing!");
                }
            }
            return _gameInstance;
        }
    }


    void Awake()
    {
        if (_gameInstance == null)
        {
            _gameInstance = this;
        }
        else if (_gameInstance != this)
        {
            Destroy(gameObject);
        }

        player = GameObject.FindWithTag("Player");
      

        UI = FindAnyObjectByType<UIManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        D1Delay = false;
        isAmyActive = true;
        UI.PlayerHealthBarBack.gameObject.SetActive(false);

        yamcontorler = Yam.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(endGameTriggered)
        {
            Fade();
        }

        if (Input.GetKeyDown(KeyCode.G) && !endNegative)
        {
            if(isAmyActive == true)
            {
                UI.amyIcon.gameObject.SetActive(true);
                UI.yamIcon.gameObject.SetActive(false);
                UI.PlayerHealthBarBack.gameObject.SetActive(true);
                isAmyActive = false;
                Amy.SetActive(false);
                Yam.SetActive(true);
            }
            else if(isAmyActive == false)
            {
                UI.yamIcon.gameObject.SetActive(true);
                UI.amyIcon.gameObject.SetActive(false);
                UI.PlayerHealthBarBack.gameObject.SetActive(false);
                isAmyActive = true;
                Yam.SetActive(false);
                Amy.SetActive(true);
            }
        }

        if(playerHealth <= 0)
        {
            //respawn


            yamcontorler.enabled = false;

            if (PickupCount < 5)
            {
                Yam.transform.position = DreamSpawn1.transform.position;
                
            }
           else if (PickupCount >= 5)
            {
                Yam.transform.position = DreamSpawn2.transform.position;
            }

            playerHealth = 100;
            yamcontorler.enabled = true;

        }



        if (D1Delay == false)
        {
            if (PickupCount == pickupCountOne && Door1 != null && KillCount == 2)
            {
                D1Delay = true;
                Door1.slide();
                D1Delay = false;
            }

            if(KillCount == 2 && Door3 != null)
            {
                //real door open
                Door3.slide();
            }


            if (KillCount == 9)
            {
                if(isAmyActive == false)
                {
                    UI.yamIcon.gameObject.SetActive(true);
                    UI.amyIcon.gameObject.SetActive(false);
                    UI.PlayerHealthBar.gameObject.SetActive(false);
                    isAmyActive = true;
                    Yam.SetActive(false);
                    Amy.SetActive(true);
                }
                endNegative = true;
                //D1Delay = true;
                //Door2.slide();
                //D1Delay = false;
            }

            if (PickupCount == 9 && endNegative)
            {
                //end game trigger active
                gameOver.SetActive(true);
               
              
            }

            Pause();

        }

        void Pause()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;

                if(paused)
                {
                    UI.PauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                else
                {
                    UI.PauseMenu.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = true;
                }

            }
            
        }
    }

    public void Fade()
    {
        currentFadeTime += Time.deltaTime;

        float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeDuration);
        UI.FadeScreen.color = new Color(0, 0, 0, alpha);

        if(currentFadeTime >= 0)
        {
            isInScreen = true;
            
        }

        if (currentFadeTime >= fadeDuration)
        {
            UI.gameOver.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }







}
