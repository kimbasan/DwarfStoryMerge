using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        // reset after reuse of object
        animator.SetBool(Constants.DEAD, false);
        animator.SetBool(Constants.STANDING, false);
    }

    public void StopRunning()
    {
        animator.SetBool(Constants.STANDING, true);
    }

    public void StartRunning()
    {
        animator.SetBool(Constants.STANDING, false);
    }

    public void Attack()
    {
        animator.SetTrigger(Constants.ATTACK);
    }

    public void TakeDamage()
    {
        animator.SetTrigger(Constants.DAMAGED);
    }

    public void Die()
    {
        animator.SetBool(Constants.DEAD, true);
    }
}
