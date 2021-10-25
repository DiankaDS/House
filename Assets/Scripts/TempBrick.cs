using UnityEngine;

public class TempBrick : MonoBehaviour
{
    public bool isAviable;
    public bool isDragging;

    private Camera mainCamera;
    private MeshRenderer mesh;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material errorMaterial;

    private void Awake()
    {
        isAviable = true;
        isDragging = true;
        mainCamera = Camera.main;
        mesh = this.GetComponent<MeshRenderer>();
    }

    public void SetNormal()
    {
        isAviable = true;
        mesh.material = normalMaterial;
    }

    public void SetError()
    {
        isAviable = false;
        mesh.material = errorMaterial;
    }

    public void OnCollisionEnter(Collision other)
    {
        SetError();
    }

    public void OnCollisionExit(Collision other)
    {
        SetNormal();
    }

    public void Update() //OnMouseDrag()
    {
        if (isDragging) {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.y);
                int z = Mathf.RoundToInt(worldPosition.z);

                transform.position = new Vector3(x, y + 0.5f, z);
            }
        }
        
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }
}
