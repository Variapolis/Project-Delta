public interface IDamageable
{
    public enum DamageType
    {
        Hit,
        Explosive,
        Stun,
        Blind
    }
    
    public void Damage(float damage, DamageType damageType = DamageType.Hit);
}