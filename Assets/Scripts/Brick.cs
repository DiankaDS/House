using UnityEngine;

public class Brick : MonoBehaviour
{
    private Builder builder;
    private MeshRenderer mesh;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material tempMaterial;

    private void Awake()
    {
        mesh = this.GetComponent<MeshRenderer>();
    }

    public void SetBuilder(Builder builderToSet)
    {
        builder = builderToSet;
    }
    public void OnMouseDown()
    {
        builder.EditWall(transform.parent);
    }

    public void SetTempMaterial()
    {
        mesh.material = tempMaterial;
    }

    public void SetNormalMaterial()
    {
        mesh.material = normalMaterial;
    }
}
