using UnityEngine;

public class TempBrick : MonoBehaviour
{
    public bool isAviable;
    public bool isDragging;

    private Camera mainCamera;
    private MeshRenderer mesh;
    private float minY;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material errorMaterial;

    private void Awake()
    {
        minY = 0.5f;
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
        SetNearPosinion(other, true);
        // SetError();
    }

    public void OnCollisionStay(Collision other)
    {
        SetNearPosinion(other, false);

    }

    public void OnCollisionExit(Collision other)
    {
        transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        // SetNormal();
    }

    public void Update() //OnMouseDrag()
    {
        if (isDragging) 
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                float interpolation = 20 * Time.deltaTime;
                Vector3 pos = transform.position;
                pos.y = transform.position.y;
                pos.x = Mathf.Lerp(transform.position.x, x, interpolation);
                pos.z = Mathf.Lerp(transform.position.z, z, interpolation);
                transform.position = pos;
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

    private void SetNearPosinion(Collision other, bool isFirstCollision)
    {
        if (transform.rotation == other.transform.rotation && transform.position.y == other.transform.position.y)
        {
            if (isFirstCollision) 
            {
                transform.position = new Vector3(transform.position.x, other.transform.position.y + 1, transform.position.z);
            }
            else if (isDragging)
            {
                float x = 0;
                float y = 0;
                float z = 0;

                foreach (ContactPoint contact in other.contacts)
                {
                    x += contact.point.x;
                    z += contact.point.z;
                }

                Vector3 collisionCenter = new Vector3(x/4, other.transform.position.y, z/4);

                Debug.Log(Mathf.Abs(other.transform.position.z - collisionCenter.z));
                
                if (Mathf.Abs(other.transform.position.x - collisionCenter.x) > 0.5 || 
                    Mathf.Abs(other.transform.position.z - collisionCenter.z) > 0.5) 
                {
                    transform.position = 2 * collisionCenter - other.transform.position;
                }
                else 
                {
                    transform.position = new Vector3(transform.position.x, other.transform.position.y + 1, transform.position.z);
                }
            }
        }
    }
}
