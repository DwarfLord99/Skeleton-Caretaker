public interface IInteractable
{
    InteractableType GetInteractableType();

    void Interact();

    string InteractionMessage { get; }
}
