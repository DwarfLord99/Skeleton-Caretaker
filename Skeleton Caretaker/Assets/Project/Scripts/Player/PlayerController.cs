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
        var target = interaction.CurrentTarget;

        if (target == null)
        {
            interaction.StopInteract();
            return;
        }

        if (target.RequiresHold)
        {
            // Hold-to-interact logic
            if (interactAction.IsPressed())
                interaction.TryInteract();
            else
                interaction.StopInteract();
        }
        else
        {
            // Tap-to-interact logic
            if (interactAction.WasPressedThisFrame())
                interaction.TryInteract();
        }
    }
}
