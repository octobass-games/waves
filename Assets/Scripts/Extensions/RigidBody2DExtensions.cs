using UnityEngine;

namespace Octobass.Waves.Extensions
{
    public static class RigidBody2DExtensions
    {
        public static Vector2 GetSafeDisplacement(this Rigidbody2D source, Vector2 direction, float distance, float skinWidth, ContactFilter2D contactFilter)
        {
            float distanceToMove = Mathf.Abs(distance);

            RaycastHit2D[] hits = new RaycastHit2D[10];
            int hitCount = source.Cast(direction.normalized, contactFilter, hits, distanceToMove + skinWidth);

            if (hitCount > 0)
            {
                RaycastHit2D hit = hits[0];

                distanceToMove = Mathf.Min(distanceToMove, hit.distance - skinWidth);
            }

            return distanceToMove * direction.normalized;
        }
    }
}
