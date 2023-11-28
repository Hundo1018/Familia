using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourEntity
{
    [SerializeField] private ProjectilePool _projectilePool;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchEnded += Shot;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shot(Vector2 target)
    {
        target = Camera.main.ScreenToWorldPoint(target);
        _projectilePool.Get(new EffectPacket(this) { Damage = 3 }, target, 1f,60);
    }

    public override void Hitted(EffectPacket effectPacket)
    {
        Debug.Log($"{gameObject.name} Hitted by {effectPacket.Source.name}");
    }
}
