using UnityEngine;

public class SideMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;

    public SideMovementRuntimeSet activeRuntimeSet;

    private bool moving = true;

    private void OnEnable()
    {
        activeRuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        activeRuntimeSet.Remove(this);
    }

    public void StopMoving()
    {
        moving = false;
    }

    public void StartMoving()
    {
        moving = true;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rigidbody2d.velocity = Constants.sideMovement;
        } else
        {
            rigidbody2d.velocity = Vector2.zero;
        }
        
    }
}
