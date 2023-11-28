using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UIController : MonoBehaviour
{
    public event Action<Vector2> AxisChanged;

    [SerializeField] private RectTransform _stickField;
    [SerializeField] private RectTransform _stick;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchBegan += OnTouchBegan;
        InputManager.Instance.TouchEnded += OnTouchEnded;
        InputManager.Instance.TouchOffsetMoved += OnTouchOffsetMoved;
        AxisChanged += InputManager.Instance.OnUIAxisChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTouchBegan(Vector2 vector2)
    {
        _stickField.gameObject.SetActive(true);
        _stickField.position = vector2;
    }
    void OnTouchOffsetMoved(Vector2 vector2)
    {
        if (vector2.magnitude == 0)
        {
            AxisChanged?.Invoke(Vector2.zero);
            return;
        }
        var fieldRadius = _stickField.rect.height / 2;
        vector2 = Vector2.ClampMagnitude(vector2, fieldRadius);
        _stick.localPosition = vector2;
        var v = vector2 / vector2.magnitude;
        AxisChanged?.Invoke(v);

    }
    void OnTouchEnded(Vector2 vector2)
    {
        _stick.localPosition = Vector3.zero;
        _stickField.gameObject.SetActive(false);
        OnTouchOffsetMoved(Vector2.zero);
    }

    private void OnDisable()
    {
        InputManager.Instance.TouchBegan -= OnTouchBegan;
        InputManager.Instance.TouchEnded -= OnTouchEnded;
        InputManager.Instance.TouchOffsetMoved -= OnTouchOffsetMoved;
        AxisChanged -= InputManager.Instance.OnUIAxisChanged;

    }
}
