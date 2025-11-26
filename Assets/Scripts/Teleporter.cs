using Octobass.Waves;
using Octobass.Waves.Map;
using Octobass.Waves.Save;
using Octobass.Waves.Spawn;
using UnityEngine;

public class Teleporter : MonoBehaviour, ISavable
{
    [SerializeField]
    private RoomId Room;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private AudioOneShot Audio;

    [SerializeField]
    private SpawnPoint SpawnPoint;

    private string SaveKey;

    private bool IsUnlocked;

    void Awake()
    {
        SaveKey = $"teleporter-{Room}";
    }

    public void Interact()
    {
        if (!IsUnlocked)
        {
            Audio.PlayOneShot();
            
            Unlock();

            if (ServiceLocator.Instance != null)
            {
                Cartographer cartographer = FindFirstObjectByType<Cartographer>();

                if (cartographer != null)
                {
                    cartographer.OnTeleporterUnlocked(Room);
                }
            }
        }
        else
        {
            FindFirstObjectByType<TeleportController>().BeginTeleport();
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

    public RoomId GetRoom()
    {
        return Room;
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
