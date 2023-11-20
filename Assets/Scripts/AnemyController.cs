using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AnemyController : MonoBehaviour
{
    private AnemyGenerator _anemyGenerator;
    private GameObject _target;
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

    public void Init(Vector2 position, AnemyGenerator anemyGenerator, GameObject target)
    {
        transform.position = position;
        _anemyGenerator = anemyGenerator;
        _target = target;
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    public void Die()
    {
        _anemyGenerator.Release(this);
    }
}
