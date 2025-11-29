using Octobass.Waves.Save;
using UnityEngine;

public class ProjectileDamageable : MonoBehaviour, ISavable
{
    [SerializeField]
    private string Id;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    [SerializeField]
    private BoxCollider2D BoxCollider;

    private bool HasExploded;
    private string SaveKey;

    void Awake()
    {
        SaveKey = $"projectile-damageable-{Id}";
    }

    public void NonProjectileHit()
    {
        Animator.SetTrigger("hit-no-damage");
    }

    public void ProjectileHit()
    {
        if (!HasExploded)
        {
            Animator.SetTrigger("hit-explode");
            HasExploded = true;
        }
    }

    public void OnSlimeExplode()
    {
        Disable();
    }

    private void Disable()
    {
        SpriteRenderer.enabled = false;
        BoxCollider.enabled = false;
        Animator.enabled = false;
    }

    public void Save(SaveData saveData)
    {
        saveData.Add(SaveKey, HasExploded);
    }

    public void Load(SaveData saveData)
    {
        HasExploded = saveData.Load<bool>(SaveKey);

        if (HasExploded)
        {
            Disable();
        }
    }
}
