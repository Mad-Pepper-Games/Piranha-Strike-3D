using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    [HideInInspector]
    public TapEvent OnTappedPosition = new TapEvent();
    public SwipeEvent OnSwiped = new SwipeEvent();
    [HideInInspector]
    public UnityEvent OnTapped = new UnityEvent();

    public Vector3 firstTouchPosition;
    public Vector3 lastTouchPosition;
    private float dragDetectingDistance;

    public bool IsUsingJoystick;
    private Joystick joystick;
    public Joystick Joystick { get { return (joystick == null) ? joystick = FindObjectOfType<Joystick>() : joystick; } }
    void Start()
    {
        dragDetectingDistance = Screen.height * 10 / 100;
    }

    private void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstTouchPosition = touch.position;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) 
            {
                lastTouchPosition = touch.position;  

                
                if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > dragDetectingDistance || Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y) > dragDetectingDistance)
                {
                    if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y))
                    {   
                        if ((lastTouchPosition.x > firstTouchPosition.x)) 
                        {   
                            SwipeInput(SwipeDir.Right);
                        }
                        else
                        {   
                            SwipeInput(SwipeDir.Left);
                        }
                    }
                    else
                    {   
                        if (lastTouchPosition.y > firstTouchPosition.y) 
                        {   
                            SwipeInput(SwipeDir.Up);
                        }
                        else
                        {  
                            SwipeInput(SwipeDir.Down);
                        }
                    }
                }
                else
                {   
                    TapInput();
                }
            }
            //firstTouchPosition = Vector2.zero;
            lastTouchPosition = Vector2.zero;
        }
    }
    private void SwipeInput(SwipeDir swipeDir)
    {
        if (IsUsingJoystick)
        {
            return;
        }
        OnSwiped.Invoke(swipeDir, firstTouchPosition);
        Debug.Log(swipeDir);
    }

    private void TapInput()
    {
        OnTappedPosition.Invoke(firstTouchPosition);
        OnTapped.Invoke();
        LevelManager.Instance.StartLevel();
        Debug.Log("Tapped");
    }
}

public enum SwipeDir
{
    Up,
    Down,
    Left,
    Right
}

public class SwipeEvent : UnityEvent<SwipeDir, Vector2> { }
public class TapEvent : UnityEvent<Vector2> { }