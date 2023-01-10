using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplitionSpritesManager : MonoBehaviour
{
    // Instances of images that need sprite from Resources
    [SerializeField] private Image _background;
    [SerializeField] private Image _character;

    void Start()
    {
        // Load right sprites for background and result picture
        _background.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Level{LevelManager.Instance.currentLevel}/background");
        _character.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Level{LevelManager.Instance.currentLevel}/complete_character");
    }
}
