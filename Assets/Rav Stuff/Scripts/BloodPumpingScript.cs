using System.Collections;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BloodPumpingScript : MonoBehaviour
{
    public KeyCode pumpKey;
    public Slider bloodBar;
    private float firstInput;
    private float secondInput;
    private float difference;
    [SerializeField] float differenceCheck;
    [SerializeField] float rate;
    public float maxBloodRate;
    [SerializeField] float faintThreshold;
    public float rateOfBarUpdate;
    private PassedOutScript passedOut;

    private void Start()
    {
        passedOut = GetComponent<PassedOutScript>();
    }
    private void Update()
    {
        if (!passedOut.value)
        {
            if (Input.GetKeyDown(pumpKey))
            {
                //inputs++;
                InputWithInterval();
            }
        }
        difference = Time.time - firstInput;
        rate = 1 / difference;

        if (!passedOut.value)
        {
            //passing out checks if pump is too high or too low
            if (rate < 0.3f || rate > 0.90f)
            {
                differenceCheck = Time.time - secondInput;
            }
            if (differenceCheck > faintThreshold)
            {
                string msg = (rate < 0.5f) ? "Passed out due to not pumping enough blood" : "Passed out due to heart attack (too much blood pump)";
                Debug.Log(msg);
                StartCoroutine(passedOut.passedOutTimer());
            }
        }


        //updating oxygen bar slider values
        if (bloodBar.value < rate / maxBloodRate) bloodBar.value += rateOfBarUpdate * Time.deltaTime;
        if (bloodBar.value > rate / maxBloodRate) bloodBar.value -= rateOfBarUpdate * Time.deltaTime;



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
        }
        difference = secondInput - firstInput;


    }

    //is called when player wakes up from having passed out
    public void Reset()
    {
        secondInput = Time.time;
    }


}