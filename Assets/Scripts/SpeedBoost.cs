using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedFactor = 100;
    [SerializeField] private float boostTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        IBoosteable obj = other.GetComponent<IBoosteable>();
        if (obj !=null)
        {
            obj.Boost(speedFactor,boostTime);
            other.gameObject.GetComponentInChildren<AudioManager>().Play("CarBoost", false);
        }

        gameObject.SetActive(false);
    }
}
