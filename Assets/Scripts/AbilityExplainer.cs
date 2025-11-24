using Octobass.Waves;
using Octobass.Waves.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class AbilityExplainer : MonoBehaviour
{
    [SerializeField]
    private AbilityController AbilityController;

    [SerializeField]
    private GameObject ExplainerRoot;

    [SerializeField]
    private TextMeshProUGUI ExplainerText;

    void Awake()
    {
        if (AbilityController == null)
        {
            Debug.LogWarning("[AbilityExplainer]: AbilityController not set");
        }

        if (ExplainerRoot == null)
        {
            Debug.LogWarning("[AbilityExplainer]: ExplainerRoot not set");
        }

        if (ExplainerText == null)
        {
            Debug.LogWarning("[AbilityExplainer]: ExplainerText not set");
        }
    }

    public void Explain(AbilityInstance ability)
    {
        ExplainerRoot.SetActive(true);
        ExplainerText.text = ability.Explainer;

        InputSystem.onAnyButtonPress.CallOnce((_) => Dismiss());
    }

    private void Dismiss()
    {
        ExplainerRoot.SetActive(false);
        ExplainerText.text = "";

        AbilityController.EndUpgrade();
    }
}
