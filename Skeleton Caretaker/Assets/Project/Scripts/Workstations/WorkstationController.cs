using UnityEngine;

public class WorkstationController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Implement interaction logic for the workstation here
        Debug.Log("Interacted with workstation!");
    }

    public InteractableType GetInteractableType()
    {
        return InteractableType.Workstation;
    }

    public string InteractionMessage => "Press 'E' to use";
}
