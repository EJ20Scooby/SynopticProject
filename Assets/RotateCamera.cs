using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject car;
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        transform.RotateAround(car.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
