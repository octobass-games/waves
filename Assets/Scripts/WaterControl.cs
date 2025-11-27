using Octobass.Waves;
using Octobass.Waves.Item;
using Octobass.Waves.Save;
using UnityEngine;

public class WaterControl : MonoBehaviour, ISavable
{
    public bool IsUnlocked { get; private set; }

    [SerializeField]
    private AbilityDefinition WaterControlAbility;

    private const string SaveKey = "water-control-unlocked";

    public void Load(SaveData saveData)
    {
        IsUnlocked = saveData.Load<bool>(SaveKey);
    }

    public void Save(SaveData saveData)
    {
        saveData.Add(SaveKey, IsUnlocked);
    }

    public void OnItemPickedUp(ItemInstance item)
    {
        if (item is AbilityItemInstance i)
        {
            if (i.Ability.Definition.Name == WaterControlAbility.Name)
            {
                IsUnlocked = true;
            }
        }
    }
}
