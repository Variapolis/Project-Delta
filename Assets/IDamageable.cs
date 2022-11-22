public interface IDamageable
{
    public enum DamageType
    {
        Hit,
        Explosion,
        Stun
    }
    
    public void Damage(float damage, DamageType damageType = DamageType.Hit);
}