using UnityEngine;

namespace Octobass.Waves
{
    public class Parallax : MonoBehaviour
    {
        public float parallaxFactor = 0.1f;

        private Transform MainCamera;
        private Vector3 PreviousCameraPosition;
        public float ActivationDistance = 15;

        void Start()
        {
            MainCamera = UnityEngine.Camera.main.transform;
            PreviousCameraPosition = MainCamera.position;
        }

        void LateUpdate()
        {
            float distance = Vector3.Distance(MainCamera.position, transform.position);

            if (distance <= ActivationDistance)
            {
                Vector3 displacement = MainCamera.position - PreviousCameraPosition;

                transform.position += new Vector3(displacement.x * parallaxFactor, displacement.y * parallaxFactor, 0);
            }

            PreviousCameraPosition = MainCamera.position;
        }
    }
}
