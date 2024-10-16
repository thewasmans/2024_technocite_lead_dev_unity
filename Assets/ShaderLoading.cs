using UnityEngine;

public class ShaderLoading : MonoBehaviour
{
    public ShaderVariantCollection collection;
    // public GraphicsStateColle collections;

    void Start()
    {
        collection.WarmUp();
        collection.WarmUpProgressively(0);
    }
}
