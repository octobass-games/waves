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

        [SerializeField]
        public TMPro.TextMeshProUGUI FlippableText;

        [SerializeField]
        public TMPro.TextMeshProUGUI NonFlippableText;


        public void OnItemPickedUp(ItemInstance item)
        {
            if (item is LoreItemInstance loreItemInstance)
            {
                if (LoreRoot != null)
                {
                    LoreRoot.SetActive(true);
                }

                PlayerInput.SwitchCurrentActionMap("UI");

                Sprite frontSideSprite = loreItemInstance.Definition.FrontSprite;
                Sprite backSideSprite = loreItemInstance.Definition.BackSprite;
                string loreText = loreItemInstance.Definition.Text;

                if (frontSideSprite != null && backSideSprite != null)
                {
                    NonFlippableImage.gameObject.SetActive(false);
                    Flippable.SetActive(true);
                    FlippableFront.sprite = frontSideSprite;
                    FlippableFront.SetNativeSize();
                    FlippableBack.sprite = backSideSprite;
                    FlippableBack.SetNativeSize();

                    EventSystem.current.SetSelectedGameObject(FlippableButton.gameObject);

                    FlippableText.gameObject.SetActive(loreText != null || loreText != "");
                    NonFlippableText.gameObject.SetActive(false);
                    FlippableText.text = loreText;
                }
                else
                {
                    NonFlippableImage.sprite = frontSideSprite;
                    NonFlippableImage.SetNativeSize();
                    NonFlippableImage.gameObject.SetActive(true);
                    Flippable.SetActive(false);

                    if (CloseButton != null)
                    {
                        EventSystem.current.SetSelectedGameObject(CloseButton);
                    }

                    NonFlippableText.gameObject.SetActive(loreText != null || loreText != "");
                    FlippableText.gameObject.SetActive(false);
                    NonFlippableText.text = loreText;
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
