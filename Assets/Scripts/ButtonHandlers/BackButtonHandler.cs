using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    // Handle the click on back button at LevelChoice Scene
    public void OnBackButton_Clicked()
    {
        // Load menu scene
        SceneManager.LoadScene(0);
    }
}
