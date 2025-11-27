using Octobass.Waves;
using Octobass.Waves.Item;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityMenuItem : Selectable
{
    public AbilityDefinition AbilityDefinition;

    private Action<AbilityDefinition> OnSelectCallback;
    private Action OnDeselectCallback;

    public void RegisterOnSelect(Action<AbilityDefinition> onSelect)
    {
        OnSelectCallback = onSelect;
    }

    public void RegisterOnDeselect(Action onDeselect)
    {
        OnDeselectCallback = onDeselect;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        OnSelectCallback?.Invoke(AbilityDefinition);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        OnDeselectCallback?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnSelectCallback?.Invoke(AbilityDefinition);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        OnDeselectCallback?.Invoke();
    }
}
