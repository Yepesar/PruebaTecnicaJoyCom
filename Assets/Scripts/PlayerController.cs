using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IBoosteable,IJumpeable
{
    [SerializeField] private Car car;
    [SerializeField] private float maxspeed;
    [SerializeField] private ParticleSystem boostVFX;

    private bool breaking = false;
    private bool canAccel = true;
    private float currentSpeed;

    private void Awake()
    {
        boostVFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        SpeedControl();

        if (Input.GetKey(KeyCode.Space))
        {
            breaking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            breaking = false;
        }

        if (canAccel)
        {
            car.GetInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaking);
        }
        else
        {
            car.GetInput(Input.GetAxis("Horizontal"), 0, breaking);
        }       
    }

    private void SpeedControl()
    {
        currentSpeed = 2 * Mathf.PI * car.FrontLeft.radius * car.FrontLeft.rpm * 60 / 100;

        if (currentSpeed < maxspeed)
        {
            canAccel = true;
        }
        else
        {
            canAccel = false;
        }
    }

    public void Boost(float factor, float time)
    {
        StartCoroutine(GetBoost(factor, time));
    }

    private IEnumerator GetBoost(float factor, float time)
    {
        float savedSpeed = maxspeed;
        maxspeed += factor;
        boostVFX.Play();
        float savedMotor = car.MotorForce;
        car.MotorForce *= 2;
        yield return new WaitForSeconds(time);
        maxspeed = savedSpeed;
        car.MotorForce = savedMotor;
        boostVFX.Stop();
        yield return null;
    }

    public void Jump(float jumpForce)
    {
        Vector3 direction = transform.TransformDirection( Vector3.up);
        car.Rb.AddForce(direction * jumpForce, ForceMode.Impulse);
    }
}
