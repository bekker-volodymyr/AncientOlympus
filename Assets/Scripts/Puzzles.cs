using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzles : MonoBehaviour
{
    // Serialize fields for images that changes thair sprites in dependence of level number
    [SerializeField] private Image _background;
    [SerializeField] private Image _result;
    [SerializeField] private List<Image> _parts;

    // 2d array for shuffling puzzles
    [SerializeField] public Image[,] parts2dArray = new Image[4, 4];

    // Constant array of anchored positions every single part
    public static readonly float[,] pos = 
    {
        {-370f, 555f },
        {-122f, 555f },
        {122.5f, 555f },
        {367.5f, 555f },
        {-370f, 311f },
        {-122f, 311f },
        {122.5f, 311f },
        {367.5f, 311f },
        {-370, 66f },
        {-122f, 66f },
        {122.5f, 66f },
        {367.5f, 66f },
        {-370f, -178f },
        {-122f, -178f },
        {122.5f, -178f },
        {367.5f, -178f }
    };

    // Stack with snapshots of puzzle field for undo function
    private Stack<SwapSnapshot> snapshots = new Stack<SwapSnapshot>();

    void Awake()
    {
        // Loading sprites for current level
        LoadSprites();

        // Fill and shuffle 2d array of parts
        Shuffle();
    }

    private void LoadSprites()
    {
        // Load right sprites for parts
        for (int i = 0; i < _parts.Count; i++)
            _parts[i].GetComponent<Image>().sprite = Resources.Load<Sprite>($"Level{LevelManager.Instance.currentLevel}/Parts/{i}");

        // Load right sprites for background and result picture
        _background.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Level{LevelManager.Instance.currentLevel}/background");
        _result.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Level{LevelManager.Instance.currentLevel}/result");
    }

    private void Shuffle()
    {
        // List of used indexes
        List<int> indexes = new List<int>();

        // Counter for positions array
        int posIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // Choose random part from _parts list
                int partIndex = Random.Range(0, 16);
                while (indexes.Contains(partIndex))
                    partIndex = Random.Range(0, 16);

                // Save chosen index
                indexes.Add(partIndex);

                // Add chosen random part to 2d array and set position
                parts2dArray[i, j] = _parts[partIndex];
                parts2dArray[i, j].GetComponent<RectTransform>().anchoredPosition = new Vector2(pos[posIndex, 0], pos[posIndex, 1]);

                // Increase position counter
                posIndex++;
            }
        }
    }

    public void IsWin()
    {
        // Check is anchored position of each part is equal to right position
        // If not - return
        for (int i = 0; i < _parts.Count; i++)
        {
            if (_parts[i].GetComponent<RectTransform>().anchoredPosition.x != pos[i, 0] || _parts[i].GetComponent<RectTransform>().anchoredPosition.y != pos[i, 1])
                return;
        }

        // Unlock next level in PlayerPrefs
        LevelManager.Instance.UnlockNextLevel();
        
        // Load level completion scene
        SceneManager.LoadScene(3);
    }

    public void SwapParts(int fromX, int fromY, int toX, int toY, bool isUndo)
    {
        // Swap positions of parts
        Vector2 newPos = parts2dArray[toX, toY].GetComponent<Transform>().position;
        parts2dArray[toX, toY].GetComponent<Transform>().position = new Vector2(parts2dArray[fromX, fromY].GetComponent<Puzzle>().MainPos.x, parts2dArray[fromX, fromY].GetComponent<Puzzle>().MainPos.y);
        parts2dArray[toX, toY].GetComponent<Puzzle>().MainPos = new Vector2(parts2dArray[fromX, fromY].GetComponent<Puzzle>().MainPos.x, parts2dArray[fromX, fromY].GetComponent<Puzzle>().MainPos.y);
        parts2dArray[fromX, fromY].GetComponent<Transform>().transform.position = new Vector2(newPos.x, newPos.y);
        parts2dArray[fromX, fromY].GetComponent<Puzzle>().MainPos = new Vector2(newPos.x, newPos.y);

        // Swap positions of parts in array
        var temp = parts2dArray[toX, toY];
        parts2dArray[toX, toY] = parts2dArray[fromX, fromY];
        parts2dArray[fromX, fromY] = temp;

        // If this swap wasn`t the undo action - make snapshot
        if(!isUndo)
            MakeSnapshot(fromX, fromY, toX, toY);
    }

    public void MakeSnapshot(int fromX, int fromY, int toX, int toY)
    {
        // Push the new snapshot to snapshots stack
        snapshots.Push(new SwapSnapshot(fromX, fromY, toX, toY));
    }

    public void Undo()
    {
        // If stack isn`t empty
        if (snapshots.Count > 0)
        {
            // Pop last snapshot
            SwapSnapshot lastSnapshot = snapshots.Pop();
            // Do parts swap
            SwapParts(lastSnapshot.OldIndexes[0], lastSnapshot.OldIndexes[1], lastSnapshot.NewIndexes[0], lastSnapshot.NewIndexes[1], true);
        }
    }
}
