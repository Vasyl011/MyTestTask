using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Shape : MonoBehaviour
{
    public static Action MouseUp;
    
    private Vector3 screenPoint;
    private Vector3 offset;

    private float x;
    private float y;
    private float z;

    private void Awake()
    {
        var pos = transform.position;
        
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }

    private void OnMouseDown()
    {
        ShapeStorage.SelectedShape = this;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
 
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        MouseUp?.Invoke();
    }

    public void RestorePlace()
    {
        transform.position = new Vector3(x, y, z);
    }
}
