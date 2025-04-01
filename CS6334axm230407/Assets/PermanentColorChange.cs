using UnityEngine;

public class ForeverColorChange : MonoBehaviour
{
    public Color newColor = Color.red;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = newColor;
    }
}
