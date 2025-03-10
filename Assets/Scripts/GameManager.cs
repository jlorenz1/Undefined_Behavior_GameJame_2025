using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float playerHealth = 100.0f;
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

    bool D1Delay;
    bool endNegative = false;
    bool paused = false;
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
    }


    // Start is called before the first frame update
    void Start()
    {
        D1Delay = false;
        isAmyActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !endNegative)
        {
            if(isAmyActive == true)
            {
                UI.amyIcon.gameObject.SetActive(true);
                UI.yamIcon.gameObject.SetActive(false);
                UI.PlayerHealthBar.gameObject.SetActive(true);
                isAmyActive = false;
                Amy.SetActive(false);
                Yam.SetActive(true);
            }
            else if(isAmyActive == false)
            {
                UI.yamIcon.gameObject.SetActive(true);
                UI.amyIcon.gameObject.SetActive(false);
                UI.PlayerHealthBar.gameObject.SetActive(false);
                isAmyActive = true;
                Yam.SetActive(false);
                Amy.SetActive(true);
            }
        }



        if (D1Delay == false)
        {
            if (PickupCount == pickupCountOne && Door1 != null)
            {
                D1Delay = true;
                Door1.slide();
                D1Delay = false;
            }

            if(KillCount == 5)
            {
                //real door open
            }


            if (KillCount == 14 && Door2 != null)
            {
                if(Yam.gameObject.activeSelf == true)
                {
                    isAmyActive = true;
                }
                endNegative = true;
                //D1Delay = true;
                //Door2.slide();
                //D1Delay = false;
            }

            if (PickupCount == 9)
            {
                //end game trigger active 
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
                    Time.timeScale = 0.0f;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1.0f;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = true;
                }

            }
            
        }
    }

    







}
