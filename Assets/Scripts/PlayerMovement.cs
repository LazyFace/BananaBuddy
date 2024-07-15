using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private Transform frontGroundCheckPosition;
    [SerializeField] private Transform backGroundCheckPosition;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 movementDirection;
    private int jumpsRemaining;

    [Header("Camara Reference")]
    [SerializeField] private Transform freeLookCamera;

    [Header("Input System Rerefence")]
    [SerializeField] private InputActionReference movementActionInput;
    [SerializeField] private InputActionReference jumpActionInput;

    public delegate void JumpAction();
    public event JumpAction OnJump;

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector2 inputDirection = movementActionInput.action.ReadValue<Vector2>();
        movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        movementDirection = freeLookCamera.TransformDirection(movementDirection);
        movementDirection.y = 0;
        movementDirection *= moveSpeed;

        MoveRigidbody(movementDirection);
        RotatePlayer(movementDirection);
    }

    private void HandleJump()
    {
        if (jumpActionInput.action.WasPressedThisFrame() && jumpsRemaining > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpsRemaining--;
            OnJump?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();
    }

    private void CheckGroundStatus()
    {
        if (Physics.Raycast(frontGroundCheckPosition.position, Vector3.down, 0.1f, groundLayer) || Physics.Raycast(backGroundCheckPosition.position, Vector3.down, 0.1f, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void MoveRigidbody(Vector3 move)
    {
        Vector3 newPosition = rb.position + move * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    private void RotatePlayer(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * 10 * Time.deltaTime);
        }
    }

    public Vector3 GetMoveDirection()
    {
        return movementDirection;
    }
}
