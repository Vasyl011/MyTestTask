using System.Collections.Generic;
using UnityEngine;

public class ShapesPool : MonoBehaviour
{
    public List<GameObject> shapes;
    
    public GameObject GetShape()
    {
        int indexShape = Random.Range(0, shapes.Count);
        return shapes[indexShape];
    }
}
