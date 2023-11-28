using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class HomeBeastController : MonoBehaviourEntity
{
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _speed;
    private Vector2 _moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.NormalizedStickMoved += SetMoveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Hitted(EffectPacket effectPacket)
    {
    }

    void SetMoveDirection(Vector2 vector2)
    {
        _moveDirection = vector2;
    }
    void Move()
    {
        if(_moveDirection.magnitude == 0)
            return;
        transform.Translate(_speed * Time.deltaTime * _moveDirection);
    }
}
