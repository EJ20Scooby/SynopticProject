using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private List<Checkpoint> checkpointList;
    private int nextCheckpointIndex;

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
            Debug.Log("Correct checkpoint");
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
        }
        else
        {
            Debug.Log("Wrong checkpoint");
        }
    }
}
