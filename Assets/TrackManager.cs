using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private List<Checkpoint> checkpointList;
    private int nextCheckpointIndex;

    public float laptime;
    private bool startTimer = false;

    public UnityEngine.UI.Text lapTimer;

    private void Update()
    {
        if (startTimer == true)
        {
            laptime = laptime + Time.deltaTime;

            lapTimer.text = "Time: " + laptime.ToString("F2");
        }
    }

    private void Awake()
    {
        Transform TrackManagerTransform = transform.Find("Checkpoints");

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
            if (checkpointList.IndexOf(checkpoint) == 0)
            {
                laptime = 0;
                startTimer = true;
            }
            Debug.Log("Correct checkpoint");
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
        }
        else
        {
            Debug.Log("Wrong checkpoint");
        }
    }
}
