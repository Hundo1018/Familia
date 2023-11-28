using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;


public class AnemyPool : MonoBehaviourPool<AnemyController>
{
    [SerializeField] private GameObject _target;
    [SerializeField] private CircleCollider2D _circleRange;
    [SerializeField] private int _maxBatchSize;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _interval;
    private float _startTime;

    // Update is called once per frame
    void Update()
    {
        int currentTime = (int)Time.time;
        //時間未到
        if (currentTime - _startTime <= _interval)
            return;
        //太多別生
        if(_pooled.Count>=_maxSize)
            return;
        //時間到了可以檢查是否要生成，若生成則重新計時
        int amount = Random.Range(1, _maxBatchSize);
        //批量生一坨怪
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = (Vector2)transform.position + Random.insideUnitCircle * _circleRange.radius;
            Get(position, _target);
        }
        //生成後時間歸零
        _startTime = (int)Time.time;
    }

    void Start()
    {
        _startTime = (int)Time.time;
    }
}
