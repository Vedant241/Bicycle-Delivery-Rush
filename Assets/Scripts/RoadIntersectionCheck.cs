using UnityEngine;

public class RoadIntersectionCheck : MonoBehaviour
{
    public bool canMove;
    void Start()
    {
        canMove = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        canMove = false;
    }
    private void OnTriggerExit(Collider other)
    {
        canMove = true;
    }
    private void OnTriggerStay(Collider other)
    {
        canMove = false;
    }
}
