using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject mainMenu;   
    private float mphConversion = 2.237f;
    private void Start()
    {
        PlayerPrefs.SetFloat("BrakePower", 1400f);
        PlayerPrefs.SetInt("BrakeBias", 5);
        PlayerPrefs.SetFloat("TopSpeed", 69.2912f * mphConversion);
        PlayerPrefs.SetFloat("RideHeightFront", 0.4f);
        PlayerPrefs.SetFloat("RideHeightRear", 0.4f);
        PlayerPrefs.SetFloat("Spring", 35000f);
        PlayerPrefs.SetFloat("Damper", 4500f);
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
