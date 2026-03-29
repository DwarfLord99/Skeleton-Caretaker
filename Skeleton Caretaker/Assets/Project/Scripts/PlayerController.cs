using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementController movement;
    private InteractionController interaction;

    private InputAction moveAction;
    private InputAction interactAction;

    private Vector2 moveInput;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");

        movement = GetComponent<MovementController>();
        interaction = GetComponent<InteractionController>();
    }

    void Update()
    {
        HandleMovement();
        movement.Move(moveInput);
        HandleInteract();
    }

    void HandleMovement()
    {
        // Handle player movement logic here
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void HandleInteract()
    {
        // Handle player interaction logic here
        if (interactAction.WasPressedThisFrame())
        {
            interaction.TryInteract();
        }
    }
}
