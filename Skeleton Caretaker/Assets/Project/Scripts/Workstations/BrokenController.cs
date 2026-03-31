using UnityEngine;

public class BrokenController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Implement interaction logic for the broken object here
        Debug.Log("Interacted with broken object!");
    }

    public InteractableType GetInteractableType()
    {
        return InteractableType.Broken;
    }

    public string InteractionMessage => "Press 'E' to repair";
}
