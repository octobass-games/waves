using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class CharacterController2DCollisionDetector
    {
        private Rigidbody2D Body;
        private float SkinWidth;
        private float WallJumpSkinWidth;
        private ContactFilter2D GroundContactFilter;
        private ContactFilter2D RideableContactFilter;

        public CharacterController2DCollisionDetector(Rigidbody2D body, CharacterController2DConfig characterController2DConfig)
        {
            Body = body;
            SkinWidth = characterController2DConfig.SkinWidth;
            WallJumpSkinWidth = characterController2DConfig.WallJumpSkinWidth;
            GroundContactFilter = characterController2DConfig.GroundContactFilter;
            RideableContactFilter = characterController2DConfig.RideableContactFilter;
        }

        public bool IsGrounded()
        {
            return Body.IsCollidingDown(GroundContactFilter, SkinWidth);
        }

        public bool IsTouchingRightWall()
        {
            return Body.IsCollidingRight(GroundContactFilter, SkinWidth);
        }

        public bool IsTouchingLeftWall()
        {
            return Body.IsCollidingLeft(GroundContactFilter, SkinWidth);
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
    }
}
