using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    private bool isHide;
    private Builder builder;
    private MeshRenderer mesh;
    private Vector3[] points;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material hideMaterial;

    private void Awake()
    {
        isHide = false;
        mesh = this.GetComponent<MeshRenderer>();
        SetPoints();
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

    private void SetPoints()
    {
        int angle = (int)(transform.rotation.eulerAngles.y * Mathf.PI/180);
        float sin = (float)Math.Round(Mathf.Sin(angle), 1);
        float cos = (float)Math.Round(Mathf.Cos(angle), 1);

        points = new Vector3[2]{
            new Vector3(transform.position.x - sin, transform.position.y, transform.position.z - cos),
            new Vector3(transform.position.x + sin, transform.position.y, transform.position.z + cos)
        };
    }
}
