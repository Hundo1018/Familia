using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviourEntity
{
    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private bool _isHolding;
    [SerializeField] private HomeBeastController _homeBeast;
    /// <summary>
    /// 移動方向
    /// </summary>
    private Vector2 _moveDirection;
    /// <summary>
    /// 駕駛移動中
    /// </summary>
    public event Action<Vector2> Driving;
    /// <summary>
    /// 這個玩家是否正在駕駛
    /// </summary>
    public event Action<bool> DriveStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchEnded += OnShot;
        InputManager.Instance.TouchMoved += OnHold;
        InputManager.Instance.NormalizedStickMoved += OnSetMoveDirection;
        DriveStateChanged?.Invoke(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    /// <summary>
    /// 處理移動中向反方向射擊
    /// </summary>
    /// <param name="vector2"></param>
    void OnHold(Vector2 vector2)
    {
        _isHolding = true;
    }
    /// <summary>
    /// 射
    /// </summary>
    /// <param name="target"></param>
    void OnShot(Vector2 target)
    {
        target = Camera.main.ScreenToWorldPoint(target);
        if (_isHolding)
            target = -target;
        _isHolding = false;
        _projectilePool.Get(new EffectPacket(this) { Damage = 3 }, target, 0.6f, 2);
    }

    public override void Hitted(EffectPacket effectPacket)
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果是家獸，就上去
        if (!other.gameObject.CompareTag("HomeBeast"))
            return;
        if (other.gameObject.TryGetComponent(out HomeBeastController homeBeastController))
            BoardOn(homeBeastController);
    }

    /// <summary>
    /// 登上家獸
    /// </summary>
    /// <param name="homeBeast"></param>
    void BoardOn(HomeBeastController homeBeast)
    {
        //HACK:折衷寫法
        _homeBeast = homeBeast;
        //TODO: 或許可以跑個進度條再進去
        InputManager.Instance.NormalizedStickMoved -= OnSetMoveDirection;
        if (homeBeast.TryOnBoard(this, out Transform sentry))
        {
            DriveStateChanged?.Invoke(true);
            transform.position = sentry.position;
            transform.parent = homeBeast.transform;
            OnSetMoveDirection(Vector2.zero);
        }
    }

    /// <summary>
    /// 離開家獸
    /// </summary>
    /// <param name="homeBeast"></param>
    public void BoardOff()
    {
        if(_homeBeast)
            throw new Exception("沒在家獸上但按下了離開家獸");
        //HACK:折衷寫法
        InputManager.Instance.NormalizedStickMoved += OnSetMoveDirection;

        if (_homeBeast.TryOffBoard(this))
            DriveStateChanged?.Invoke(false);
        _homeBeast = null;
    }

    /// <summary>
    /// 每幀調用的移動
    /// </summary>
    private void Move()
    {
        if (_moveDirection.magnitude == 0)
            return;
        transform.Translate(Speed * Time.deltaTime * _moveDirection);
        Driving?.Invoke(_moveDirection);
    }

    /// <summary>
    /// 改變移動方向
    /// </summary>
    /// <param name="vector2"></param>
    private void OnSetMoveDirection(Vector2 vector2)
    {
        _moveDirection = vector2;
    }


    private void OnDisable()
    {
        InputManager.Instance.NormalizedStickMoved -= OnSetMoveDirection;
    }
}
