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

    public float brakeFront;
    public float brakeRear;

    public float motorPower;
    public float brakePower;
    public int brakeBiasLevel;
    public float handBrakePower;
    public AnimationCurve steeringCurve;       
    public float slipAngle;

    public float speed;
    public float maxSpeed;
    public float steerSpeed;
    private float mphConversion = 2.237f;

    public int isEngineRunning;
    public bool EngineRunning = false;
    public bool stabilityControl = true;

    private void Start()
    {
        brakePower = PlayerPrefs.GetFloat("BrakePower");
        brakeBiasLevel = PlayerPrefs.GetInt("BrakeBias");
        maxSpeed = PlayerPrefs.GetFloat("TopSpeed");

        colliders.FLWheel.suspensionDistance = PlayerPrefs.GetFloat("RideHeightFront");
        colliders.FRWheel.suspensionDistance = PlayerPrefs.GetFloat("RideHeightFront");
        colliders.RLWheel.suspensionDistance = PlayerPrefs.GetFloat("RideHeightRear");
        colliders.RRWheel.suspensionDistance = PlayerPrefs.GetFloat("RideHeightRear");

        var FL = colliders.FLWheel.suspensionSpring;
        var FR = colliders.FLWheel.suspensionSpring;
        var RL = colliders.FLWheel.suspensionSpring;
        var RR = colliders.FLWheel.suspensionSpring;

        FL.spring = PlayerPrefs.GetFloat("SpringFront");
        FR.spring = PlayerPrefs.GetFloat("SpringFront");
        RL.spring = PlayerPrefs.GetFloat("SpringRear");
        RR.spring = PlayerPrefs.GetFloat("SpringRear");              

        FL.damper = PlayerPrefs.GetFloat("DamperFront");
        FR.damper = PlayerPrefs.GetFloat("DamperFront");
        RL.damper = PlayerPrefs.GetFloat("DamperRear");
        RR.damper = PlayerPrefs.GetFloat("DamperRear");

        colliders.FLWheel.suspensionSpring = FL;
        colliders.FRWheel.suspensionSpring = FR;
        colliders.RLWheel.suspensionSpring = RL;
        colliders.RRWheel.suspensionSpring = RR;

        carRB = GetComponent<Rigidbody>();
        //maxSpeed = maxSpeed * mphConversion;
        SetBrakeBias();
    }

    void Update()
    {
        //speed = ((colliders.RRWheel.rotationSpeed + colliders.RLWheel.rotationSpeed + colliders.FRWheel.rotationSpeed + colliders.FLWheel.rotationSpeed) / 4) * colliders.RRWheel.radius * Mathf.PI / 30;
        speed = carRB.velocity.magnitude * mphConversion;//Convert to MPH        
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
            else
            {
                brakeInput = 0.0f;
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

    public void SetBrakeBias()
    {
        if(brakeBiasLevel == 0)
        {
            brakeFront = 0.0f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 1)
        {
            brakeFront = 0.1f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 2)
        {
            brakeFront = 0.2f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 3)
        {
            brakeFront = 0.3f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 4)
        {
            brakeFront = 0.4f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 5)
        {
            brakeFront = 0.5f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 6)
        {
            brakeFront = 0.6f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 7)
        {
            brakeFront = 0.7f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 8)
        {
            brakeFront = 0.8f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 9)
        {
            brakeFront = 0.9f;
            brakeRear = 1.0f - brakeFront;
        }
        if (brakeBiasLevel == 10)
        {
            brakeFront = 1.0f;
            brakeRear = 1.0f - brakeFront;
        }
    }

    void ApplyBrake()
    {
        //Braking is front biased 70% - 30% division
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * brakeFront;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * brakeFront;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower * brakeRear;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * brakeRear;
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
        if (slipAngle < 120f && stabilityControl == true)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, carRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }

    public void setStabilityControl()
    {
        if (stabilityControl != true)
        {
            stabilityControl = true;
        }
        else
        {
            stabilityControl = false;
        }
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