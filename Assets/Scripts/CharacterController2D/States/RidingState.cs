using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class RidingState : ICharacterState
    {
        private StateContext StateContext;
        private IRideable Rideable;

        public RidingState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            Rideable = GetPlatform(Vector2.down * StateContext.CharacterControllerConfig.SkinWidth);
        }

        public void Exit()
        {
            Rideable = null;
        }

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = Rideable.GetDisplacement() + new Vector2(StateContext.DriverSnapshot.Movement.x, 0) * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            IRideable platform = GetPlatform(Vector2.down * StateContext.CharacterControllerConfig.SkinWidth);

            if (platform == null)
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.Jumping;
            }

            return null;
        }

        public void Update()
        {
        }

        private IRideable GetPlatform(Vector2 displacement)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(StateContext.Body.position + displacement, StateContext.Body.GetComponent<Collider2D>().bounds.size, 0f, Vector2.down, StateContext.CharacterControllerConfig.SkinWidth, StateContext.CharacterControllerConfig.RideableContactFilter.layerMask.value);

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
    }
}
