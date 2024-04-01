using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] setupMenus;
    int currentSetupMenu = 0;    

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
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            NextSetup();
        }
        if(Input.GetKeyDown(KeyCode.PageDown))
        {
            LastSetup();
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

    public void OpenMain()
    {
        ClosePause();
        mainMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CloseMain()
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetUpMenuOpen()
    {
        currentSetupMenu = 0;

        CloseMain();
        setupMenus[currentSetupMenu].SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void SetUpMenuClose()
    {
        setupMenus[currentSetupMenu].SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void NextSetup()
    {
        if(currentSetupMenu != setupMenus.Length-1) 
        {
            setupMenus[currentSetupMenu].SetActive(false);
            currentSetupMenu++;
            setupMenus[currentSetupMenu].SetActive(true);
        }
    }
    public void LastSetup()
    {
        if (currentSetupMenu > 0)
        {
            setupMenus[currentSetupMenu].SetActive(false);
            currentSetupMenu--;
            setupMenus[currentSetupMenu].SetActive(true);
        }
    }
}
