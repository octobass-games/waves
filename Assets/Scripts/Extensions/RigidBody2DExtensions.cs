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

        public static bool IsCollidingDown(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsColliding(source, Vector2.down, contactFilter, distance);
        }

        public static bool IsCollidingUp(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsColliding(source, Vector2.up, contactFilter, distance);
        }

        public static bool IsCollidingVertically(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsCollidingUp(source, contactFilter, distance) || IsCollidingDown(source, contactFilter, distance);
        }

        public static bool IsCollidingLeft(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsColliding(source, Vector2.left, contactFilter, distance);
        }

        public static bool IsCollidingRight(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsColliding(source, Vector2.right, contactFilter, distance);
        }

        public static bool IsCollidingHorizontally(this Rigidbody2D source, ContactFilter2D contactFilter, float distance)
        {
            return IsCollidingLeft(source, contactFilter, distance) || IsCollidingRight(source, contactFilter, distance);
        }

        public static bool IsColliding(this Rigidbody2D source, Vector2 direction, ContactFilter2D contactFiler, float distance)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = source.Cast(direction.normalized, contactFiler, hits, distance);

            return count > 0;
        }
    }
}
