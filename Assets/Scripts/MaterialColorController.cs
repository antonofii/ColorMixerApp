using UnityEngine;

public class MaterialColorController : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Color defaultColor;

    private void Start()
    {
        material.color = defaultColor;
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }
}
