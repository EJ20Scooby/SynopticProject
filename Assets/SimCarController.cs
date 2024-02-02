using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static CarMovement;



public class SimCarController : MonoBehaviour
{
    private Rigidbody carRB;
    private float speedClamped;

    public WheelColliders colliders;
    public WheelMeshes meshes;

    private float throttleInput;
    private float brakeInput;
    private float steerInput;
    private bool handBrakeInput;    

    public float motorPower;
    public float brakePower;
    public float handBrakePower;
    public AnimationCurve steeringCurve;       
    public float slipAngle;

    public float speed;
    public float maxSpeed;
    public float steerSpeed;

    public int isEngineRunning;
    public bool EngineRunning = false;

    private void Start()
    {
        carRB = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        speed = ((colliders.RRWheel.rotationSpeed + colliders.RLWheel.rotationSpeed + colliders.FRWheel.rotationSpeed + colliders.FLWheel.rotationSpeed) / 4) * colliders.RRWheel.radius * Mathf.PI / 30;
        speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);
        steerSpeed = carRB.velocity.magnitude; 
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        ApplyHandBrake();
        UpdateWheels();
    }

    void CheckInput() 
    {
        throttleInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(throttleInput) > 0 && isEngineRunning ==0)
        {
            StartCoroutine(StartEngine());
        }
        steerInput = Input.GetAxis("Horizontal");
        handBrakeInput = Input.GetKey(KeyCode.Space);
        slipAngle = Vector3.Angle(transform.forward, carRB.velocity-transform.forward);        

        if (slipAngle < 120f)
        {
            if (throttleInput < 0)
            {
                brakeInput = Mathf.Abs(throttleInput);
                throttleInput = 0.0f;
            }            
        }
        else
        {
            brakeInput = 0.0f;
        }
    }

    void ApplyMotor()
    {
        if (isEngineRunning > 1)
        {
            if (speed < maxSpeed)
            {
                //RWD
                colliders.RRWheel.motorTorque = motorPower * throttleInput;
                colliders.RLWheel.motorTorque = motorPower * throttleInput;
            }
            if (speed > maxSpeed)
            {
                colliders.RRWheel.motorTorque = 0;
                colliders.RLWheel.motorTorque = 0;
            }
        }
    }

    public IEnumerator StartEngine()
    {
        isEngineRunning = 1;
        yield return new WaitForSeconds(0.6f);
        EngineRunning = true;
        yield return new WaitForSeconds(0.4f);
        isEngineRunning = 2;
    }

    void ApplyBrake()
    {
        //Braking is front biased 70% - 30% division
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }

    void ApplyHandBrake()
    {
        if(handBrakeInput == true)
        {
            colliders.RRWheel.brakeTorque = handBrakePower * 1.0f;
            colliders.RLWheel.brakeTorque = handBrakePower * 1.0f;
        }
    }

    void ApplySteering()
    {
        float steeringAngle = steerInput * steeringCurve.Evaluate(steerSpeed);
        //if (slipAngle < 120f)
        //{
        //    steeringAngle += Vector3.SignedAngle(transform.forward, carRB.velocity + transform.forward, Vector3.up);
        //}
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }

    void UpdateWheels()
    {
        UpdateWheel(colliders.FRWheel, meshes.FRWheel);
        UpdateWheel(colliders.FLWheel, meshes.FLWheel);
        UpdateWheel(colliders.RRWheel, meshes.RRWheel);
        UpdateWheel(colliders.RLWheel, meshes.RLWheel);
    }

    void UpdateWheel(WheelCollider col, MeshRenderer wheelMesh)
    {
        //Get wheel collider state
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        //Set wheel transform state
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = rotation;
    }

    public float GetSpeedRatio()
    {
        var gas = Mathf.Clamp(Mathf.Abs(throttleInput), 0.5f, 1f);
        return speedClamped * gas / maxSpeed;
    }
}

[Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}

[Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}