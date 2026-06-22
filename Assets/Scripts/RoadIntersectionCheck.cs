using UnityEngine;

public class RoadIntersectionCheck : MonoBehaviour
{
    [SerializeField] private int objectInsideIntersection;

    public bool canMove;
    void Start()
    {
        canMove = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == objectInsideIntersection)
        {
            canMove = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == objectInsideIntersection)
        {
            canMove = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == objectInsideIntersection)
        {
            canMove = false;
        }
    }
}
