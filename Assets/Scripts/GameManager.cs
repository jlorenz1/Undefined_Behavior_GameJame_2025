using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerControl;

    public GameObject Amy;
    public GameObject Yam;
    public bool isAmyActive;

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
        playerControl = player.GetComponent<PlayerController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(isAmyActive == true)
            {
                isAmyActive = false;
                Amy.SetActive(false);
                Yam.SetActive(true);
            }
            else if(isAmyActive == false)
            {
                isAmyActive = true;
                Yam.SetActive(false);
                Amy.SetActive(true);
            }
        }
    }

    







}
