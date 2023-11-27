using UnityEngine;

/// <summary>
/// 表示可複用物件
/// </summary>
public interface IReusable
{
    /// <summary>
    /// 建構子
    /// </summary>
    /// <param name="pool">注入建構此物件的物件池</param>
    /// <param name="args">建構子參數</param>
    public void Initialize(IPool pool, params object[] args);
    /// <summary>
    /// 取得所屬物件池
    /// </summary>
    /// <returns>所屬物件池</returns>
    public IPool GetPool();
    /// <summary>
    /// 令此物件回到所屬池中
    /// </summary>
    public void Release();
}