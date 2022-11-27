using JetBrains.Annotations;
using Photon.Realtime;

public interface IDamageable
{
    public enum DamageType
    {
        Hit,
        Explosive,
        Stun,
        Blind
    }
    
    public void Damage(float damage, [CanBeNull] Player attacker = null, DamageType damageType = DamageType.Hit);
}