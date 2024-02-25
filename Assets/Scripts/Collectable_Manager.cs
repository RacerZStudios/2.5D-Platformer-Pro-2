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
    [SerializeField]
    private GameObject winTeleprefab;
    [SerializeField]
    private Transform winteleTransformPos; 

    public bool Collected()
    {
        GetCollected(1);
        return true;
    }

    public void GetCollected(int score)
    {
        dispScore += score;
        collectText.text = "Collectable: " + dispScore;

        if (dispScore > 5 || dispScore == 6)
        {
            IsWinMet(true);
        }
    }

    private bool IsWinMet(bool met)
    {
        if (winTeleprefab != null)
        {
            Instantiate(winTeleprefab, winteleTransformPos.position, Quaternion.identity);
            return met;  
        }
        return false; 
    }
}