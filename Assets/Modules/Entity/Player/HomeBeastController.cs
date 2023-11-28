using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBeastController : MonoBehaviourEntity
{
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.NormalizedMoved += onMove;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Hitted(EffectPacket effectPacket)
    {
    }

    void onMove(Vector2 vector2)
    {
        // transform.Translate(vector2);
    }
}
