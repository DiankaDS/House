// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private float startPoint;
    private Brick[,] grid;
    private TempBrick tempBrick;
    private Brick brickPrefab;
    private Camera mainCamera;

    [SerializeField] private Canvas uiMenu;
    // [SerializeField] private Canvas editMenu;
    
    private void Awake()
    {
        grid = new Brick[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;
    }

    private void RotateTemp()
    {
        float endPoint = Input.mousePosition.x;
        float delta = startPoint - Input.mousePosition.x;
        if (delta != 0) {
            int direction = (delta < 0) ? -1 : 1;
            tempBrick.transform.rotation = Quaternion.Euler(0, tempBrick.transform.rotation.eulerAngles.y + direction * 5, 0);
            startPoint = endPoint;
        }
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
        if (tempBrick != null && !tempBrick.isDragging)
        {
            startPoint = Input.mousePosition.x;
        }
	}

    public void OnMouseDrag()
    {
        if (tempBrick == null)
        {
            //TODO: mainCamera movement
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
                0, Mathf.RoundToInt(tempBrick.transform.rotation.eulerAngles.y / 45) * 45, 0);
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
            Instantiate(brickPrefab, tempBrick.transform.position, tempBrick.transform.rotation);
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
