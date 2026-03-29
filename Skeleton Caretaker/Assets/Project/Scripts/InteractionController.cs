using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableLayer;

    public IInteractable CurrentTarget { get; private set; }

    private void Update()
    {
        DetectInteractable();
    }

    public void DetectInteractable()
    {
        // Use a sphere cast to detect interactables in front of the player
        if (Physics.SphereCast(
            player.position,
            0.5f, // Sphere radius
            player.forward,
            out RaycastHit hitInfo,
            interactionRange,
            interactableLayer))
        {
            CurrentTarget = hitInfo.collider.GetComponent<IInteractable>();
        }
        else
        {
            CurrentTarget = null;
        }
    }

    public void TryInteract()
    {
        CurrentTarget?.Interact();
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position + player.forward * interactionRange, 0.5f);
        }
    }
}
