using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100;
   
    private void OnTriggerEnter(Collider other)
    {
        IJumpeable obj = other.GetComponent<IJumpeable>();
        if (obj != null)
        {
            obj.Jump(jumpForce);
            other.gameObject.GetComponentInChildren<AudioManager>().Play("CarJump", false);         
        }
    }
}
