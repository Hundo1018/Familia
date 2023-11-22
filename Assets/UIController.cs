using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform _stickField;
    [SerializeField] private RectTransform _stick;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchBegan += OnTouchBegan;
        InputManager.Instance.TouchEnded += OnTouchEnded;
        InputManager.Instance.TouchOffsetMoved += OnTouchOffsetMoved;
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
        //XXX:這裡應該使用localPosition與Field邊界範圍來設定
        //HACK:這是硬算的 因為圖片40*40
        var temp = Mathf.Pow(_stickField.rect.height, 2) / 1000;
        vector2 = Vector2.ClampMagnitude(vector2,temp);
        _stick.localPosition = vector2;
    }
    void OnTouchEnded(Vector2 vector2)
    {
        _stick.localPosition = Vector3.zero;
        _stickField.gameObject.SetActive(false);
    }
}
