using UnityEngine;

public interface IInteractable
{
    InteractableType GetInteractableType();

    bool RequiresHold { get; }

    Color OutlineColor { get; }

    void Interact();

    void StopInteract();

    string InteractionMessage { get; }
}
