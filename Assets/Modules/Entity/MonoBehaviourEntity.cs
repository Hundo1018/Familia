using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourEntity : MonoBehaviour
{
    public new Collider2D collider2D;
    public int MaxHP;
    public int HP;
    public int Alignments;
    public abstract void Hitted(EffectPacket effectPacket);
}