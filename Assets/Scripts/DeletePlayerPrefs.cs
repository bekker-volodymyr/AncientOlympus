using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour
{
    void Update()
    {
        // If touch count more than 2 - remove all PlayerPrefs
        if (Input.touchCount > 2)
        {
            Debug.Log("Delete prefs");
            PlayerPrefs.DeleteAll();
        }
    }
}
