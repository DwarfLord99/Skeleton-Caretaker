using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform lookOrigin;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private InteractionPromptUI promptUI;

    public IInteractable CurrentTarget { get; private set; }

    private OutlineController currentOutline;

    private void Update()
    {
        DetectInteractable();
    }

    public void DetectInteractable()
    {
        // Get look direction from camera forward vector
        Vector3 lookDirection = lookOrigin.forward;
        lookDirection.y = 0; // Ignore vertical component for horizontal interaction
        lookDirection.Normalize();

        // Use a raycast to detect interactables in front of the player
        if (Physics.Raycast(
            lookOrigin.position,
            lookDirection,
            out RaycastHit hitInfo,
            interactionRange,
            interactableLayer))
        {
            CurrentTarget = hitInfo.collider.GetComponent<IInteractable>();

            // Look cone check
            Vector3 toTarget = (hitInfo.point - player.position).normalized;
            float dot = Vector3.Dot(lookDirection, toTarget);

            if (dot < 0.6f)
            {
                return; // Target is outside of the look cone
            }

            // Change outline layer if the object is interactable
            OutlineController outline = hitInfo.collider.GetComponent<OutlineController>();
            
            if(outline != currentOutline)
            {
                if (currentOutline != null)
                {
                    currentOutline.DisableOutline();
                }
                if (outline != null)
                {
                    outline.EnableOutline(CurrentTarget.OutlineColor);
                }
                currentOutline = outline;
            }

            // Show interaction prompt
            if (CurrentTarget != null)
            {
                promptUI.Show(CurrentTarget.InteractionMessage);
            }
        }
        else
        {
            CurrentTarget = null;
            if (currentOutline != null)
            {
                currentOutline.DisableOutline();
                currentOutline = null;
            }
            promptUI.Hide();
        }
    }

    public void TryInteract()
    {
        CurrentTarget?.Interact();
    }

    public void StopInteract()
    {
        CurrentTarget?.StopInteract();
    }

    private void OnDrawGizmos()
    {
        if (lookOrigin == null)
            return;


        // Draw the origin point
        Gizmos.DrawSphere(lookOrigin.position, 0.05f);

        // Calculate look direction
        Gizmos.color = Color.yellow;
        Vector3 lookDirection = lookOrigin.forward;
        lookDirection.y = 0f;
        lookDirection.Normalize();

        // Draw the ray
        Gizmos.DrawRay(lookOrigin.position, lookDirection * interactionRange);
    }
}
