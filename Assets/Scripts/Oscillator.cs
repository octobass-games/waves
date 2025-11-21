using UnityEngine;

namespace Octobass.Waves
{
    public class Oscillator : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D Body;

        [SerializeField]
        private float Speed;

        [SerializeField]
        [Tooltip("This is the point that the oscillator will first move to")]
        private Transform PointA;

        [SerializeField]
        private Transform PointB;

        private Transform Target;
        private float Tolerance;

        void Awake()
        {
            Target = PointA;
            Tolerance = Mathf.Max((Speed * Time.fixedDeltaTime) / 2, 0.03125f);
        }

        void FixedUpdate()
        {
            if (Vector2.Distance(Body.position, Target.position) < Tolerance)
            {
                Target = Target == PointA ? PointB : PointA;
            }
            else
            {
                Vector2 direction = ((Vector2)Target.position - Body.position).normalized;

                Vector2 displacement = direction * Speed * Time.fixedDeltaTime;

                Body.MovePosition(Body.position + displacement);
            }
        }
    }
}
