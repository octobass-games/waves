using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D Body;

        [SerializeField]
        private Animator Animator;

        [SerializeField]
        private float Speed;

        [SerializeField]
        private LayerMask TargetLayerMask;

        private Vector2 Direction = Vector2.zero;
        private bool HasMadeImpact;
        private ContactFilter2D TargetContactFilter;

        private int ColliderCount;
        private Collider2D[] Colliders = new Collider2D[3];

        void Awake()
        {
            TargetContactFilter = new()
            {
                useLayerMask = true,
                layerMask = TargetLayerMask
            };
        }

        public void Init(Vector2 direction, Vector2 startPosition)
        {
            Direction = direction;
            Body.position = startPosition;
        }

        void Update()
        {
            if (HasMadeImpact)
            {
                Animator.SetTrigger("Explode");
            }
        }

        void FixedUpdate()
        {
            ColliderCount = Body.Overlap(TargetContactFilter, Colliders);

            if (ColliderCount > 0)
            {
                HasMadeImpact = true;

                for (int i = 0; i < ColliderCount; i++)
                {
                    Collider2D collider = Colliders[i];

                    if (collider.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.OnOneShot();
                    }
                    else if (collider.TryGetComponent(out OscillatingHider hider))
                    {
                        hider.Hide();
                    }
                }
            }
            else
            {
                Body.MovePosition(Body.position + Direction * Speed * Time.fixedDeltaTime);
            }
        }

        void OnExplosionAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}
