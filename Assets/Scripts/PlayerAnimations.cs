using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerController;

    private void OnEnable()
    {
        playerController.OnJump += TriggerJumpAnimation;
    }

    private void OnDisable()
    {
        playerController.OnJump -= TriggerJumpAnimation;
    }

    private void Update()
    {
        Vector3 moveDirection = playerController.GetMoveDirection();
        bool isMoving = moveDirection.magnitude >= 0.1f;
        animator.SetBool("isMoving", isMoving);
    }

    private void TriggerJumpAnimation()
    {
        animator.SetTrigger("hasJumped");
    }
}
