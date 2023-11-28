using UnityEngine;

/// <summary>
/// 物件池
/// </summary>
public interface IPool
{
    public GameObject Get( params object[] initParams);

    public void Release(GameObject element);

    public void Clear();
}
