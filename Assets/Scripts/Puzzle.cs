using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    // Field for inctance of all puzzles
    [SerializeField] private Puzzles _puzzles;

    // Collider of current part
    [SerializeField] private Collider2D _collider;

    // Current position of part
    public Vector2 MainPos { get; set; }

    // Boolean variable to check if this part is touched
    private bool moveAllowed = false;

    private void Start()
    {
        // Set main position to current position
        MainPos = transform.position;

        // Get collider for current part
        _collider = GetComponent<Collider2D>();

        // Get puzzles instance from GameObject
        _puzzles = GameObject.Find("Puzzles").GetComponent<Puzzles>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player touches screen start other checks
        if (Input.touchCount > 0)
        {
            // Save the touch object and position of current touch
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            // If touch just began check the collider of object that touced
            // If true - allow to move current parts and move it upfront of other parts
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if (_collider == touchedCollider)
                {
                    moveAllowed = true;
                    transform.SetAsLastSibling();
                }
            }

            // If touch is moving check if moving is allowed
            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    // Save moving puzzle delta to variable
                    Vector2 delta = new Vector2(touchPos.x - MainPos.x, touchPos.y - MainPos.y);

                    Debug.Log($"{delta.x}, {delta.y}");

                    // Check if abs of delta.x bigger of lesser of abs of delta.y
                    // If bigger - means player move part on the x axis
                    // If lessser - means player move part on the y axis
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        // Check if delta.x bigger or lesser than 0
                        // If bigger - means player moving part at right side
                        // If lesser - means player moving part at left side
                        if (delta.x > 0)
                        {
                            // If current part is in the right column - return
                            if (
                                _puzzles.parts2dArray[0, 3].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[1, 3].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[2, 3].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[3, 3].GetComponent<Puzzle>() == this
                                )
                            {
                                return;
                            }

                            // If delta.x bigger than 123 - swap it with next part
                            // Else - just move on the x axis with the touch position
                            if (delta.x > 123)
                            {
                                // Cancel touch tracking
                                touch.phase = TouchPhase.Canceled;
                                //Debug.Log($"{transform.position.x}, {transform.position.y}\n{mainPos.x}, {mainPos.y}");
                                // Search for current part in parts array and swap with the next
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (_puzzles.parts2dArray[i, j].GetComponent<Puzzle>() == this)
                                        {
                                            // Swap parts
                                            _puzzles.SwapParts(i, j, i, j + 1, false);

                                            moveAllowed = false;
                                            break;
                                        }
                                    }
                                    if (!moveAllowed) break;
                                }
                            }
                            else
                            {
                                transform.position = new Vector2(touchPos.x, MainPos.y);
                            }
                        }
                        else if (delta.x < 0)
                        {
                            // If current part is in the left column - return
                            if (
                                _puzzles.parts2dArray[0, 0].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[1, 0].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[2, 0].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[3, 0].GetComponent<Puzzle>() == this
                                )
                            {
                                return;
                            }

                            // If delta.x lesser than -123 - swap it with prev part
                            // Else - just move on the x axis with the touch position
                            if (delta.x < -123)
                            {
                                // Cancel touch tracking
                                touch.phase = TouchPhase.Canceled;

                                // Search for current part in parts array and swap with the prev
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 1; j < 4; j++)
                                    {
                                        if (_puzzles.parts2dArray[i, j].GetComponent<Puzzle>() == this)
                                        {
                                            // Swap parts
                                            _puzzles.SwapParts(i, j, i, j - 1, false);

                                            moveAllowed = false;
                                            break;
                                        }
                                    }
                                    if (!moveAllowed) break;
                                }
                            }
                            else
                            {
                                transform.position = new Vector2(touchPos.x, MainPos.y);
                            }
                        }
                    }
                    else if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                    {
                        // Check if delta.y bigger or lesser than 0
                        // If bigger - means player moving part up
                        // If lesser - means player moving part down
                        if (delta.y > 0)
                        {
                            // If current part is in the uppest column - return
                            if(
                                _puzzles.parts2dArray[0, 0].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[0, 1].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[0, 2].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[0, 3].GetComponent<Puzzle>() == this)
                            {
                                return;
                            }

                            // If delta.y bigger than 123 - swap it with upper part
                            // Else - just move on the y axis with the touch position
                            if (delta.y > 123)
                            {
                                // Cancel touch tracking
                                touch.phase = TouchPhase.Canceled;

                                // Search for current part in parts array and swap with the upper
                                for (int i = 1; i < 4; i++)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (_puzzles.parts2dArray[i, j].GetComponent<Puzzle>() == this)
                                        {
                                            // Swap parts
                                            _puzzles.SwapParts(i, j, i - 1, j, false);

                                            moveAllowed = false;
                                            break;
                                        }
                                    }
                                    if (!moveAllowed) break;
                                }
                            }
                            else
                            {
                                transform.position = new Vector2(MainPos.x, touchPos.y);
                            }
                        }
                        else if (delta.y < 0)
                        {
                            // If current part is in the downest column - return
                            if (
                                _puzzles.parts2dArray[3, 0].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[3, 1].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[3, 2].GetComponent<Puzzle>() == this ||
                                _puzzles.parts2dArray[3, 3].GetComponent<Puzzle>() == this)
                            {
                                return;
                            }

                            // If delta.y lesser than -123 - swap it with downer part
                            // Else - just move on the y axis with the touch position
                            if (delta.y < -123)
                            {
                                // Cancel touch tracking
                                touch.phase = TouchPhase.Canceled;

                                // Search for current part in parts array and swap with the downer
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (_puzzles.parts2dArray[i, j].GetComponent<Puzzle>() == this)
                                        {
                                            // Swap parts
                                            _puzzles.SwapParts(i, j, i + 1, j, false);

                                            moveAllowed = false;
                                            break;
                                        }
                                    }
                                    if (!moveAllowed) break;
                                }
                            }
                            else
                            {
                                transform.position = new Vector2(MainPos.x, touchPos.y);
                            }
                        }
                    }
                }
            }

            if(touch.phase == TouchPhase.Canceled)
            {
                _puzzles.IsWin();
            }

            // If touch is ended than return part to main postition and stop moving
            if (touch.phase == TouchPhase.Ended)
            {
                if (moveAllowed)
                {
                    moveAllowed = false;
                    transform.position = MainPos;
                    transform.SetSiblingIndex(5);
                }
            }
        }
    }
}
