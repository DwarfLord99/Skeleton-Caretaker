using UnityEngine;
using UnityEngine.UI;

public class BrokenController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject root;
    [SerializeField] private Slider repairProgressBar; // UI element to show repair progress
    [SerializeField] private float repairTime = 3f; // Time required to repair the object
    [SerializeField] private GameObject broken;
    [SerializeField] private GameObject repaired;

    public bool isBeingRepaired = false;

    public bool RequiresHold => true;

    public Color OutlineColor => Color.red;

    private void Awake()
    {
        if (repairProgressBar != null)
        {
            repairProgressBar.value = 0f;
            root.SetActive(false);
            broken.SetActive(true);
        }
    }

    public void Interact()
    {
        // Implement interaction logic for the broken object here
        Debug.Log("Interacted with broken object!");

        if (!isBeingRepaired)
        {
            isBeingRepaired = true;
            StartCoroutine(RepairCoroutine());
        }
    }

    public void StopInteract()
    {
        // Implement logic to stop interaction if needed
        Debug.Log("Stopped interacting with broken object!");
        StopAllCoroutines();
        if (repairProgressBar != null)
        {
            repairProgressBar.value = 0f;
        }
        root.SetActive(false);
        isBeingRepaired = false;
    }

    private System.Collections.IEnumerator RepairCoroutine()
    {
        root.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < repairTime)
        {
            if (!isBeingRepaired)
            {
                repairProgressBar.value = 0f;
                root.SetActive(false);
                yield break; // Exit if repair is stopped
            }

            elapsedTime += Time.deltaTime;
            if (repairProgressBar != null)
            {
                repairProgressBar.value = Mathf.Clamp01(elapsedTime / repairTime);
            }
            yield return null;
        }
        // Repair complete
        Debug.Log("Object repaired!");
        root.SetActive(false);
        broken.SetActive(false);
        repaired.SetActive(true);
        isBeingRepaired = false;
    }

    public InteractableType GetInteractableType()
    {
        return InteractableType.Broken;
    }

    public string InteractionMessage => "Hold 'E' to repair";
}
