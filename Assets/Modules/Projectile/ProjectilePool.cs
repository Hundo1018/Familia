using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilePool : MonoBehaviourPool<Projectile>
{
    /// <summary>
    /// 取得並初始化
    /// </summary>
    /// <param name="effectPacket">效果包</param>
    /// <param name="target">目標</param>
    /// <param name="lifeTimeMax">持續時間</param>
    /// <param name="lifeHitMax">可擊中次數</param>
    /// <returns></returns>
    public GameObject Get(EffectPacket effectPacket, Vector2 target, float lifeTimeMax, int lifeHitMax)
    {
        return base.Get(effectPacket, target, lifeTimeMax, lifeHitMax);
    }
}
