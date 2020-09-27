﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABrain : MonoBehaviour,IBoosteable
{
    [SerializeField] private List<Transform> nodes;
    [SerializeField] private float minNodeDistance = 0.1f;
    [SerializeField] private Car car;
    [SerializeField] private float maxspeed = 100;
    [SerializeField] private ParticleSystem boostVFX;

    private float currentSpeed;
    private float horizontalInput;
    private float verticalInput;
    private int nodeIndex = 0;
    private bool hasDirection = false;

    private void Awake()
    {
        boostVFX.Stop();
    }

    private void LateUpdate()
    {
        ApplySteer();
    }

    private void FixedUpdate()
    {       
        Drive();
        ChecktNodeDistance();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[nodeIndex].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * car.MaxSteerAngle;

        car.SteerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * car.FrontLeft.radius * car.FrontLeft.rpm * 60 / 100;

        if (currentSpeed < maxspeed)
        {
            verticalInput = 1;
        }
        else
        {
            verticalInput = 0;
        }
        
        car.GetInput(horizontalInput, verticalInput, false);
    }

    private void ChecktNodeDistance()
    {
        if (Vector3.Distance(transform.position, nodes[nodeIndex].position) <= minNodeDistance)
        {
            if (nodeIndex >= nodes.Count)
            {
                nodeIndex = 0;
            }
            else
            {
                nodeIndex++;
            }

            
        }
    }

    public void Boost(float factor, float time)
    {
        StartCoroutine(GetBoost(factor, time));
        Debug.Log("Getting more speed");
    }

    private IEnumerator GetBoost(float factor, float time)
    {
        float savedSpeed = maxspeed;
        float savedMotor = car.MotorForce;
        car.MotorForce *= 2;
        maxspeed += factor;
        boostVFX.Play();
        yield return new WaitForSeconds(time);
        maxspeed = savedSpeed;
        car.MotorForce = savedMotor;
        boostVFX.Stop();
        yield return null;
    }
}
