using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourEntity
{
    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private bool _isHolding;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchEnded += Shot;
        InputManager.Instance.TouchMoved += Hold;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 處理移動中向反方向射擊
    /// </summary>
    /// <param name="vector2"></param>
    void Hold(Vector2 vector2)
    {
        _isHolding = true;
    }
    void Shot(Vector2 target)
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
}
