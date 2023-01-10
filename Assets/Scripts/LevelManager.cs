using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Static instance
    public static LevelManager Instance { get; private set; }

    // Field that saves current player level
    public int currentLevel = 0;
    
    // Singleton init
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Unlocks next level
    public void UnlockNextLevel()
    {
        // If next level number bigger than last already unlocked level - unlock next level
        if (currentLevel + 1 > PlayerPrefs.GetInt("maxCompletedLevel"))
        {
            PlayerPrefs.SetInt("maxCompletedLevel", currentLevel + 1);
        }
    }
}
