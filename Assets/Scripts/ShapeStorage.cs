using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public static ShapeStorage Instance;
    public List<GameObject> shapeList;
    public static Shape SelectedShape;
    
    private ShapesPool shapesPool;
    private Vector3[] spawnPosition =
    {
        new Vector3(1.0f, 0.0f,0.0f),
        new Vector3(4.0f, 0.0f,0.0f), 
        new Vector3(7.0f, 0.0f,0.0f)
    };

    private const int ShapesAmount = 3;

    private void Awake()
    {
        shapesPool = GetComponent<ShapesPool>();
        Instance = this;
    }

    private void Start() => SpawnShape();

    private void SpawnShape()
    {
        for (int i = 0; i < ShapesAmount; i++)
        {
            var shape = Instantiate(shapesPool.GetShape(), spawnPosition[i], Quaternion.identity);
            shapeList.Add(shape);
        }
    }

    public void ShapeDestroy()
    {
        shapeList.Remove(SelectedShape.gameObject);
        Destroy(SelectedShape.gameObject);
        
        if (shapeList.Count == 0)
            SpawnShape();
    }

    public void ResetToDefaultPlace()
    {
        SelectedShape.RestorePlace();
    }
}
