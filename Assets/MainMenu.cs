using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject mainMenu;   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToTrack()
    {
        SceneManager.LoadScene("TestTrack");
    }

    public void goToGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
