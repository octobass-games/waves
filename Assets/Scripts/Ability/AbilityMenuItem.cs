using Octobass.Waves;
using Octobass.Waves.Item;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityMenuItem : Selectable
{
    public AbilityDefinition AbilityDefinition;

    private Action<AbilityDefinition> OnSelectCallback;

    public void RegisterOnSelect(Action<AbilityDefinition> onSelect)
    {
        OnSelectCallback = onSelect;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        OnSelectCallback?.Invoke(AbilityDefinition);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        OnSelectCallback?.Invoke(AbilityDefinition);
    }
}
