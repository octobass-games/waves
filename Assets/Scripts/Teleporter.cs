using Octobass.Waves.Save;
using UnityEngine;

public class Teleporter : MonoBehaviour, ISavable
{
    [SerializeField]
    private string Name;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private TeleportController TeleportController;

    [SerializeField]
    private AudioOneShot Audio;

    private string SaveKey;

    private bool IsUnlocked;

    void Awake()
    {
        SaveKey = $"teleporter-{Name}";
    }

    public void Interact()
    {
        if (!IsUnlocked)
        {
            Unlock();
        }
        else
        {
            TeleportController.BeginTeleport();
        }
    }

    public void Load(SaveData saveData)
    {
        IsUnlocked = saveData.Load<bool>(SaveKey);
    }

    public void Save(SaveData saveData)
    {
        saveData.Add(SaveKey, IsUnlocked);
    }

    private void Unlock()
    {
        Animator.SetTrigger("open");
        Audio.PlayOneShot();
        IsUnlocked = true;
    }
}
