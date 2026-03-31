using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI promptText;

    public void Show(string message)
    {
        promptText.text = message;
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
