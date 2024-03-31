using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    
    public void OpenPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

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
        }        
    }

    public void ClosePause()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1.0f;
    }
}
