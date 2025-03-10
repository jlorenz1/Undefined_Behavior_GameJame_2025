using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] public GameObject credits;
    bool isCredits = false;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Credits()
    {
       isCredits = !isCredits;
       credits.SetActive(isCredits);
    }

    public void CreditsBack()
    {
        isCredits = !isCredits;
        credits.SetActive(isCredits);
    }
}
