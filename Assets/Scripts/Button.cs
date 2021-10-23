// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TempBrick tempPrefab;
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private Builder builder;

    public void OnPointerDown (PointerEventData eventData)
    {
        // gameObject.SetActive(false);
        builder.StartPlacing(tempPrefab, brickPrefab);
    }

}
