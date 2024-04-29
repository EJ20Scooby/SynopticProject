using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Setup : MonoBehaviour
{
    private float mphConversion = 2.237f;
        
    public float brakePower;
    public int brakeBiasLevel;    
    public float maxSpeed;
    public float rideHeightF;
    public float rideHeightR;
    public float suspensionF;
    public float suspensionR;
    public float damperF;
    public float damperR;

    int currentSetupMenu = 0;
    [SerializeField] GameObject setupButton;
    [SerializeField] GameObject driveButton;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject[] setupMenus;    

    [SerializeField] GameObject player;
    SimCarController carSettings;

    [SerializeField] UnityEngine.UI.Slider brakePressureSlider;
    [SerializeField] Text pressure;
    private float brakePressureAdjust = 50;

    [SerializeField] UnityEngine.UI.Slider brakeBiasSlider;
    [SerializeField] Text bias;    

    [SerializeField] UnityEngine.UI.Slider finalDriveSlider;
    [SerializeField] Text topSpeed;

    [SerializeField] UnityEngine.UI.Slider rideHeightSliderFront;
    [SerializeField] UnityEngine.UI.Slider rideHeightSliderRear;

    [SerializeField] UnityEngine.UI.Slider suspensionStiffnessSliderFront;
    [SerializeField] Text stiffnessFront;
    [SerializeField] UnityEngine.UI.Slider suspensionStiffnessSliderRear;
    [SerializeField] Text stiffnessRear;
    private float springAdjust = 5000.0f;

    [SerializeField] UnityEngine.UI.Slider damperStiffnessSliderFront;
    [SerializeField] Text damperFront;
    [SerializeField] UnityEngine.UI.Slider damperStiffnessSliderRear;
    [SerializeField] Text damperRear;
    private float damperAdjust = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        DefaultValues();
        brakePressureSlider.value = 0.5f;
        brakePressureSlider.maxValue = 1.0f;
        brakePressureSlider.minValue = 0.0f;
        pressure.text = "Pressure: " + PlayerPrefs.GetFloat("BrakePower");

        brakeBiasSlider.value = 0.5f;
        brakeBiasSlider.maxValue = 1.0f;
        brakeBiasSlider.minValue = 0.0f;
        player.GetComponent<SimCarController>().SetBrakeBias();
        bias.text = "F " + player.GetComponent<SimCarController>().brakeFront * 100 + "/R " + player.GetComponent<SimCarController>().brakeRear * 100;

        finalDriveSlider.value = 0.5f;
        finalDriveSlider.maxValue = 1.0f;
        finalDriveSlider.minValue = 0.0f;
        topSpeed.text = "Top Speed: " + PlayerPrefs.GetFloat("TopSpeed").ToString("F0") + "MPH";

        rideHeightSliderFront.value = 0.5f;
        rideHeightSliderFront.maxValue = 1.0f;
        rideHeightSliderFront.minValue = 0.0f;
        rideHeightSliderRear.value = 0.5f;
        rideHeightSliderRear.maxValue = 1.0f;
        rideHeightSliderRear.minValue = 0.0f;

        suspensionStiffnessSliderFront.value = 0.5f;
        suspensionStiffnessSliderFront.maxValue = 1.0f;
        suspensionStiffnessSliderFront.minValue = 0.0f;
        stiffnessFront.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";
        suspensionStiffnessSliderRear.value = 0.5f;
        suspensionStiffnessSliderRear.maxValue = 1.0f;
        suspensionStiffnessSliderRear.minValue = 0.0f;
        stiffnessRear.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.spring + "N/m";

        damperStiffnessSliderFront.value = 0.5f;
        damperStiffnessSliderFront.maxValue = 1.0f;
        damperStiffnessSliderFront.minValue = 0.0f;
        damperFront.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
        damperStiffnessSliderRear.value = 0.5f;
        damperStiffnessSliderRear.maxValue = 1.0f;
        damperStiffnessSliderRear.minValue = 0.0f;
        damperRear.text = "Damper: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.damper + "N-s/m2";

        brakePower = PlayerPrefs.GetFloat("BrakePower");
        brakeBiasLevel = PlayerPrefs.GetInt("BrakeBias");
        maxSpeed = PlayerPrefs.GetFloat("TopSpeed");
        rideHeightF = PlayerPrefs.GetFloat("RideHeightFront");
        rideHeightR = PlayerPrefs.GetFloat("RideHeightRear");
        suspensionF = PlayerPrefs.GetFloat("SpringFront");
        suspensionR = PlayerPrefs.GetFloat("SpringRear");
        damperF = PlayerPrefs.GetFloat("DamperFront");
        damperR = PlayerPrefs.GetFloat("DamperRear");
    }

    void DefaultValues()
    {
        PlayerPrefs.SetFloat("BrakePower", 1400f);
        PlayerPrefs.SetInt("BrakeBias", 5);
        brakeBiasLevel = 5;
        player.GetComponent<SimCarController>().brakeBiasLevel = 5;
        PlayerPrefs.SetFloat("TopSpeed", 35.7632f * mphConversion);
        PlayerPrefs.SetFloat("RideHeightFront", 0.4f);
        PlayerPrefs.SetFloat("RideHeightRear", 0.4f);
        PlayerPrefs.SetFloat("SpringFront", 35000f);
        PlayerPrefs.SetFloat("SpringRear", 35000f);
        PlayerPrefs.SetFloat("DamperFront", 4500f);
        PlayerPrefs.SetFloat("DamperRear", 4500f);
        player.GetComponent<SimCarController>().colliders.FRWheel.suspensionDistance = 0.4f;
        player.GetComponent<SimCarController>().colliders.FLWheel.suspensionDistance = 0.4f;
        player.GetComponent<SimCarController>().colliders.RRWheel.suspensionDistance = 0.4f;
        player.GetComponent<SimCarController>().colliders.RLWheel.suspensionDistance = 0.4f;
        
        var Default = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
        Default.spring = 35000f;
        Default.damper = 4500f;

        player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = Default;
        player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = Default;
        player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = Default;
        player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = Default;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            NextSetup();
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            LastSetup();
        }
    }

    public void goToTrack()
    {
        SceneManager.LoadScene("TestTrack");
    }

    public void goToMain()
    {
        SceneManager.LoadScene("MainMenu");        
    }

    public void increaseBrakePressure()
    {
        if(brakePressureSlider.value != brakePressureSlider.maxValue) 
        {
            brakePower = brakePower + brakePressureAdjust;
            PlayerPrefs.SetFloat("BrakePower", brakePower);
            brakePressureSlider.value += 0.1f;
            pressure.text = "Pressure: " + PlayerPrefs.GetFloat("BrakePower");
        }
    }

    public void decreaseBrakePressure()
    {
        if (brakePressureSlider.value != brakePressureSlider.minValue)
        {
            brakePower = brakePower - brakePressureAdjust;
            PlayerPrefs.SetFloat("BrakePower", brakePower);
            brakePressureSlider.value -= 0.1f;
            pressure.text = "Pressure: " + PlayerPrefs.GetFloat("BrakePower");
        }
    }

    public void increaseBrakeBias()
    {
        if (brakeBiasSlider.value != brakeBiasSlider.maxValue)
        {
            brakeBiasLevel = brakeBiasLevel + 1;
            player.GetComponent<SimCarController>().brakeBiasLevel += 1;
            PlayerPrefs.SetInt("BrakeBias", brakeBiasLevel);
            brakeBiasSlider.value += 0.1f;
            player.GetComponent<SimCarController>().SetBrakeBias();
            bias.text = "F " + player.GetComponent<SimCarController>().brakeFront * 100 + "/R " + player.GetComponent<SimCarController>().brakeRear * 100;
        }
    }

    public void decreaseBrakeBias()
    {
        if(brakeBiasSlider.value != brakeBiasSlider.minValue)
        {
            brakeBiasLevel = brakeBiasLevel - 1;
            player.GetComponent<SimCarController>().brakeBiasLevel -= 1;
            PlayerPrefs.SetInt("BrakeBias", brakeBiasLevel);
            brakeBiasSlider.value -= 0.1f;
            player.GetComponent<SimCarController>().SetBrakeBias();
            bias.text = "F " + player.GetComponent<SimCarController>().brakeFront * 100 + "/R " + player.GetComponent<SimCarController>().brakeRear * 100;
       }
    }    

    public void increaseTopspeed()
    {
        if (finalDriveSlider.value != finalDriveSlider.maxValue)
        {
            maxSpeed = maxSpeed + 5;
            PlayerPrefs.SetFloat("TopSpeed", maxSpeed);
            finalDriveSlider.value += 0.1f;
            topSpeed.text = "Top Speed: " + PlayerPrefs.GetFloat("TopSpeed").ToString("F0") + "MPH";
        }
    }

    public void decreseTopspeed()
    {
        if (finalDriveSlider.value != finalDriveSlider.minValue)
        {
            maxSpeed = maxSpeed - 5;
            PlayerPrefs.SetFloat("TopSpeed", maxSpeed);
            finalDriveSlider.value -= 0.1f;
            topSpeed.text = "Top Speed: " + PlayerPrefs.GetFloat("TopSpeed").ToString("F0") + "MPH";
        }
    }

    public void increaseRideHeightFront()
    {
        if (rideHeightSliderFront.value != rideHeightSliderFront.maxValue)
        {
            rideHeightSliderFront.value += 0.1f;

            rideHeightF += 0.025f;
            PlayerPrefs.SetFloat("RideHeightFront", rideHeightF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionDistance += 0.025f;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionDistance += 0.025f;             
        }
    }

    public void decreseRideHeightFront()
    {
        if (rideHeightSliderFront.value != rideHeightSliderFront.minValue)
        {
            rideHeightSliderFront.value -= 0.1f;

            rideHeightF -= 0.025f;
            PlayerPrefs.SetFloat("RideHeightFront", rideHeightF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionDistance -= 0.025f;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionDistance -= 0.025f;            
        }
    }

    public void increaseSuspensionStiffnessFront() 
    {
        if (suspensionStiffnessSliderFront.value != suspensionStiffnessSliderFront.maxValue)
        {
            suspensionStiffnessSliderFront.value += 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            
            fr.spring += springAdjust;
            fl.spring += springAdjust;

            suspensionF = suspensionF + springAdjust;
            PlayerPrefs.SetFloat("SpringFront", suspensionF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;

            stiffnessFront.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void decreaseSuspensionStiffnessFront()
    {
        if (suspensionStiffnessSliderFront.value != suspensionStiffnessSliderFront.minValue)
        {
            suspensionStiffnessSliderFront.value -= 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            
            fr.spring -= springAdjust;
            fl.spring -= springAdjust;

            suspensionF = suspensionF - springAdjust;
            PlayerPrefs.SetFloat("SpringFront", suspensionF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            
            stiffnessFront.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void increaseDamperStiffnessFront()
    {
        if (damperStiffnessSliderFront.value != damperStiffnessSliderFront.maxValue)
        {
            damperStiffnessSliderFront.value += 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            
            fr.damper += damperAdjust;
            fl.damper += damperAdjust;

            damperF = damperF + damperAdjust;
            PlayerPrefs.SetFloat("DamperFront", damperF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            
            damperFront.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }

    public void decreaseDamperStiffnessFront()
    {
        if (damperStiffnessSliderFront.value != damperStiffnessSliderFront.minValue)
        {
            damperStiffnessSliderFront.value -= 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            
            fr.damper -= damperAdjust;
            fl.damper -= damperAdjust;

            damperF = damperF - damperAdjust;
            PlayerPrefs.SetFloat("DamperFront", damperF);

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            
            damperFront.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }

    public void increaseRideHeightRear()
    {
        if (rideHeightSliderRear.value != rideHeightSliderRear.maxValue)
        {
            rideHeightSliderRear.value += 0.1f;

            rideHeightR += 0.025f;
            PlayerPrefs.SetFloat("RideHeightRear", rideHeightR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionDistance += 0.025f;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionDistance += 0.025f;
        }
    }

    public void decreseRideHeightRear()
    {
        if (rideHeightSliderRear.value != rideHeightSliderRear.minValue)
        {
            rideHeightSliderRear.value -= 0.1f;

            rideHeightR -= 0.025f;
            PlayerPrefs.SetFloat("RideHeightRear", rideHeightR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionDistance -= 0.025f;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionDistance -= 0.025f;
        }
    }

    public void increaseSuspensionStiffnessRear()
    {
        if (suspensionStiffnessSliderRear.value != suspensionStiffnessSliderRear.maxValue)
        {
            suspensionStiffnessSliderRear.value += 0.1f;
                        
            var rr = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            
            rr.spring += springAdjust;
            rl.spring += springAdjust;

            suspensionR = suspensionR + springAdjust;
            PlayerPrefs.SetFloat("SpringRear", suspensionR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            stiffnessRear.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void decreaseSuspensionStiffnessRear()
    {
        if (suspensionStiffnessSliderRear.value != suspensionStiffnessSliderRear.minValue)
        {
            suspensionStiffnessSliderRear.value -= 0.1f;
                        
            var rr = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            
            rr.spring -= springAdjust;
            rl.spring -= springAdjust;

            suspensionR = suspensionR - springAdjust;
            PlayerPrefs.SetFloat("SpringRear", suspensionR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            stiffnessRear.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void increaseDamperStiffnessRear()
    {
        if (damperStiffnessSliderRear.value != damperStiffnessSliderRear.maxValue)
        {
            damperStiffnessSliderRear.value += 0.1f;
                        
            var rr = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            
            rr.damper += damperAdjust;
            rl.damper += damperAdjust;

            damperR = damperR + damperAdjust;
            PlayerPrefs.SetFloat("DamperRear", damperR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            damperRear.text = "Damper: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }

    public void decreaseDamperStiffnessRear()
    {
        if (damperStiffnessSliderRear.value != damperStiffnessSliderRear.minValue)
        {
            damperStiffnessSliderRear.value -= 0.1f;
                        
            var rr = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring;
            
            rr.damper -= damperAdjust;
            rl.damper -= damperAdjust;

            damperR = damperR - damperAdjust;
            PlayerPrefs.SetFloat("DamperRear", damperR);

            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            damperRear.text = "Damper: " + player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }

    public void SetUpMenuOpen()
    {
        currentSetupMenu = 0;        
        setupButton.SetActive(false);
        driveButton.SetActive(false);
        menuButton.SetActive(false);
        
        setupMenus[currentSetupMenu].SetActive(true);
    }
    public void SetUpMenuClose()
    {
        setupMenus[currentSetupMenu].SetActive(false);        
        setupButton.SetActive(true);
        driveButton.SetActive(true);
        menuButton.SetActive(true);
    }

    public void NextSetup()
    {
        if (currentSetupMenu != setupMenus.Length - 1)
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
