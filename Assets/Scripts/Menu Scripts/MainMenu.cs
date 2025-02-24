using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionMenu;
    private float mphConversion = 2.237f;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);

        PlayerPrefs.SetFloat("BrakePower", 1400f);
        PlayerPrefs.SetInt("BrakeBias", 5);
        PlayerPrefs.SetFloat("TopSpeed", 35.7632f * mphConversion);
        PlayerPrefs.SetFloat("RideHeightFront", 0.4f);
        PlayerPrefs.SetFloat("RideHeightRear", 0.4f);
        PlayerPrefs.SetFloat("SpringFront", 35000f);
        PlayerPrefs.SetFloat("SpringRear", 35000f);
        PlayerPrefs.SetFloat("DamperFront", 4500f);
        PlayerPrefs.SetFloat("DamperRear", 4500f);

        PlayerPrefs.SetFloat("DefaultSliderPos", 0.5f);
        PlayerPrefs.SetFloat("BrakePressureSliderPos", 0.5f);
        PlayerPrefs.SetFloat("BrakeBiasSliderPos", 0.5f);
        PlayerPrefs.SetFloat("FinalDriveSliderPos", 0.5f);
        PlayerPrefs.SetFloat("RideHeightFrontSliderPos", 0.5f);
        PlayerPrefs.SetFloat("RideHeightRearSliderPos", 0.5f);
        PlayerPrefs.SetFloat("SpringFrontSliderPos", 0.5f);
        PlayerPrefs.SetFloat("SpringRearSliderPos", 0.5f);
        PlayerPrefs.SetFloat("DamperFrontSliderPos", 0.5f);
        PlayerPrefs.SetFloat("DamperRearSliderPos", 0.5f);
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
