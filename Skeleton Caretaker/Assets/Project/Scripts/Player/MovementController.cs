using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Keep movement on the horizontal plane
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0; // Keep movement on the horizontal plane
        right.Normalize();

        Vector3 moveDirection = forward * input.y + right * input.x;
        characterController.Move(speed * Time.deltaTime * moveDirection);
        animator.SetFloat("Speed", moveDirection.magnitude);

        RotateTowards(moveDirection);
        ApplyGravity();
    }

    void RotateTowards(Vector3 move)
    {
        if(move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the character grounded
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
