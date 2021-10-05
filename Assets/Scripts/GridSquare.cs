using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public GameObject activeObject;
    public bool Activated { get; set; }
    public bool Occupied { get; set; }
    public int SquareIndex{ get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Occupied = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Occupied = false;
    }
    
    public void Deactivate()
    {
        activeObject.gameObject.SetActive(false);
        Activated = false;
    }

    public void ActivateSquare()
    {
        activeObject.gameObject.SetActive(true);
        Activated = true;
        Occupied = false;
    }
    
}
