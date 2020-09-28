using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Player, IA}

public class Car : MonoBehaviour
{
    [SerializeField] private Type carType;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 centerOfMass;
    [SerializeField] private AudioManager audioManager;
    //Ruedas
    [Header("Wheels colliders")]
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider backLeft;
    [SerializeField] private WheelCollider backRight;

    private float xInput;
    private float yInput;
    private bool isBreaking;
    private float currentBreakForce;
    private float steerAngle;

    public float MaxSteerAngle { get => maxSteerAngle; set => maxSteerAngle = value; }
    public WheelCollider FrontLeft { get => frontLeft; set => frontLeft = value; }
    public WheelCollider FrontRight { get => frontRight; set => frontRight = value; }
    public float SteerAngle { get => steerAngle; set => steerAngle = value; }
    public float MotorForce { get => motorForce; set => motorForce = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }

    private void Start()
    {
        Rb.centerOfMass = centerOfMass;
    }

    private void FixedUpdate()
    {     
        HandleMotor();
        HandleSteering();       
    }

    private void HandleMotor()
    {
        backLeft.motorTorque = yInput * MotorForce;
        backRight.motorTorque = yInput * MotorForce;
        
        if (isBreaking)
        {
            currentBreakForce = breakForce;          
            audioManager.Play("CarBreak", false);
            Break();
        }
        else
        {
            currentBreakForce = 0;
            Break();           
        }
    }

    private void HandleSteering()
    {
        if (carType == Type.Player)
        {
            SteerAngle = MaxSteerAngle * xInput;
            FrontLeft.steerAngle = SteerAngle;
            FrontRight.steerAngle = SteerAngle;
        } 
        else
        {
            FrontLeft.steerAngle = SteerAngle;
            FrontRight.steerAngle = SteerAngle;
        }
    }

    private void Break()
    {
        FrontLeft.brakeTorque = currentBreakForce;
        FrontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        
    }

    public void GetInput(float x, float y, bool breaking)
    {
        xInput = x;
        yInput = y;
        isBreaking = breaking;
    }
}
