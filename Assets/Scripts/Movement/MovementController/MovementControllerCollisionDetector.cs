using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class MovementControllerCollisionDetector
    {
        public Rigidbody2D Body;

        private float SkinWidth;
        private float WallJumpSkinWidth;
        private float SwimmingBobHeight;
        private ContactFilter2D GroundContactFilter;
        private ContactFilter2D RideableContactFilter;
        private ContactFilter2D WaterwayContactFilter;
        private ContactFilter2D WaterwayEntranceContactFilter;

        public MovementControllerCollisionDetector(Rigidbody2D body, MovementConfig characterController2DConfig)
        {
            Body = body;
            SkinWidth = characterController2DConfig.SkinWidth;
            WallJumpSkinWidth = characterController2DConfig.WallJumpSkinWidth;
            GroundContactFilter = characterController2DConfig.GroundContactFilter;
            RideableContactFilter = characterController2DConfig.RideableContactFilter;
            WaterwayContactFilter = characterController2DConfig.WaterwayContactFilter;
            WaterwayEntranceContactFilter = characterController2DConfig.WaterwayEntranceContactFilter;
            SwimmingBobHeight = characterController2DConfig.SwimmingBobHeight;
        }

        public bool IsXCoordinateLessThanOrEqualTo(float xCoordinate)
        {
            return Body.position.x <= xCoordinate;
        }

        public bool IsXCoordinateGreaterThanOrEqualTo(float xCoordinate)
        {
            return Body.position.x >= xCoordinate;
        }

        public bool IsYCoordinateGreaterThanOrEqualTo(float yCoordinate)
        {
            BoxCollider2D collider = Body.GetComponent<BoxCollider2D>();

            return collider.bounds.min.y >= yCoordinate;
        }

        public bool IsGrounded()
        {
            return Body.IsCollidingDown(GroundContactFilter, SkinWidth);
        }

        public bool IsAtLedge()
        {
            return GetLedgeClimbTargetPosition() != null;
        }

        public Vector2? GetLedgeClimbTargetPosition()
        {
            BoxCollider2D collider = Body.GetComponent<BoxCollider2D>();
            Vector2 colliderCenter = collider.bounds.center;
            float halfWidth = collider.bounds.extents.x;
            Vector2 direction = IsTouchingRightWall() ? Vector2.right : Vector2.left;

            Vector2 topRayOrigin = (Vector2)collider.bounds.center + new Vector2(0, 0.5f);

            RaycastHit2D topRayHit = Physics2D.Raycast(topRayOrigin, direction, halfWidth + 0.03125f * 5, GroundContactFilter.layerMask);
            RaycastHit2D bottomRayHit = Physics2D.Raycast(colliderCenter, direction, halfWidth + 0.03125f * 5, GroundContactFilter.layerMask);
            
            if (topRayHit.collider == null && bottomRayHit.collider != null)
            {
                float horizontalDistance = bottomRayHit.distance + halfWidth / 2;
                float verticalDistance = topRayOrigin.y - collider.bounds.min.y;

                return direction == Vector2.right
                    ? new Vector2(horizontalDistance, verticalDistance) + new Vector2(colliderCenter.x, collider.bounds.min.y)
                    : new Vector2(-horizontalDistance, verticalDistance) + new Vector2(colliderCenter.x, collider.bounds.min.y);
            }

            return null;
        }

        public bool IsAtClimbHeight()
        {
            BoxCollider2D collider = Body.GetComponent<BoxCollider2D>();

            float halfWidth = collider.bounds.extents.x;
            Vector2 direction = IsTouchingRightWall() ? Vector2.right : Vector2.left;

            Vector2 topRayOrigin = (Vector2)collider.bounds.center + new Vector2(0, 0.5f);
            RaycastHit2D topRayHit = Physics2D.Raycast(topRayOrigin, direction, halfWidth + 0.03125f * 5, GroundContactFilter.layerMask);

            return topRayHit.collider != null;
        }

        public bool IsTouchingRightWall()
        {
            return Body.IsCollidingRight(GroundContactFilter, SkinWidth);
        }

        public bool IsTouchingLeftWall()
        {
            return Body.IsCollidingLeft(GroundContactFilter, SkinWidth);
        }

        public bool IsTouchingWall(Vector2 direction)
        {
            return Body.IsColliding(direction, GroundContactFilter, SkinWidth);
        }

        public bool IsTouchingWall()
        {
            return IsTouchingLeftWall() || IsTouchingRightWall();
        }

        public bool IsTouchingCeiling()
        {
            return Body.IsCollidingUp(GroundContactFilter, SkinWidth);
        }

        public bool IsOnPlatform()
        {
            return Body.IsCollidingDown(RideableContactFilter, SkinWidth);
        }

        public bool IsCloseToRightWall()
        {
            return Body.IsCollidingRight(GroundContactFilter, SkinWidth + WallJumpSkinWidth);
        }

        public bool IsCloseToLeftWall()
        {
            return Body.IsCollidingLeft(GroundContactFilter, SkinWidth + WallJumpSkinWidth);
        }

        public bool IsCloseToWall()
        {
            return IsCloseToLeftWall() || IsCloseToRightWall();
        }

        public IRideable GetPlatform()
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(Body.position + Vector2.down * SkinWidth, Body.GetComponent<Collider2D>().bounds.size, 0f, Vector2.down, SkinWidth, RideableContactFilter.layerMask.value);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                GameObject gameObject = hit.collider.gameObject;
                MonoBehaviour[] monoBehaviours = gameObject.GetComponents<MonoBehaviour>();

                foreach (MonoBehaviour monoBehaviour in monoBehaviours)
                {
                    IRideable rideable = monoBehaviour as IRideable;

                    if (rideable != null)
                    {
                        return rideable;
                    }
                }
            }

            return null;
        }

        public bool IsSwimmingAtWaterwayEntrance()
        {
            Collider2D[] colliders = new Collider2D[10];

            int count = Body.Overlap(WaterwayEntranceContactFilter, colliders);

            if (count > 0)
            {
                var characterY = Body.GetComponent<BoxCollider2D>().bounds.max.y;
                var colliderY = colliders[0].bounds.max.y;
                var bobPositionY = colliderY + SwimmingBobHeight;
                float verticalDistanceFromBobHeight = characterY - bobPositionY;

                return verticalDistanceFromBobHeight >= 0;
            }

            return false;
        }

        public bool IsTouchingWaterway()
        {
            return DetectWaterway() != null;
        }

        public Collider2D DetectWaterway()
        {
            Collider2D[] colliders = new Collider2D[10];

            int count = Body.Overlap(WaterwayContactFilter, colliders);

            if (count > 0)
            {
                return colliders[0];
            }

            return null;
        }
    }
}
