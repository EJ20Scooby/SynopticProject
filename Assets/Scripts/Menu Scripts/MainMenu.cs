using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionMenu;     
    
    private void Start()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }   

    public void goToTrack()
    {
        SceneManager.LoadScene("TestTrack");
    }

    public void openMain()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void openOptions()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
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
