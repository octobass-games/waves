using UnityEngine;

namespace Octobass.Waves.Water
{
    public class WaterFillable : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer SpriteRenderer;

        [SerializeField]
        private float FillableSpeed;

        [SerializeField]
        private Transform FillableTop;

        [SerializeField]
        private Transform FillableBottom;

        [SerializeField]
        private float FillableLineWidth;

        private Material FillableMaterial;
        private float FillableLine;

        private PlayerInput PlayerInput;

        void Awake()
        {
            if (SpriteRenderer == null)
            {
                Debug.LogWarning("[WaterFillable]: SpriteRenderer not set");
            }
            
            if (FillableSpeed == 0)
            {
                Debug.LogWarning("[WaterFillable]: FillableSpeed is zero");
            }

            if (FillableTop == null)
            {
                Debug.LogWarning("[WaterFillable]: FillableTop not set");
            }

            if (FillableBottom == null)
            {
                Debug.LogWarning("[WaterFillable]: FillableBottom not set");
            }

            if (FillableLineWidth == 0)
            {
                Debug.LogWarning("[WaterFillable]: FillableLineWidth is zero");
            }

            FillableMaterial = SpriteRenderer.material;
            FillableLine = FillableBottom.position.y;
            FillableMaterial.SetFloat("_FillableLine", FillableLine);

            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        void Update()
        {
            if (PlayerInput.Movement.Inspect.IsPressed())
            {
                FillableLine = Mathf.Clamp(FillableLine + FillableSpeed * Time.deltaTime, FillableBottom.transform.position.y, FillableTop.transform.position.y);

                FillableMaterial.SetFloat("_FillableLine", FillableLine);
            }
            else if (PlayerInput.Movement.Attack.IsPressed())
            {
                FillableLine = Mathf.Clamp(FillableLine - FillableSpeed * Time.deltaTime, FillableBottom.transform.position.y, FillableTop.transform.position.y);

                FillableMaterial.SetFloat("_FillableLine", FillableLine);
            }
        }
    }
}
