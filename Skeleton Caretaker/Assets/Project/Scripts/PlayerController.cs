using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementController movement;

    private InputAction moveAction;

    private Vector2 moveInput;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        movement = GetComponent<MovementController>();
    }

    void Update()
    {
        HandleMovement();
        movement.Move(moveInput);
    }

    void HandleMovement()
    {
        // Handle player movement logic here
        moveInput = moveAction.ReadValue<Vector2>();
    }
}
