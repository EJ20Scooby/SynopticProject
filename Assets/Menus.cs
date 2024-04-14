using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{    
    [SerializeField] GameObject pauseMenu;    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            bool isOpen = pauseMenu.activeSelf;

            if (!isOpen)
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

    public void goToMain()
    {
        SceneManager.LoadScene("MainMenu");        
    }
    public void goToGarage()
    {
        SceneManager.LoadScene("Garage");
    }
}
