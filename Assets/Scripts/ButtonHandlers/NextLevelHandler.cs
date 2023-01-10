using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelHandler : MonoBehaviour
{
    // Handle the click on Next Scene button at Level Completion Scene
    public void OnNextLevelButton_Clicked()
    {
        // If current level is last level set current level to 1
        // Else increase current level
        if (LevelManager.Instance.currentLevel == 6)
        {
            LevelManager.Instance.currentLevel = 1;
        }
        else
        {
            LevelManager.Instance.currentLevel++;
        }

        // Load Level Scene
        SceneManager.LoadScene(2);
    }
}
