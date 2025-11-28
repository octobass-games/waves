using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves
{
    public class OscillatingHider : MonoBehaviour
    {
        [SerializeField]
        private Animator Animator;

        [SerializeField]
        private Oscillator Oscillator;

        public void Hide()
        {
            Oscillator.Pause();
            Animator.SetBool("Hit", true);
        }

        public void OnHideAnimationEnd()
        {
            Oscillator.Oscillate();
            Animator.SetBool("Hit", false);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                if (collision.GetComponent<AttackMove>() != null)
                {
                    Hide();
                }
            }
        }
    }
}
