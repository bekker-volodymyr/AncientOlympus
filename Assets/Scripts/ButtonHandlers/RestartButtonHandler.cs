using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonHandler : MonoBehaviour
{
    // Handle the Restart button click
    public void OnRestartButton_Clicked()
    {
        // Load Level Scene
        SceneManager.LoadScene(2);
    }
}
