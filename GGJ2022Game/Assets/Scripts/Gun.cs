using UnityEngine;

namespace Nidavellir
{
    public class Gun : Weapon
    {
        [SerializeField] private Projectile m_projectileAsset;

        private readonly Vector3 m_aim = new Vector3(0f, .7071068f, .7071068f);

        private Vector3 m_gunVelocity;
        private Vector3 m_positionAtLastFrame = Vector3.zero;

        public void Update()
        {
            this.m_gunVelocity = (this.transform.position - m_positionAtLastFrame) / Time.deltaTime;
            this.m_positionAtLastFrame = this.transform.position;
        }

        public override void Attack()
        {
            base.Attack();
            var projectile = Instantiate(this.m_projectileAsset);
            projectile.transform.position = this.transform.position;

            // by adding the gun's (= the character's) velocity to the projectile, we can actively throw it further by moving while shooting
            projectile.Launch(1f * this.m_gunVelocity + 12f * this.transform.TransformDirection(this.m_aim));
        }
    }
}