// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private int camSpeed;
    private int rotateSpeed;
    private Vector2 startPoint;
    private Brick[,] grid;
    private TempBrick tempBrick;
    private Brick brickPrefab;
    private Camera mainCamera;
    private int angle;

    [SerializeField] private Canvas uiMenu;
    // [SerializeField] private Canvas editMenu;
    
    private void Awake()
    {
        grid = new Brick[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;
        camSpeed = 2;
        angle = 45;
        rotateSpeed = 5;
    }

    private void RotateTemp()
    {
        float xDistance = startPoint.x - Input.mousePosition.x;
        if (xDistance != 0) {
            int direction = (xDistance < 0) ? -1 : 1;
            tempBrick.transform.rotation = Quaternion.Euler(0, tempBrick.transform.rotation.eulerAngles.y + direction * rotateSpeed, 0);
            startPoint = Input.mousePosition;
        }
    }

    private void MoveCamera()
    {
        float xDistance = startPoint.x - Input.mousePosition.x;
        float yDistance = startPoint.y - Input.mousePosition.y;

        float interpolation = camSpeed * Time.deltaTime;
        
        Vector3 position = mainCamera.transform.position;
        position.y = mainCamera.transform.position.y;
        position.x = Mathf.Lerp(mainCamera.transform.position.x, mainCamera.transform.position.x + xDistance, interpolation);
        position.z = Mathf.Lerp(mainCamera.transform.position.z, mainCamera.transform.position.z + yDistance, interpolation);
            
        mainCamera.transform.position = position;
        startPoint = Input.mousePosition;
    }

    public void StartPlacing(TempBrick temp, Brick brick)
    {
        if (tempBrick != null)
        {
            if (tempBrick.isAviable)
            {
                EndPlacing();
            }
            else
            {
                CancelPlacing();
            }
        }
        
        tempBrick = Instantiate(temp);
        brickPrefab = brick;

        uiMenu.gameObject.SetActive(true);
    }

    // public void ShowEditMenu()
    // {
    //     editMenu.gameObject.SetActive(true);
    // }

    public void OnMouseDown()
    {
        startPoint = Input.mousePosition;
	}

    public void OnMouseDrag()
    {
        if (tempBrick == null)
        {   
            MoveCamera();
        }
        else if (tempBrick != null && !tempBrick.isDragging)
        {
            RotateTemp();
        }
    }

    public void OnMouseUp()
    {
        if (tempBrick != null && !tempBrick.isDragging)
        {
            tempBrick.transform.rotation = Quaternion.Euler(
                0, Mathf.RoundToInt(tempBrick.transform.rotation.eulerAngles.y / angle) * angle % 180, 0);
        }
        else if (tempBrick != null && tempBrick.isDragging) 
        {
            tempBrick.isDragging = false;
        }
    }

    public void EndPlacing()
    {
        if (tempBrick != null && tempBrick.isAviable)
        {
            Brick brick = Instantiate(brickPrefab, tempBrick.transform.position, tempBrick.transform.rotation);
            brick.SetBuilder(this);
            
            if (tempBrick.parentWall != null)
            {
                brick.transform.parent = tempBrick.parentWall;
            }
            else
            {
                GameObject parentObject = new GameObject();
                parentObject.name = "Wall"; 
                brick.transform.parent = parentObject.transform;
            }

            CancelPlacing();
        }
    }

    public void CancelPlacing()
    {
        if (tempBrick != null)
        {
            Destroy(tempBrick.gameObject);
            tempBrick = null;
            uiMenu.gameObject.SetActive(false);
        }
    }
}
