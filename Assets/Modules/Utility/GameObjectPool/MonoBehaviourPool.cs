using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 物件池抽象類別，處理Get Release與Clear
/// 因此子類別只需要實現使用這些函式的時機與適當的建構式多載
/// </summary>
/// <typeparam name="T">可複用物件的Controller</typeparam>
public abstract class MonoBehaviourPool<T> : MonoBehaviour, IPool
    where T : Component, IReusable
{
    public GameObject _prefab;
    protected Stack<GameObject> _pooled = new();
    public int CountActive { get; private set; }
    public int CountAll { get; private set; }
    public int CountInactive { get; private set; }


    public void Release(GameObject element)
    {
        element.SetActive(false);
        _pooled.Push(element);
        CountInactive++;
        CountActive--;
    }

    public void Clear()
    {
        while (_pooled.Count > 0)
        {
            Destroy(_pooled.Pop());
        }
        CountAll = 0;
        CountActive = 0;
        CountInactive = 0;
    }
    public GameObject Get(params object[] initParams)
    {
        //重點是相關型別的controller
        T controller;
        if (!_pooled.TryPop(out GameObject instance))
        {
            instance = Instantiate(_prefab);
            instance.transform.SetParent(transform);
            CountAll++;
        }
        else
            CountInactive--;

        CountActive++;

        controller = instance.GetComponent<T>();
        //設定初始值
        controller.Initialize(this, initParams);
        // instance
        instance.SetActive(true);
        return instance;
    }
}