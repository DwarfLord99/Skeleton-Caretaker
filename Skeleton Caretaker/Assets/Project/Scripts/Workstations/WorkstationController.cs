using UnityEngine;

public class WorkstationController : MonoBehaviour, IInteractable
{
    public bool RequiresHold => false;

    public Color OutlineColor => Color.blue;

    public void Interact()
    {
        // Implement interaction logic for the workstation here
        Debug.Log("Interacted with workstation!");
    }

    public void StopInteract() { }

    public InteractableType GetInteractableType()
    {
        return InteractableType.Workstation;
    }

    public string InteractionMessage => "Press 'E' to use";
}
