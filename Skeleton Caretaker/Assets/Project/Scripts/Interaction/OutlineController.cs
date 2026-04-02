using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private int originalLayer;
    private int outlineLayer;

    private void Awake()
    {
        originalLayer = gameObject.layer;
        outlineLayer = LayerMask.NameToLayer("Outline");
    }

    public void EnableOutline(Color color)
    {
        Shader.SetGlobalColor("_OutlineColor", color);
        SetLayerRecursively(outlineLayer);
    }

    public void DisableOutline()
    {
        SetLayerRecursively(originalLayer);
    }

    private void SetLayerRecursively(int newLayer)
    {
        gameObject.layer = newLayer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = newLayer;
        }
    }
}
