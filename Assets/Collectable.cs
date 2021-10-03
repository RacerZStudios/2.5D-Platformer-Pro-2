using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private TMP_Text collectText; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Destroy(gameObject);
            AddCollectScore(1); 
        }
    }

    private void AddCollectScore(int amount)
    {
        collectText.text += amount.ToString();  
    }
}
