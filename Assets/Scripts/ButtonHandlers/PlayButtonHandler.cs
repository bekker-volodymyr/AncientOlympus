using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    // Handle the Play button click at Menu Scene
    public void OnPlayButton_Clicked()
    {
        // Load Level Choice Scene
        SceneManager.LoadScene(1);
    }
}
