using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheels
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;        
        public Axel axel;
    }

    public ControlMode control;

    //[SerializeField] WheelCollider frontRight;
    //[SerializeField] WheelCollider frontLeft;
    //[SerializeField] WheelCollider rearRight;
    //[SerializeField] WheelCollider rearLeft;

    //[SerializeField] Transform frontRightTransform;
    //[SerializeField] Transform frontLeftTransform;
    //[SerializeField] Transform rearRightTransform;
    //[SerializeField] Transform rearLeftTransform;

    //public Rigidbody rb;
    //public float speed = 10;
    public float maxAcceleration = 600f;
    public float brakingforce = 300f;

    public float turnSensitivity = 1f;
    public float maxTurnAngle = 30f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    //private float currentTurnAngle = 0f;

    public Vector3 _centreOfMass;

    public List<Wheels> wheels;  

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        //carRb.centerOfMass = _centreOfMass;
    }


    void Update()
    {
        GetInputs();
        UpdateWheel();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Steer();
        Brake();

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = currentBrakeForce;
        }
        // //Get fwd/rvrse acceleration for vertical axis (up/down)
        // currentAcceleration = acceleration * Input.GetAxis("Vertical");

        ////Apply brake force
        // if (Input.GetKey(KeyCode.Space)) 
        //   {
        //       currentBrakeForce = brakingforce;
        //   }
        //   else
        //   {
        //       currentBrakeForce = 0f;
        //   }

        ////Apply acceleration rear wheels
        //rearRight.motorTorque = currentAcceleration;
        //rearLeft.motorTorque = currentAcceleration;

        //frontRight.brakeTorque = currentBrakeForce;
        //frontLeft.brakeTorque = currentBrakeForce;
        //rearRight.brakeTorque = currentBrakeForce;
        //rearLeft.brakeTorque = currentBrakeForce;

        //Apply acceleration front wheels
        //frontRight.motorTorque = currentAcceleration;
        //frontLeft.motorTorque = currentAcceleration;

        //frontRight.brakeTorque = currentBrakeForce;
        //frontLeft.brakeTorque = currentBrakeForce;
        //rearRight.brakeTorque = currentBrakeForce;
        //rearLeft.brakeTorque = currentBrakeForce;

        ////Steering
        //currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        //frontLeft.steerAngle = currentTurnAngle;
        //frontRight.steerAngle = currentTurnAngle;

        ////Update Wheel meshes
        //UpdateWheel(frontLeft, frontLeftTransform);
        //UpdateWheel(frontRight, frontRightTransform);
        //UpdateWheel(rearLeft, rearLeftTransform);
        //UpdateWheel(rearRight, rearRightTransform);
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        currentAcceleration = maxAcceleration * Input.GetAxis("Vertical");
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front) //FWD
            {
                wheel.wheelCollider.motorTorque = currentAcceleration;
            }
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front) //FWD
            {
                var _steerAngle = steerInput * turnSensitivity * maxTurnAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakingforce;            
        }
        else
        {
            currentBrakeForce = 0f;            
        }        
    }

    void UpdateWheel() 
    {
        foreach (var wheel in wheels)
        {
            //Get wheel collider state
            Vector3 position;
            Quaternion rotation;
            wheel.wheelCollider.GetWorldPose(out position, out rotation);

            //Set wheel transform state
            wheel.wheelModel.transform.position = position;
            wheel.wheelModel.transform.rotation = rotation;
        }
    }
}
