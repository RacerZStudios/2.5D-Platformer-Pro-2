using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private TMP_Text collectText;
    [SerializeField]
    private int dispScore;
    [SerializeField]
    private Collectable_Manager cM;

    private void Start()
    {
        if (cM != null)
        {
            cM = GameObject.Find("Collectable_Text").GetComponent<Collectable_Manager>();
        }

        if (cM == null)
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            cM.Collected();
            Destroy(gameObject);
        }
    }
}
