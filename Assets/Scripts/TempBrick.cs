using UnityEngine;

public class TempBrick : MonoBehaviour
{
    public bool isAviable = true;
    public bool isDragging = true;

    private Camera mainCamera;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material errorMaterial;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetNormal()
    {
        isAviable = true;
        this.GetComponent<MeshRenderer>().material = normalMaterial;
    }

    public void SetError()
    {
        isAviable = false;
        this.GetComponent<MeshRenderer>().material = errorMaterial;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
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
