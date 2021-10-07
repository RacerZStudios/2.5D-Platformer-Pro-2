using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Collectable_Manager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text collectText;
    [SerializeField]
    private int dispScore;

    public bool Collected()
    {
        GetCollected(1);
        return true;
    }

    public void GetCollected(int score)
    {
        dispScore += score;
        collectText.text = "Collectable: " + dispScore; 
    }

    private void Update()
    {
        if(dispScore >= 5)
        {
            SceneManager.LoadSceneAsync(2); 
        }
    }
}