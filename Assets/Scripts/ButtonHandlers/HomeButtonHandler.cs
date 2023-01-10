using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonHandler : MonoBehaviour
{
    // Handle the click on every Home buttons
    public void OnHomeButton_Clicked()
    {
        // Load Menu_Scene
        SceneManager.LoadScene(0);
    }
}
