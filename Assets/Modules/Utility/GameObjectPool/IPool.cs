using UnityEngine;

/// <summary>
/// 物件池
/// </summary>
public interface IPool
{
    public GameObject Get(IPool pool, params object[] initParams);

    public void Release(GameObject element);

    public void Clear();
}
