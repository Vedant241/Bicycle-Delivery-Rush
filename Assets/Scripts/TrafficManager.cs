using UnityEngine;
using UnityEngine.Splines;

public class TrafficManager : MonoBehaviour
{
    [Header("Reference of all Car AI Instances")]
    [SerializeField] private CarAIMovement_Simple[] cars;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        for(int i = 0; i < cars.Length - 1; i++)
        {
            for (int j = i + 1; j < cars.Length; j++)
            {
                if (cars[i] != null && cars[j] != null)
                {
                    if (cars[i].GetSplinePath() == cars[j].GetSplinePath())
                    {
                        if (cars[i].GetTValue() < cars[j].GetTValue())
                        {
                            if (cars[j].GetCurrentSpeed() < 3f)
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
}
