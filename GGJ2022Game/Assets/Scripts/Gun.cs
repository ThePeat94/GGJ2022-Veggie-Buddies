using UnityEngine;

namespace Nidavellir
{
    public class Gun : Weapon
    {
        [SerializeField] private Projectile m_projectileAsset;

        public override void Attack()
        {
            base.Attack();
            // TODO: use a pool for the projectiles, especially when adding more complex stuff like audio sources
            var projectile = Instantiate(this.m_projectileAsset);
            projectile.transform.position = this.transform.position;

            // by adding the gun's (= the character's) velocity to the projectile, we can actively throw it further by moving while shooting
            projectile.Launch(this.transform.forward * 1000f);
        }
    }
}