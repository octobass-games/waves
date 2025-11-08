using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class MovementController : MonoBehaviour
    {
        public Rigidbody2D Body;
        public MovementControllerConfig CharacterControllerConfig;
        private MovementControllerCollisionDetector CollisionDetector;

        private MovementStateMachine MovementStateMachine;
        private MovementStateMachineContext MovementStateMachineContext;

        private ContactFilter2D AllGroundContactFilter;

        void Awake()
        {
            CollisionDetector = new MovementControllerCollisionDetector(Body, CharacterControllerConfig);

            MovementStateMachineContext = new MovementStateMachineContext(Body, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig, CollisionDetector);
            MovementStateMachine = new MovementStateMachine(MovementStateMachineContext);

            AllGroundContactFilter = new()
            {
                useLayerMask = true,
                layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask
            };
        }

        public Vector2 Tick(CharacterController2DDriverSnapshot snapshot)
        {
            MovementStateMachineContext.DriverSnapshot = snapshot;

            MovementStateMachine.Tick();

            Vector2 Displacement = MovementStateMachineContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = Displacement == Vector2.zero ? Vector2.zero : Displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), Displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), Displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);

            MovementStateMachine.EvaluateTransitions();

            return Displacement;
        }

        public void AddState(CharacterStateId newState)
        {
            MovementStateMachine.AddState(newState);
        }

        public CharacterStateId GetCurrentState()
        {
            return MovementStateMachine.CurrentStateId;
        }
    }
}
