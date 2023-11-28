using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// 基礎投射物
/// </summary>
public class Projectile : MonoBehaviour, IReusable
{
    [SerializeField] private Vector2 _target;
    [SerializeField] private float _lifeTimeLeft;
    [SerializeField] private float _lifeTimeMax;
    [SerializeField] private float _lifeHitMax;
    [SerializeField] private float _lifeHitLeft;
    [SerializeField] ProjectilePool _projectilePool;
    [SerializeField] EffectPacket _effectPacket;
    public IPool GetPool()
    {
        return _projectilePool;
    }

    private void Init(EffectPacket effectPacket, Vector2 target, float lifeTimeMax, int lifeHitMax)
    {
        transform.position = effectPacket.Source.transform.position;
        _target = target;
        _effectPacket = effectPacket;
        _lifeTimeMax = lifeTimeMax;
        _lifeTimeLeft = _lifeTimeMax;
        _lifeHitMax = lifeHitMax;
        _lifeHitLeft = lifeHitMax;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pool"></param>
    /// <param name="args">source:GameObject , target:Vector , maxLifeTime:float</param>
    public void Initialize(IPool pool, params object[] args)
    {
        _projectilePool = (ProjectilePool)pool;
        Init((EffectPacket)args[0], (Vector2)args[1], (float)args[2], (int)args[3]);
    }

    public void Release()
    {
        _projectilePool.Release(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, _target, _lifeTimeMax - _lifeTimeLeft);
        ReleaseCheck(ref _lifeTimeLeft, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //確定有繼承Entity，以及非友軍才繼續
        if (!other.TryGetComponent(out MonoBehaviourEntity component) ||
        component.Alignments == _effectPacket.Source.Alignments)
            return;
        component.Hitted(_effectPacket);
        ReleaseCheck(ref _lifeHitLeft, 1);
    }
    private void ReleaseCheck(ref float left, float reduceBy)
    {
        left -= reduceBy;
        if (left <= 0) Release();
    }
}
