using System;
using UnityEngine;

public class InputManager : MonoBehaviourSingletonPersistent<InputManager>
{
    public event Action<Vector2> TouchBegan;
    public event Action<Vector2> TouchMoved;
    public event Action<Vector2> TouchOffsetMoved;
    public event Action<Vector2> TouchStationary;
    public event Action<Vector2> TouchEnded;
    public event Action<Vector2> NormalizedMoved;

    private Touch _touch = new();
    private Vector2 _offset = new();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DetectTouch();
    }

    private void DetectTouch()
    {
        if (Input.touchCount == 0)
            return;

        _touch = Input.GetTouch(0);
        var first = _touch.position;
        _offset += _touch.deltaPosition;
        var current = first + _offset;

        switch (_touch.phase)
        {
            case TouchPhase.Began:
                TouchBegan?.Invoke(first);
                break;
            case TouchPhase.Moved:
                TouchOffsetMoved?.Invoke(_offset);
                TouchMoved?.Invoke(current);
                //TODO:令其功能類似getaxis
                NormalizedMoved?.Invoke(current/current.magnitude);
                break;
            case TouchPhase.Stationary:
                TouchStationary?.Invoke(current);
                break;
            case TouchPhase.Ended:
                _offset = Vector2.zero;
                TouchEnded?.Invoke(current);
                break;
            default:
                break;
        }
    }

}