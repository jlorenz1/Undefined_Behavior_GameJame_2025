using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public GameObject player;

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


            }
            return _gameInstance;
        }
    }


    void Awake()
    {
       
        player = GameObject.FindWithTag("Player");
       
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
