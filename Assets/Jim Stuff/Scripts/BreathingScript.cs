using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class BreathingScript : MonoBehaviour
{
    private float firstInput;
    private float secondInput;
    private float difference;
    private float differenceCheck;
    private float rate;
    public float time;
    public float faintThreshold;

    //For check values coroutine
    private int inputs = 0;
    public float interval = 1f;
    private float inputsPerSecond;
    public float maximum;
    public float minimum;

    private void Start()
    {
        //StartCoroutine(CheckValues());
    }

    private void Update()
    {
        time = Time.time;
        if (Input.GetKeyDown(KeyCode.M))
        {
            //inputs++;
            InputWithInterval();
        }
        if (difference <= (Time.time - secondInput))
        {
            difference = Time.time - firstInput;
            Debug.Log($"rate changing difference = {difference}");
        }
        rate = 1 / difference;
        Debug.Log($"{rate}/s");
        if (rate < 0.5f)
        {
            differenceCheck = Time.time - secondInput;
        }
        if (differenceCheck > faintThreshold)
        {
            Debug.Log("You have passed out!");
        }
    }

    private void InputWithInterval()
    {
        if (firstInput != 0)
        {
            firstInput = secondInput;
            secondInput = Time.time;
        }
        else
        {
            firstInput = Time.time;
            secondInput = firstInput;
            Debug.Log("Initial input");
        }
        difference = secondInput - firstInput;
        Debug.Log($"Input detect difference = {difference}");


    }

    private IEnumerator CheckValues()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (interval == 1)
            {
                inputsPerSecond = inputs;
            }
            else
            {
                float divider = 1 / interval;
                inputsPerSecond = inputs * divider;
            }
            Debug.Log($"{inputs} inputs made during interval");
            inputs = 0;
            Debug.Log($"{inputsPerSecond} breadths per second");
        }
    }

}
