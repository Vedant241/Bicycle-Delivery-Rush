using UnityEngine;

public class TrafficSignal : MonoBehaviour
{
    [Header("Timer")]
    private float timer = 0f;

    //Enum for Traffic Signal
    public enum SignalState
    {
        Green,Red,Yellow
    }
    //Current State of Signal
    public SignalState currentSignal;
    void Start()
    {
        currentSignal = SignalState.Green;
    }
    void Update()
    {
        SetTrafficSignal();
        //Debug.Log(currentSignal);
    }
    private void SetTrafficSignal()
    {
        timer += Time.deltaTime;

        if (timer <= 10f)
        {
            currentSignal = SignalState.Green;
        }
        else if (timer <= 13f && timer > 10f)
        {
            currentSignal = SignalState.Yellow;
        }
        else if (timer <= 20f && timer > 13f)
        {
            currentSignal = SignalState.Red;
        }
        else
        {
            timer = 0f;
        }
    }
}
