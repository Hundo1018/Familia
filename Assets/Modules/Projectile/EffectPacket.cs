public class EffectPacket
{
    public MonoBehaviourEntity Source;
    public int Damage;
    public EffectPacket(in MonoBehaviourEntity source) => Source = source;
}