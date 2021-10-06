using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZ : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform playerResetPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            player.transform.position = new Vector3(0, 100, 0);
        }
    }
}