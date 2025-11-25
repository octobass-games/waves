using Octobass.Waves;
using Octobass.Waves.Map;
using Octobass.Waves.Save;
using UnityEngine;

public class Teleporter : MonoBehaviour, ISavable
{
    [SerializeField]
    private RoomId Name;

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

        if (TeleportController == null)
        {
            Debug.LogWarning($"[Teleporter]: TeleportController not set for {gameObject.name}");
        }
    }

    public void Interact()
    {
        if (!IsUnlocked)
        {
            Audio.PlayOneShot();
            
            Unlock();

            if (ServiceLocator.Instance != null)
            {
                Cartographer cartographer = ServiceLocator.Instance.Get<Cartographer>();

                if (cartographer != null)
                {
                    cartographer.OnTeleporterUnlocked(Name);
                }
            }
        }
        else
        {
            TeleportController.BeginTeleport();
        }
    }

    public void Load(SaveData saveData)
    {
        IsUnlocked = saveData.Load<bool>(SaveKey);

        if (IsUnlocked)
        {
            Unlock();
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.Add(SaveKey, IsUnlocked);
    }

    private void Unlock()
    {
        Animator.SetTrigger("open");
        IsUnlocked = true;
    }
}
