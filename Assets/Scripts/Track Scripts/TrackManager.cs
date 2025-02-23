using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private List<Checkpoint> checkpointList;//Store list of all checkpoint objects
    private int nextCheckpointIndex;//Store value of next checkpoint to pass

    public float laptime;
    public float personalBest;
    public float lapDelta;
    public float lastLap;
    private bool startTimer = false;    

    //UI elemtnents for player HUD
    public UnityEngine.UI.Text lapTimer;
    public UnityEngine.UI.Text bestTime;
    public UnityEngine.UI.Text lapDeltaTime;
    public UnityEngine.UI.Text speed;

    private void Update()
    {
        if (startTimer == true)//Lap timer only starts after player has crossed the start checkpoint for the first time
        {
            laptime = laptime + Time.deltaTime;            

            lapTimer.text = "Time: " + laptime.ToString("F2");//Display current lap time to accuracy of 2 decimal points
            bestTime.text = "PB: " + personalBest.ToString("F2");            
        }
        speed.text = FindObjectOfType<SimCarController>().speed.ToString("F0") + "MPH";//Display player speed in MPH
    }

    private void Awake()
    {
        Transform TrackManagerTransform = transform.Find("Checkpoints");//Check number of child checkpoint objects stored in parent object

        checkpointList = new List<Checkpoint>();
        foreach (Transform checkpointTransform in TrackManagerTransform) 
        { 
            Checkpoint checkpoint = checkpointTransform.GetComponent<Checkpoint>();
            checkpoint.SetCheckpoints(this);

            checkpointList.Add(checkpoint);
        }
        nextCheckpointIndex = 0;
    }    

    public void CheckpointPass(Checkpoint checkpoint)
    {
        if(checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            if (checkpointList.IndexOf(checkpoint) == 0)//When player passes starting checkpoint again indicating a full lap has been completed
            {
                lastLap = laptime;//Used to generate value of lap delta
                lapDelta = Mathf.Abs(personalBest - lastLap);//Always produce positive value for time gain/loss on players personal best

                if (lastLap < personalBest)//Determine whether player gained or lost time on personal best and alter the HUD text appropriatly to represent this.
                {
                    lapDeltaTime.text = "Delta: -" + lapDelta.ToString("F2");//Player gained time on personal best
                }
                if (lastLap > personalBest)
                {
                    lapDeltaTime.text = "Delta: +" + lapDelta.ToString("F2");//Player lost time on personale best
                }                

                if (personalBest == 0)//Initialize personal best when first lap is set when personal best had no value
                {
                    personalBest = laptime;
                }
                if (laptime < personalBest)//If player improved on personal best update value for personal best
                {
                    personalBest = laptime;                    
                }               
               
                laptime = 0;//Reset lapTime
                startTimer = true;//Starts timer after crossing starting line for the first time
            }
            Debug.Log("Correct checkpoint");//Check whether checkpoints are being crossed in correct order
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;//Increment nextCheckpointIndex. Determine remainder of checkpoints for when all of been passed
        }
        else//Console message to show checkpoint had been passed in wrong order
        {
            Debug.Log("Wrong checkpoint");
        }
    }
}
