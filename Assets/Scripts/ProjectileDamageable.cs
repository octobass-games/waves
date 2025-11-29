using UnityEngine;

public class ProjectileDamageable : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;

    private bool IsExploding;

    public void NonProjectileHit()
    {
        Debug.Log("Hello!");

        Animator.SetTrigger("hit-no-damage");
    }

    public void ProjectileHit()
    {
        if (!IsExploding)
        {
            Animator.SetTrigger("hit-explode");
            IsExploding = true;
        }
    }

    public void OnSlimeExplode()
    {
        Destroy(gameObject);
    }
}
