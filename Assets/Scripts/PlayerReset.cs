using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField]
    private Vector3 playerStartPos;
    [SerializeField]
    private Transform playerResetPos;
    [SerializeField]
    private bool resetPlayer;
    [SerializeField]
    private CharacterController controller; 
    // Character Controller uses PhysX engine with player controller to move, ovverrides transform.position. 

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KillZ"))
        {
            // calls
            Debug.Log("Reset");
            controller = GetComponent<CharacterController>(); 
            // doesn't call ?
            PlayerReset player = GetComponent<PlayerReset>(); 
            if(player != null)
            {
                controller.enabled = false; // turn off controller 
                player.transform.position = playerResetPos.position; // do translation 
                controller.enabled = true; // turn controller on 
            }
        }
    }
}