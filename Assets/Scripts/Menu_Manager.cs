using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
        if(Application.isPlaying)
        {
            Application.Quit();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu(); 
        }
    }
}
