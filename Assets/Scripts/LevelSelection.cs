using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // Array of level buttons
    [SerializeField] private Button[] _lvlButtons;

    // Every time on awake checks max unlocked level and enabling buttons
    private void Awake()
    {
        // Get max unlocked level from PlayerPrefs
        // If not set to 1
        int maxCompletedLevel = PlayerPrefs.GetInt("maxCompletedLevel", 1);

        // Check every button is it`s level unlocked
        // If unlocked - enable and hide lock image
        // If locked - unenable and show lock image
        for (int i = 0; i < _lvlButtons.Length; i++)
        {
            if (i + 1 > maxCompletedLevel)
            {
                _lvlButtons[i].interactable = false;
                if(i > 0)
                    _lvlButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
            else
            {
                _lvlButtons[i].interactable = true;
                if(i > 0)
                    _lvlButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
        }
    }

    // Handle click on level buttons
    public void OnLevelButton_Clicked(int levelNumber)
    {
        // Set current level to new level
        LevelManager.Instance.currentLevel = levelNumber;

        // Load level scene
        SceneManager.LoadScene(2);
    }
}
