using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Axel
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject wheelModel;
    public WheelCollider wCollider;
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 20f;
    [SerializeField] private float turnSensitivity = 1f;
    [SerializeField] private float maxSteerAngle = 45f;
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speedMultiplicator = 500;

    private float inputX, inputY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GeInputs();
    }

    private void LateUpdate()
    {
        Move();
    }

    private void GeInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wCollider.motorTorque = inputY * maxAcceleration * speedMultiplicator * Time.deltaTime;
        }
    }
}
