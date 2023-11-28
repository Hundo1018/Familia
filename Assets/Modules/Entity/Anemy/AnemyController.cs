using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyController : MonoBehaviourEntity, IReusable
{
    private AnemyPool _anemyGenerator;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }


    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        // transform.rotation.SetLookRotation(,)
    }

    public void Die() => _anemyGenerator.Release(gameObject);


    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="anemyGenerator"></param>
    /// <param name="position"></param>
    /// <param name="target"></param>
    private void Init(Vector2 position, GameObject target)
    {
        transform.position = position;
        _target = target;
    }



    public IPool GetPool() => _anemyGenerator;


    public void Release() => _anemyGenerator.Release(gameObject);

    // /// <summary>
    // /// 建構子
    // /// </summary>
    // /// <param name="pool">物件池</param>
    // /// <param name="args">0:出生點,1:target</param>
    void IReusable.Initialize(IPool pool, params object[] args)
    {
        _anemyGenerator = (AnemyPool)pool;
        Init((Vector2)args[0], (GameObject)args[1]);
    }

    public override void Hitted(EffectPacket effectPacket)
    {
        Debug.Log($"{gameObject.name} Hitted by {effectPacket.Source.name}");
        HP -= effectPacket.Damage;
        if (HP <= 0) Release();
    }
}
