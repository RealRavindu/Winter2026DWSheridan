using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class BreathingScript : MonoBehaviour
{
    [Header("Breathing variables")]
    public KeyCode breatheKey;
    [SerializeField] Slider oxygenBar;
    [SerializeField] float breath, rateOfInhale, rateOfExhale;
    public float maxBreath, rateOfBarUpdate;


    [Header("Rate Calculation Variables")]
    [SerializeField] float timeWhenLastInhale;
    public float rateOfBreathing;


    [Header("Passing Out variables")]
    [SerializeField] float lastBreathSinceHolding;
    [SerializeField] float holdingBreathThreshold, passOutTimer, passOutThreshold;
    [SerializeField] bool isBreathBeingHeld = false;
    private PassedOutScript passedOut;

    private void Start()
    {
        passedOut = GetComponent<PassedOutScript>();
    }

    private void Update()
    {

        //decreasing rate to simulate exhaling
        breath -= rateOfExhale * Time.deltaTime;
        //Capping rate between 0 and max Breath Rate
        if (breath < 0) breath = 0;
        if (breath > maxBreath) breath = maxBreath;

        //taking a breath. Player is forced to hold M to breathe emulating what its like to breathe in real life.
        if (Input.GetKey(breatheKey) && !passedOut.value)
        {
            breath += rateOfInhale * Time.deltaTime;
        }

        //updating oxygen bar slider values
        if (oxygenBar.value < breath / maxBreath) oxygenBar.value += rateOfBarUpdate * Time.deltaTime;
        if (oxygenBar.value > breath / maxBreath) oxygenBar.value -= rateOfBarUpdate * Time.deltaTime;


        /*
        //Calculating Rate of Breathing
        if (Input.GetKeyDown(breatheKey)) timeWhenLastInhale = Time.time;

        float difference = Time.time - timeWhenLastInhale;
        rateOfBreathing = 1 / difference;
        */



        //Checking for player holding breath
        if (!passedOut.value)
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
                Debug.Log("Passed out due to holding breath");
                StartCoroutine(passedOut.passedOutTimer());
            }
        }



    }

    //is called when player wakes up from having passed out
    public void Reset()
    {
        Debug.Log("Breathing script reset called");
        passOutTimer = 0;
    }

}
