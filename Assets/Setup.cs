using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Setup : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] UnityEngine.UI.Slider finalDriveSlider;
    [SerializeField] Text topSpeed;    
    SimCarController carSettings;
    // Start is called before the first frame update
    void Start()
    {
        finalDriveSlider.value = 0.5f;
        finalDriveSlider.maxValue = 1.0f;
        finalDriveSlider.minValue = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseTopspeed()
    {
        if (finalDriveSlider.value != finalDriveSlider.maxValue)
        {
            player.GetComponent<SimCarController>().maxSpeed += 5;
            finalDriveSlider.value += 0.1f;
            topSpeed.text = "Top Speed: " + player.GetComponent<SimCarController>().maxSpeed + "MPH";
        }
    }

    public void decreseTopspeed()
    {
        if (finalDriveSlider.value != finalDriveSlider.minValue)
        {
            player.GetComponent<SimCarController>().maxSpeed -= 5;
            finalDriveSlider.value -= 0.1f;
            topSpeed.text = "Top Speed: " + player.GetComponent<SimCarController>().maxSpeed + "MPH";
        }
    }
}
