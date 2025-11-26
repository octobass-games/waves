using Octobass.Waves.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Octobass.Waves.Item
{
    public class LoreInspector : MonoBehaviour
    {
        [SerializeField]
        private GameObject LoreRoot;

        [SerializeField]
        private TextMeshProUGUI LoreText;

        [SerializeField]
        private MovementController MovementController;

        [SerializeField]
        private Image NonFlippableImage;

        [SerializeField]
        private GameObject Flippable;

        [SerializeField]
        private Image FlippableFront;

        [SerializeField]
        private Image FlippableBack;

        [SerializeField]
        private Button FlippableButton;

        [SerializeField]
        private GameObject CloseButton;

        [SerializeField]
        private PlayerInput PlayerInput;

        void Awake()
        {
            if (LoreRoot == null)
            {
                Debug.LogWarning("[LoreInspector]: LoreRoot not set");
            }

            if (LoreText == null)
            {
                Debug.LogWarning("[LoreInspector]: LoreText not set");
            }

            if (MovementController == null)
            {
                Debug.LogWarning("[LoreInspector]: MovementController not set");
            }
        }

        public void OnItemPickedUp(ItemInstance item)
        {
            if (item is LoreItemInstance loreItemInstance)
            {
                LoreRoot.SetActive(true);

                PlayerInput.SwitchCurrentActionMap("UI");

                Sprite frontSideSprite = loreItemInstance.Definition.FrontSprite;
                Sprite backSideSprite = loreItemInstance.Definition.BackSprite;

                if (frontSideSprite != null && backSideSprite != null)
                {
                    NonFlippableImage.gameObject.SetActive(false);
                    Flippable.SetActive(true);
                    FlippableFront.sprite = frontSideSprite;
                    FlippableFront.SetNativeSize();
                    FlippableBack.sprite = backSideSprite;
                    FlippableBack.SetNativeSize();

                    EventSystem.current.SetSelectedGameObject(FlippableButton.gameObject);
                }
                else
                {
                    NonFlippableImage.sprite = frontSideSprite;
                    NonFlippableImage.SetNativeSize();
                    NonFlippableImage.gameObject.SetActive(true);
                    Flippable.SetActive(false);

                    EventSystem.current.SetSelectedGameObject(CloseButton);
                }
            }
        }

        public void OnLoreInspected()
        {
            LoreRoot.SetActive(false);

            PlayerInput.SwitchCurrentActionMap("Gameplay");
        }
    }
}
