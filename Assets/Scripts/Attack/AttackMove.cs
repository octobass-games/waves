using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class AttackMove : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D Collider;

        private List<IDamageable> Attackables = new();
        private Collider2D[] OverlappingColliders = new Collider2D[10];

        public void Activate()
        {
            var contactFilter = new ContactFilter2D()
            {
                useTriggers = true
            };

            int count = Collider.Overlap(contactFilter, OverlappingColliders);

            for (int i = 0; i < count; i++)
            {
                Collider2D collider = OverlappingColliders[i];

                IDamageable attackable = collider.GetComponent<IDamageable>();

                if (attackable != null && !Attackables.Contains(attackable))
                {
                    attackable.OnHit();
                    Attackables.Add(attackable);
                }

                OscillatingHider oscillatingHider = collider.GetComponent<OscillatingHider>();

                if (oscillatingHider != null)
                {
                    oscillatingHider.Hide();
                }

                ProjectileDamageable projectileDamageable = collider.GetComponent<ProjectileDamageable>();

                if (projectileDamageable != null)
                {
                    projectileDamageable.NonProjectileHit();
                }
            }
        }

        public void Deactivate()
        {
            Attackables.Clear();
        }
    }
}
