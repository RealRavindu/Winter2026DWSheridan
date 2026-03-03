using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class BreathingScript : MonoBehaviour
{
    [Header("Breathing variables")]
    [SerializeField] Slider oxygenBar;
    [SerializeField] float breath, rateOfInhale, rateOfExhale;
    public float maxBreathRate, rateOfBarUpdate;

    [Header("Passing Out variables")]
    [SerializeField] float lastBreathSinceHolding;
    [SerializeField] float holdingBreathThreshold, passOutTimer, passOutThreshold, timeTillWakeUp;
    [SerializeField] bool isBreathBeingHeld = false, isPassedOut = false;

    private void Update()
    {
        //decreasing rate to simulate exhaling
        breath -= rateOfExhale * Time.deltaTime;
        //Capping rate between 0 and max Breath Rate
        if (breath < 0) breath = 0;
        if (breath > maxBreathRate) breath = maxBreathRate;

        //taking a breath. Player is forced to hold M to breathe emulating what its like to breathe in real life.
        if (Input.GetKey(KeyCode.M))
        {
            breath += rateOfInhale * Time.deltaTime;
        }

        //updating oxygen bar slider values
        if (oxygenBar.value < breath / maxBreathRate) oxygenBar.value += rateOfBarUpdate * Time.deltaTime;
        if (oxygenBar.value > breath / maxBreathRate) oxygenBar.value -= rateOfBarUpdate * Time.deltaTime;

        //Checking for player holding breath
        if (!isPassedOut)
        {
            //if breath was in a certain range {holdingBreathThreshold} of a previous breath value
            if (breath > lastBreathSinceHolding - holdingBreathThreshold &&
            breath < lastBreathSinceHolding + holdingBreathThreshold)
            {
                isBreathBeingHeld = true;
            }
            else isBreathBeingHeld = false;

            //if breath is being held, add time to 'passOutTimer'. If breath is not being held then set passOutTimer to 0.
            if (isBreathBeingHeld) passOutTimer += Time.deltaTime;
            else
            {
                passOutTimer = 0;
                lastBreathSinceHolding = breath;
            }

            //if passOutTimer is past a set threshold, call pass out function.
            if (passOutTimer > passOutThreshold)
            {
                Debug.Log("PASSED OUT");
                passOutTimer = 0;
                isPassedOut = true;
                StartCoroutine(passedOut());
            }
        }
        

    }


    //counts up till 'timeTillWakeUp' and then sets isPassedOut to true. Need to implement any animation coding here.
    private IEnumerator passedOut()
    {
        yield return new  WaitForSeconds(timeTillWakeUp);
        isPassedOut = false;
    }


}
