using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool<AnemyGenerator>
{

}

public class AnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private AnemyController _prefab;
    [SerializeField] private Stack<AnemyController> _pooled = new Stack<AnemyController>();
    [SerializeField] private CircleCollider2D _circleRange;
    [SerializeField] private int _maxBatchSize;
    [SerializeField] private int _interval;
    private float _startTime;

    // Update is called once per frame
    void Update()
    {
        int currentTime = (int)Time.time;
        //時間未到
        if (currentTime - _startTime <= _interval)
            return;
        //時間到了可以檢查是否要生成，若生成則重新計時
        int amount = Random.Range(1, _maxBatchSize);
        //生一坨怪
        for (int i = 0; i < amount; i++)
        {
            Get();
        }
        //生成後時間歸零
        _startTime = (int)Time.time;
    }
    
    void Start()
    {
        _startTime = (int)Time.time;
    }

    public AnemyController Get()
    {
        if (!_pooled.TryPop(out AnemyController anemy)){
            anemy = Instantiate(_prefab);
            anemy.transform.SetParent(transform);
        }        
        //設定初始值
        Vector2 position = (Vector2)transform.position + Random.insideUnitCircle * _circleRange.radius;
        anemy.Init(position, this, _target);
        anemy.gameObject.SetActive(true);
        return anemy;
    }

    public void Release(AnemyController element)
    {
        element.gameObject.SetActive(false);
        _pooled.Push(element);
    }

    public void Clear()
    {
        while (_pooled.Count > 0)
        {
            Destroy(_pooled.Pop().gameObject);
        }
    }

}
