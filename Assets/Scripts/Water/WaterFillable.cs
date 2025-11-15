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

        [SerializeField]
        private BoxCollider2D Collider;

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

            if (Collider == null)
            {
                Debug.LogWarning("[WaterFillable]: Collider not set");
            }

            FillableMaterial = SpriteRenderer.material;
            FillableLine = FillableBottom.position.y;
            FillableMaterial.SetFloat("_FillableLine", FillableLine);

            Collider.size = new Vector3(Collider.size.x, (FillableBottom.position.y - SpriteRenderer.bounds.min.y) / transform.lossyScale.y);
            Collider.offset = new Vector2(0, FillableBottom.position.y / transform.lossyScale.y);

            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        public void Fill()
        {
            Move(Vector2.up);
        }

        public void Drain()
        {
            Move(Vector2.down);
        }

        private void Move(Vector2 direction)
        {
            FillableLine = Mathf.Clamp(FillableLine + direction.y * Time.deltaTime, FillableBottom.transform.position.y, FillableTop.transform.position.y);

            Collider.size = new Vector2(Collider.size.x, (FillableLine - SpriteRenderer.bounds.min.y) / transform.lossyScale.y);
            Collider.offset = new Vector2(0, (SpriteRenderer.bounds.min.y + ((FillableLine - SpriteRenderer.bounds.min.y) / 2) - transform.position.y) / transform.lossyScale.y);

            FillableMaterial.SetFloat("_FillableLine", FillableLine);
        }
    }
}
