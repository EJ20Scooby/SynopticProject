using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    bool optionOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            bool isOpen = pauseMenu.activeSelf;

            if (!isOpen && optionOpen == false)
            {
                OpenPause();
            }
            else
            {
                ClosePause();
            }
        }         
    }

    public void OpenPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ClosePause()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1.0f;
    }

    public void OpenOptions()
    {
        optionOpen = true;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void goToMain()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
    public void goToGarage()
    {
        SceneManager.LoadScene("Garage");
    }
}
