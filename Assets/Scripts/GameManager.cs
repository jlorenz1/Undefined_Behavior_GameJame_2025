using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject player;
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
        
    }









}
