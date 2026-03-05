using UnityEngine;
using UnityEngine.UI;

public class LubDubScript : MonoBehaviour
{
    //input container
    public KeyCode input;

    //heart rate values
    public float heartRate; //beats per second
    [Tooltip("Divided by beatInterval to set heartRate")]
    [SerializeField] int ratio = 60; //time range for determining input frequency
    [Tooltip("If true halves ratio, doubling hearRate.")]
    [SerializeField] bool doubleBeat = true; //changes evaluation mode to double or single beat

    //fainting values
    public float redlineLimit; //maximum heart rate
    public float stallLimit; //minimum heart rate
    public float faintTimer = 10f;


    //stores time between inputs
    private float beatInterval; // value used in heart rate calculation
    private float previousInterval; // interval between first and second double beat
    private float currentInterval; // interval between second double beat and current time
    
    //stores time of input
    private float firstTime; // time of first input from last double beat
    private float secondTime; // time of first input from current double beat

    //values to evaluate double beat validity
    private float secondBeatTimer; // timer measuring gap between first and second input of double beat
    [SerializeField] float secondBeatLimit = 0.8f; // valid gap between frist and second input of double beat
    private int inputCount = 0; // counts number of inputs

    //external script refrences
    private PassedOutScript PassedOutScript;

    [SerializeField] Slider bloodBar;
    [SerializeField] Gradient barColor;

    private void Start()
    {
        PassedOutScript = GetComponent<PassedOutScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(input))
        {
            BeatInput();
        }
        CheckBeats();
        SetIntervalValue();
        CalculateBeatPerTime();
        Faint();
        UpdateBloodBar();

    }

    /// <summary>
    /// Updates the <c>beatInterval</c> field to the greater of the current or previous time intervals.
    /// </summary>
    /// <remarks>This method calculates two intervals based on the values of <c>firstTime</c>,
    /// <c>secondTime</c>, and <c>Time.time</c>, and assigns the larger interval to <c>beatInterval</c>. It does not
    /// perform validation on the time values; callers should ensure that these fields are set appropriately before
    /// invoking this method.</remarks>
    private void SetIntervalValue()
    {
        previousInterval = secondTime - firstTime;
        currentInterval = Time.time - secondTime;
        if (currentInterval > previousInterval)
        {
            beatInterval = currentInterval;
        }
        else
        {
            beatInterval = previousInterval;
        }
    }

    /// <summary>
    /// Updates <c>heartRate</c> value by dividing <c>ratio</c> by <c>beatInterval</c>
    /// </summary>
    private void CalculateBeatPerTime()
    {
        if (beatInterval == 0)
        {
            heartRate = 0;
            return;
        }
        int doubleBeatRatio = ratio / 2;
        if (!doubleBeat)
        {
            heartRate = ratio / beatInterval;
        }
        else
        {
            heartRate = doubleBeatRatio / beatInterval;
        }

        //Faint();
    }

    /// <summary>
    /// Track time between inputs, check if double beat is valid
    /// </summary>
    private void CheckBeats()
    {
        if(inputCount == 1)
        {
            secondBeatTimer += Time.deltaTime;
        }

        if(secondBeatTimer > secondBeatLimit)
        {
            inputCount = 0;
            currentInterval += 1;
            secondBeatTimer = 0;
            //Failed beat indicator can go here
            Debug.Log($"Second beat failed");
        }
    }

    /// <summary>
    /// Register input from player for heart beat
    /// </summary>
    private void BeatInput()
    {
        //beat effect here
        if (inputCount == 0)
        {
            firstTime = secondTime;
            secondTime = Time.time;
            inputCount++;
        }
        else if (inputCount == 1)
        {
            secondBeatTimer = 0;
            inputCount = 0;
        }
        if (!doubleBeat)
        {
            inputCount = 0;
        }
        //Debug.Log($"beats = {inputCount}");
    }

    private void Faint()
    {
        if (faintTimer > 10) faintTimer = 10;

        if (!PassedOutScript.value)
        {
            if (heartRate > redlineLimit || heartRate < stallLimit)
            {
                faintTimer -= Time.deltaTime;
            }
            else
            {
                faintTimer += Time.deltaTime;
            }
        }
        else
        {
            faintTimer = 10;
        }

        if (faintTimer <= 0)
        {
            //Debug.Log("Player has feinted");
            faintTimer = 10;
            //Passout script here
            PassedOutScript.PassOut();
        }
    }

    private void UpdateBloodBar()
    {
        bloodBar.value = heartRate / 110;
        Image bar = bloodBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        bar.color = barColor.Evaluate((10 - faintTimer) / 10);
    }
}
