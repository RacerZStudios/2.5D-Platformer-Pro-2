using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 handPos;
    [SerializeField]
    private Vector3 standPos; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LedgeCheck"))
        {
            var player =  other.transform.parent.GetComponent<PlayerController>(); 

            if(player != null)
            {
                player.GrabLedge(handPos, this); 
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return standPos; 
    }
}
