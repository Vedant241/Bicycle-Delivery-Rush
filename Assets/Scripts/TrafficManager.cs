using UnityEngine;
using UnityEngine.Splines;

public class TrafficManager : MonoBehaviour
{
    [Header("Reference of all Car AI Instances")]
    [SerializeField] private CarAIMovement_Simple[] cars;

    void Start()
    {
        
    }
    void Update()
    {
        for(int i = 0; i < cars.Length - 1; i++)
        {
            if(cars[i] != null)
            {
                if(cars[i].GetSplinePath() == cars[i + 1].GetSplinePath())
                {
                    if (cars[i].GetTValue() < cars[i + 1].GetTValue())
                    {
                        if (cars[i + 1].GetCurrentSpeed() < 3f)
                        {
                            //Debug.Log("Slow");
                            cars[i].SetCanMove(false);
                            cars[i].SetCanTurn(false);
                            cars[i].DecelerateTheCar(0f, 20f);
                        }
                    }
                } 
            }
        }
    }
}
