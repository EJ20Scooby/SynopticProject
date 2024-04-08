using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Setup : MonoBehaviour
{
    [SerializeField] GameObject player;
    SimCarController carSettings;

    [SerializeField] UnityEngine.UI.Slider finalDriveSlider;
    [SerializeField] Text topSpeed;

    [SerializeField] UnityEngine.UI.Slider rideHeightSlider;

    [SerializeField] UnityEngine.UI.Slider suspensionStiffnessSlider;
    [SerializeField] Text stiffness;
    private float springAdjust = 5000.0f;

    [SerializeField] UnityEngine.UI.Slider damperStiffnessSlider;
    [SerializeField] Text damper;
    private float damperAdjust = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        finalDriveSlider.value = 0.5f;
        finalDriveSlider.maxValue = 1.0f;
        finalDriveSlider.minValue = 0.0f;
        topSpeed.text = "Top Speed: " + player.GetComponent<SimCarController>().maxSpeed + "MPH";

        rideHeightSlider.value = 0.5f;
        rideHeightSlider.maxValue = 1.0f;
        rideHeightSlider.minValue = 0.0f;

        suspensionStiffnessSlider.value = 0.5f;
        suspensionStiffnessSlider.maxValue = 1.0f;
        suspensionStiffnessSlider.minValue = 0.0f;
        stiffness.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";

        damperStiffnessSlider.value = 0.5f;
        damperStiffnessSlider.maxValue = 1.0f;
        damperStiffnessSlider.minValue = 0.0f;
        damper.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
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

    public void increaseRideHeight()
    {
        if (rideHeightSlider.value != rideHeightSlider.maxValue)
        {
            rideHeightSlider.value += 0.1f;
            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionDistance += 0.025f;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionDistance += 0.025f;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionDistance += 0.025f;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionDistance += 0.025f;
        }
    }

    public void decreseRideHeight()
    {
        if (rideHeightSlider.value != rideHeightSlider.minValue)
        {
            rideHeightSlider.value -= 0.1f;
            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionDistance -= 0.025f;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionDistance -= 0.025f;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionDistance -= 0.025f;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionDistance -= 0.025f;
        }
    }

    public void increaseSuspensionStiffness() 
    {
        if (suspensionStiffnessSlider.value != suspensionStiffnessSlider.maxValue)
        {
            suspensionStiffnessSlider.value += 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            fr.spring += springAdjust;
            fl.spring += springAdjust;
            rr.spring += springAdjust;
            rl.spring += springAdjust;

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            stiffness.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void decreaseSuspensionStiffness()
    {
        if (suspensionStiffnessSlider.value != suspensionStiffnessSlider.minValue)
        {
            suspensionStiffnessSlider.value -= 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            fr.spring -= springAdjust;
            fl.spring -= springAdjust;
            rr.spring -= springAdjust;
            rl.spring -= springAdjust;

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            stiffness.text = "Stiffness: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.spring + "N/m";
        }
    }

    public void increaseDamperStiffness()
    {
        if (damperStiffnessSlider.value != damperStiffnessSlider.maxValue)
        {
            damperStiffnessSlider.value += 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            fr.damper += damperAdjust;
            fl.damper += damperAdjust;
            rr.damper += damperAdjust;
            rl.damper += damperAdjust;

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            damper.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }

    public void decreaseDamperStiffness()
    {
        if (damperStiffnessSlider.value != damperStiffnessSlider.minValue)
        {
            damperStiffnessSlider.value -= 0.1f;

            var fr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var fl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rr = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            var rl = player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring;
            fr.damper -= damperAdjust;
            fl.damper -= damperAdjust;
            rr.damper -= damperAdjust;
            rl.damper -= damperAdjust;

            player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring = fr;
            player.GetComponent<SimCarController>().colliders.FLWheel.suspensionSpring = fl;
            player.GetComponent<SimCarController>().colliders.RRWheel.suspensionSpring = rr;
            player.GetComponent<SimCarController>().colliders.RLWheel.suspensionSpring = rl;
            damper.text = "Damper: " + player.GetComponent<SimCarController>().colliders.FRWheel.suspensionSpring.damper + "N-s/m2";
        }
    }
}
