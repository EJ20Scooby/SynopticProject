using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private TrackManager trackCheckpoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SimCarController>(out SimCarController player))
        {
            trackCheckpoints.CheckpointPass(this);
        }
    }

    public void SetCheckpoints(TrackManager trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
