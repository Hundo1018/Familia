using System;
using UnityEngine;

public class InputManager : MonoBehaviourSingletonPersistent<InputManager>
{
    /// <summary>
    /// 點下去
    /// </summary>
    public event Action<Vector2> TouchBegan;
    /// <summary>
    /// 按住移動
    /// </summary>
    public event Action<Vector2> TouchMoved;
    /// <summary>
    /// 自從按下之後的位移量
    /// </summary>
    public event Action<Vector2> TouchOffsetMoved;
    /// <summary>
    /// 停留
    /// </summary>
    public event Action<Vector2> TouchStationary;
    /// <summary>
    /// 離開
    /// </summary>
    public event Action<Vector2> TouchEnded;
    /// <summary>
    /// 移動向量0~1(根據虛擬搖桿)
    /// </summary>
    public event Action<Vector2> NormalizedStickMoved;
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
    public void OnUIAxisChanged(Vector2 vector2){
        NormalizedStickMoved?.Invoke(vector2);
    }
    private void DetectTouch()
    {
        if (Input.touchCount == 0)
            return;

        _touch = Input.GetTouch(0);
        var first = _touch.position;
        _offset += _touch.deltaPosition;
        var current = first + _offset;
        // Debug.Log($"f:{first},o:{_offset},c:{current}");
        switch (_touch.phase)
        {
            case TouchPhase.Began:
                TouchBegan?.Invoke(first);
                break;
            case TouchPhase.Moved:
                TouchOffsetMoved?.Invoke(_offset);
                TouchMoved?.Invoke(current);
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