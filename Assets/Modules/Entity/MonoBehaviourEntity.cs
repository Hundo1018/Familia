using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourEntity : MonoBehaviour
{
    public new Collider2D collider2D;
    public int MaxHP;
    public int HP;
    public int Alignments;
    public virtual void Hitted(EffectPacket effectPacket)
    {
        throw new NotImplementedException($"{gameObject.name} hitted by {effectPacket.Source.name}");
    }
}