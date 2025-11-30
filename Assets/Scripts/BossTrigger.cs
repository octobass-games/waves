using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Animator animator;
    void OnTriggerEnter2D(Collider2D col)
    {
        animator.SetTrigger("StartBoss");
    }
}
