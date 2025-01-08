using UnityEngine;

public class ParticleWeapon : Weapon
{
    public ParticleSystem particles;
    private void FixedUpdate()
    {
        if (Cooldown != null)
        {
            particles.Emit(1);
        }
    }
    public override bool Fire(Vector2 source, Vector2 target, GameObject owner)
    {
        
        return base.Fire(source, target, owner);
    }
}
