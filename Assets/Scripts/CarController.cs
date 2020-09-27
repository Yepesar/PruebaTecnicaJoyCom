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
    [SerializeField] private Vector3 centerOfMass;

    private float inputX, inputY;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateWheels();
        GeInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion _rot;
            Vector3 _pos;
            wheel.wCollider.GetWorldPose(out _pos, out _rot);
            //wheel.wheelModel.transform.position = _pos;
            wheel.wheelModel.transform.rotation = _rot;
        }
    }

    private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = inputX * turnSensitivity * maxSteerAngle;
                wheel.wCollider.steerAngle = Mathf.Lerp( wheel.wCollider.steerAngle,_steerAngle,0.5f);
            }
        }
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
