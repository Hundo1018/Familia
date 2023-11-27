using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物件池抽象類別，處理Get Release與Clear
/// 因此子類別只需要實現使用這些函式的時機
/// </summary>
/// <typeparam name="T">可複用物件的Controller</typeparam>
public abstract class MonoBehaviourPool<T> : MonoBehaviour, IPool
    where T : IReusable
{
    public GameObject _prefab;
    protected Stack<GameObject> _pooled = new();

    public void Release(GameObject element)
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

    public GameObject Get(IPool pool, params object[] initParams)
    {
        T controller;
        //重點是controller
        if (!_pooled.TryPop(out GameObject instance))
        {
            instance = Instantiate(_prefab);
            instance.transform.SetParent(transform);
        }

        controller = instance.GetComponent<T>();
        //設定初始值
        controller.Initialize(pool, initParams);
        // instance
        instance.SetActive(true);
        return instance;
    }
}