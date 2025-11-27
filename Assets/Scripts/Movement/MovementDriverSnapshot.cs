using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class MovementDriverSnapshot
    {
        public Vector2 Movement;
        public Vector2 Swimming;
        public Vector2 Climbing;
        public bool DashPressed;
        public bool DashReleased;
        public bool JumpPressed;
        public bool JumpReleased;
        public bool GrabPressed;
        public bool GrabReleased;
        public bool GrabHeld;
        public bool AttackPressed;
        public bool AttackReleased;
        public bool ProjectileAttackPressed;
        public bool ProjectilAttackReleased;
    }
}
