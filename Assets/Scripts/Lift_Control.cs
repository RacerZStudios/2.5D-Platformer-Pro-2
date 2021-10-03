using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_Control : MonoBehaviour
{
    [SerializeField]
    private Transform liftPos;
    [SerializeField]
    private Vector3 liftUpPos;
    [SerializeField]
    private Vector3 liftDownPos; 
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                other.gameObject.transform.position = liftPos.position;
                liftPos.position += Vector3.down * 450 / 300;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                other.gameObject.transform.position = liftPos.position;
                liftPos.position -= Vector3.down * 450 / 300;
            }
        }
    }
}
