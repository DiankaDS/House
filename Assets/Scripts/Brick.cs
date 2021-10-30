using UnityEngine;

public class Brick : MonoBehaviour
{
    private bool isHide;
    private Builder builder;
    private MeshRenderer mesh;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material hideMaterial;

    private void Awake()
    {
        isHide = false;
        mesh = this.GetComponent<MeshRenderer>();
    }

    public void SetBuilder(Builder builderToSet)
    {
        builder = builderToSet;
    }
    public void OnMouseDown()
    {
        Transform wall = transform.parent;
        if (isHide) 
        {
            for (int i = 0; i< wall.transform.childCount; i++)
            {
                wall.transform.GetChild(i).GetComponent<Brick>().SetNormal();
            }
        }
        else 
        {
            for (int i = 0; i< wall.transform.childCount; i++)
            {
                wall.transform.GetChild(i).GetComponent<Brick>().SetHide();
            }
        }
    }

    public void SetHide()
    {
        isHide = true;
        mesh.material = hideMaterial;
    }

    public void SetNormal()
    {
        isHide = false;
        mesh.material = normalMaterial;
    }
}
